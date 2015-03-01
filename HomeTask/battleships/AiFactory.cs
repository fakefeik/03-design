using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public class AiFactory : IAiFactory
    {
        public Ai CreateAi(string exe, Action<Process> registerProcess)
        {
            return new Ai(exe, registerProcess);
        }
    }
}
