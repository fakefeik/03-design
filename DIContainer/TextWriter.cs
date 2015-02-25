using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    class TextWriter : ITextWriter
    {
        public void Write(string writeable)
        {
            Console.WriteLine(writeable);   
        }
    }

    public interface ITextWriter
    {
        void Write(string writeable);
    }
}
