using System;

namespace DIContainer.Commands
{
    public class PrintTimeCommand : BaseCommand
    {
        private ITextWriter writer;

        public PrintTimeCommand(ITextWriter writer)
        {
            this.writer = writer;
        }

        public override void Execute()
        {
            writer.Write(DateTime.Now.ToString());
        }
    }
}