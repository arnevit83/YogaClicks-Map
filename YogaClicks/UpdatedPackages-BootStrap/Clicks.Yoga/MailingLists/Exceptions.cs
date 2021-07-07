using System;
using System.Runtime.Serialization;

namespace Clicks.Yoga.MailingLists
{
    [Serializable]
    public class MailingListProviderException : Exception
    {
        public MailingListProviderException() { }
        public MailingListProviderException(string message) : base(message) { }
        public MailingListProviderException(string message, System.Exception inner) : base(message, inner) { }
        protected MailingListProviderException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class MailingListAddressUnknownException : Exception
    {
        public MailingListAddressUnknownException() { }
        public MailingListAddressUnknownException(string message) : base(message) { }
        public MailingListAddressUnknownException(string message, System.Exception inner) : base(message, inner) { }
        protected MailingListAddressUnknownException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}