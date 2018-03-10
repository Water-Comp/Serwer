using System;

namespace ServerConsole.source.exceptions
{
    class InvalidNumberOfParametersException : Exception
    {
        public InvalidNumberOfParametersException(string msg) : base(msg)
        {

        }
    }
}
