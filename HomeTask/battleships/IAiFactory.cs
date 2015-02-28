using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public interface IAiFactory
    {
        Ai CreateAi(string exe, ProcessMonitor monitor);
    }
}
