using System;
using System.Diagnostics;
using System.Linq;

namespace PerfLogger
{
    enum TimeFormat
    {
        Seconds,
        Milliseconds,
        Ticks
    }

    internal class PerfLogger : IDisposable
    {
        private Stopwatch sw;
        private TimeFormat format;
        private string name;

        public PerfLogger(string sayable, TimeFormat format)
        {
            name = sayable;
            this.format = format;
            sw = new Stopwatch();
            sw.Start();
        }

        public static PerfLogger Measure(string name, TimeFormat format)
        {
            return new PerfLogger(name, format);
        }

        public void Dispose()
        {
            sw.Stop();
            var time = "";
            switch (format)
            {
                case TimeFormat.Milliseconds:
                    time = sw.ElapsedMilliseconds.ToString();
                    break;
                case TimeFormat.Seconds:
                    time = sw.Elapsed.ToString();
                    break;
                case TimeFormat.Ticks:
                    time = sw.ElapsedTicks.ToString();
                    break;
            }
            Console.WriteLine(name + " " + time);
        }
    }

    class Program
	{    
		static void Main(string[] args)
		{
			var sum = 0.0;
			using (PerfLogger.Measure("100M for iterations", TimeFormat.Seconds))
				for (var i = 0; i < 100000000; i++) sum += i;
			using (PerfLogger.Measure("100M LINQ iterations", TimeFormat.Seconds))
				sum -= Enumerable.Range(0, 100000000).Sum(i => (double)i);
			Console.WriteLine(sum);
            Console.ReadKey();
		}
	}
}
