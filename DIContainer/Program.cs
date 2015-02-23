using System;
using System.Linq;
using DIContainer.Commands;
using Ninject;
using Ninject.Extensions.Conventions;


namespace DIContainer
{
    public class Program
    {
        private readonly CommandLineArgs arguments;
        private readonly ITextWriter writer;
        private readonly ICommand[] commands;

        public Program(CommandLineArgs arguments, ITextWriter writer,params ICommand[] commands)
        {
            this.arguments = arguments;
            this.commands = commands;
            this.writer = writer;
        }

        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Bind<ICommand>().To<PrintTimeCommand>();
            kernel.Bind<ICommand>().To<TimerCommand>();
            kernel.Bind<ICommand>().To<HelpCommand>();
            kernel.Bind<ITextWriter>().To<TextWriter>();
            kernel.Bind<CommandLineArgs>().To<CommandLineArgs>().WithConstructorArgument(args);
            kernel.Get<Program>().Run();
            //var arguments = new CommandLineArgs(args);
            //var printTime = new PrintTimeCommand();
            //var timer = new TimerCommand(arguments);
            //var commands = new ICommand[] { printTime, timer };
            //new Program(arguments, commands).Run();
            
        }

        public void Run()
        {
            if (arguments.Command == null)
            {
                writer.Write("Please specify <command> as the first command line argument");
                return;
            }
            var command = commands.FirstOrDefault(c => c.Name.Equals(arguments.Command, StringComparison.InvariantCultureIgnoreCase));
            if (command == null)
                writer.Write(string.Format("Sorry. Unknown command {0}", arguments.Command));
            else
                command.Execute();
        }
    }
}
