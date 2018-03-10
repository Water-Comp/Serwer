using System;

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
