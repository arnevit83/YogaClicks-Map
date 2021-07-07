using System;
using System.Runtime.Serialization;

namespace Clicks.Yoga.Media
{
    public class UnsupportedMediaException : Exception
    {
        public const string DefaultMessage = "Unsupported media type.";

        public UnsupportedMediaException() : this(DefaultMessage) { }
        public UnsupportedMediaException(string message) : base(message) { }
        public UnsupportedMediaException(string message, Exception inner) : base(message, inner) { }
        protected UnsupportedMediaException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}