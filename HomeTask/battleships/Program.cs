using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Ninject;
using NLog;

namespace battleships
{
	public class Program
	{
		private static void Main(string[] args)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			if (args.Length == 0)
			{
				Console.WriteLine("Usage: {0} <ai.exe>", Process.GetCurrentProcess().ProcessName);
				return;
			}

            Logger resultsLog = LogManager.GetLogger("results");
            IKernel kernel = new StandardKernel();
            kernel.Bind<Settings>().To<Settings>().WithConstructorArgument("settings.txt");
            var settings = kernel.Get<Settings>();
            kernel.Bind<Random>().ToConstant(new Random(settings.RandomSeed));
            kernel.Bind<ProcessMonitor>().To<ProcessMonitor>()
                .WithConstructorArgument(TimeSpan.FromSeconds(settings.TimeLimitSeconds * settings.GamesCount))
                .WithConstructorArgument((long) settings.MemoryLimit);
		    var tester = kernel.Get<AiTester>();
		    var monitor = kernel.Get<ProcessMonitor>();
		    var generator = kernel.Get<MapGenerator>();

		    tester.Ai = new Ai(args[0], monitor.Register);
		    tester.onGameCrashed += () => tester.Ai = new Ai(args[0], monitor.Register);
		    tester.onCreateGame += () => tester.Game = new Game(generator.GenerateMap(), tester.Ai);
		    if (File.Exists(args[0]))
		    {
		        var writer = tester.TestSingleFile(args[0]);
                writer.onLog += resultsLog.Info;
                writer.WriteTotal();
		    }
		    else Console.WriteLine("No AI exe-file " + args[0]);
		}
	}
}