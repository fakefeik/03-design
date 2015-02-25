using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer.Commands
{
    class HelpCommand : BaseCommand
    {
        private Lazy<ICommand[]> commands;
        private ITextWriter writer;

        public HelpCommand(Lazy<ICommand[]> commands, ITextWriter writer)
        {
            this.commands = commands;
            this.writer = writer;
        }

        public override void Execute()
        {
            foreach (var command in commands.Value)
            {
                writer.Write(command.Name);   
            }
        }
    }
}
