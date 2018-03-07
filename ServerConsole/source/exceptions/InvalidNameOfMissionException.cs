using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerConsole.source.exceptions
{
    class InvalidNameOfMissionException : Exception
    {
        public InvalidNameOfMissionException(string msg)
            :base (msg)
        {
        }
    }
}
