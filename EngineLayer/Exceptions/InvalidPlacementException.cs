using System;

namespace EngineLayer.Exceptions
{
    public sealed class InvalidPlacementException : Exception
    {
        public InvalidPlacementException(string message) : base(message)
        {
        }
    }
}
