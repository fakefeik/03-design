using System;
using System.Threading;

namespace DIContainer.Commands
{
    public class TimerCommand : BaseCommand
    {
        private readonly CommandLineArgs arguments;
        private ITextWriter writer;

        public TimerCommand(CommandLineArgs arguments, ITextWriter writer)
        {
            this.arguments = arguments;
            this.writer = writer;
        }

        public override void Execute()
        {
            var timeout = TimeSpan.FromMilliseconds(arguments.GetInt(0));
            writer.Write("Waiting for " + timeout);
            Thread.Sleep(timeout);
            writer.Write("Done!");
        }
    }
}