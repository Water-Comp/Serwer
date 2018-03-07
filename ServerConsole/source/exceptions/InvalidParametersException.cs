using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerConsole.source.exceptions
{
    class InvalidParametersException : Exception
    {
        public InvalidParametersException(string msg)
            : base(msg)
        {
        }
    }
}
