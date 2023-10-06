using System;
using System.Runtime.Serialization;

namespace EngineLayer.Exceptions
{
    [Serializable]
    public sealed class InvalidPlacementException : Exception
    {
        public InvalidPlacementException(string message) : base(message)
        {
        }
        private InvalidPlacementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
