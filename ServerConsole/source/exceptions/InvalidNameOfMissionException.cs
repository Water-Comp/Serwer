using System;

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
