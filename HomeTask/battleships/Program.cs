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

            IKernel kernel = new StandardKernel();
            kernel.Bind<Settings>().To<Settings>().WithConstructorArgument("settings.txt");
            var settings = kernel.Get<Settings>();
            kernel.Bind<Random>().ToConstant(new Random(settings.RandomSeed));
            kernel.Bind<ProcessMonitor>().To<ProcessMonitor>()
                .WithConstructorArgument(TimeSpan.FromSeconds(settings.TimeLimitSeconds * settings.GamesCount))
                .WithConstructorArgument((long) settings.MemoryLimit);
            if (File.Exists(args[0]))
                kernel.Get<AiTester>().TestSingleFile(args[0]);
            else Console.WriteLine("No AI exe-file " + args[0]);
		}
	}
}