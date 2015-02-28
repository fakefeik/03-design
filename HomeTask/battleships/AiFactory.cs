using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public class AiFactory : IAiFactory
    {
        public Ai CreateAi(string exe, ProcessMonitor monitor)
        {
            return new Ai(exe, monitor);
        }
    }
}
