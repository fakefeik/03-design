using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public class AiFactory : IAiFactory
    {
        private ProcessMonitor monitor;

        public AiFactory(ProcessMonitor monitor)
        {
            this.monitor = monitor;
        }

        public Ai CreateAi(string exe)
        {
            return new Ai(exe, monitor);
        }
    }
}
