using System;

namespace ServerConsole.source.exceptions
{
    class LowMemoryException : Exception
    {
        public LowMemoryException(string msg)
            : base(msg)
        {
        }
    }
}
