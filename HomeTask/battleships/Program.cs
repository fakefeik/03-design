using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Ninject;

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

            var settings = new Settings("settings.txt");
            var gen = new MapGenerator(settings, new Random(settings.RandomSeed));
            var vis = new GameVisualizer();
            var monitor = new ProcessMonitor(TimeSpan.FromSeconds(settings.TimeLimitSeconds * settings.GamesCount), settings.MemoryLimit);
		    var aiTester = new AiTester(settings, gen, vis, monitor);
            if (File.Exists(args[0]))
                aiTester.TestSingleFile(args[0]);
            else Console.WriteLine("No AI exe-file " + args[0]);
            
		    //IKernel kernel = new StandardKernel();
		    //kernel.Bind<Settings>().To<Settings>().WithConstructorArgument("settings.txt");
		    //if (File.Exists(args[0]))
		    //    kernel.Get<AiTester>().TestSingleFile(args[0]);
		    //else Console.WriteLine("No AI exe-file " + args[0]);
		}
	}
}