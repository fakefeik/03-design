using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentTask
{
    class Command
    {
        private Action act;
        private bool isUntil;

        public Command(Action action)
        {
            this.act = action;
        }

        public Command(Func<Behaviour, Behaviour> func)
        {
            
        }

        public void Execute()
        {
            if (isUntil)
            {
                while (!Console.KeyAvailable)
                {
                    act();
                }
            }
        }
    }

    public class Behaviour
    {
        List<Action> commands = new List<Action>();
     
        public Behaviour Say(string sayable)
        {
            commands.Add(() => Console.WriteLine(sayable));
            return this;
        }

        public Behaviour UntilKeyPressed(Func<Behaviour, Behaviour> action)
        {
            commands.Add(() =>
            {
                while (!Console.KeyAvailable)
                {
                    Console.ReadKey();
                    action(new Behaviour()).Execute();
                }
            });
            return this;
        }

        public void Execute()
        {
            foreach (var command in commands)
            {
                command();
            }
        }
    }
}
