using System;
using System.Runtime.Serialization;

namespace Clicks.Yoga.Domain
{
    [Serializable]
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message) { }
        public AuthenticationException(string message, System.Exception inner) : base(message, inner) { }
        protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class AuthenticatedException : Exception
    {
        public const string DefaultMessage = "There is already a currently authenticated user.";

        public AuthenticatedException() : this(DefaultMessage) {}
        public AuthenticatedException(string message) : base(message) {}
        public AuthenticatedException(string message, System.Exception inner) : base(message, inner) {}
        protected AuthenticatedException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }

    [Serializable]
    public class NotAuthenticatedException : Exception
    {
        public const string DefaultMessage = "There is no currently authenticated user.";

        public NotAuthenticatedException() : this(DefaultMessage) { }
        public NotAuthenticatedException(string message) : base(message) { }
        public NotAuthenticatedException(string message, System.Exception inner) : base(message, inner) { }
        protected NotAuthenticatedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class PermissionDeniedException : Exception
    {
        public const string DefaultMessage = "The current user does not have permission to perform this action.";

        public PermissionDeniedException() : this(DefaultMessage) { }
        public PermissionDeniedException(string message) : base(message) { }
        public PermissionDeniedException(string message, System.Exception inner) : base(message, inner) { }
        protected PermissionDeniedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class UnacceptablePasswordException : Exception
    {
        public const string DefaultMessage = "The password does not match the defined requirements.";

        public UnacceptablePasswordException() : this(DefaultMessage) { }
        public UnacceptablePasswordException(string message) : base(message) { }
        public UnacceptablePasswordException(string message, System.Exception inner) : base(message, inner) { }
        protected UnacceptablePasswordException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class AuthenticationProviderException : Exception
    {
        public AuthenticationProviderException(string message) : base(message) { }
        public AuthenticationProviderException(string message, System.Exception inner) : base(message, inner) { }
        protected AuthenticationProviderException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class DuplicateUsernameException : Exception
    {
        public const string DefaultMessage = "The given user identifier is owned by another user.";

        public DuplicateUsernameException() : this(DefaultMessage) { }
        public DuplicateUsernameException(string message) : base(message) { }
        public DuplicateUsernameException(string message, System.Exception inner) : base(message, inner) { }
        protected DuplicateUsernameException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class UnknownEntityException : Exception
    {
        public const string DefaultMessage = "There is no active entity matching the given identifier.";

        public UnknownEntityException() : this(DefaultMessage) { }
        public UnknownEntityException(string message) : base(message) { }
        public UnknownEntityException(string message, System.Exception inner) : base(message, inner) { }
        protected UnknownEntityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class UnknownEntityTypeException : Exception
    {
        public const string DefaultMessage = "Unknown entity type.";

        public UnknownEntityTypeException() : this(DefaultMessage) { }
        public UnknownEntityTypeException(string message) : base(message) { }
        public UnknownEntityTypeException(string message, System.Exception inner) : base(message, inner) { }
        protected UnknownEntityTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class UnknownIdentifierException : Exception
    {
        public const string DefaultMessage = "The given identifier could not be found.";

        public UnknownIdentifierException() : this(DefaultMessage) { }
        public UnknownIdentifierException(string message) : base(message) { }
        public UnknownIdentifierException(string message, System.Exception inner) : base(message, inner) { }
        protected UnknownIdentifierException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class RepeatedActionException : Exception
    {
        public const string DefaultMessage = "The specified action has already been completed.";

        public RepeatedActionException() : this(DefaultMessage) { }
        public RepeatedActionException(string message) : base(message) { }
        public RepeatedActionException(string message, System.Exception inner) : base(message, inner) { }
        protected RepeatedActionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class OpportunityExpiredException : Exception
    {
        public const string DefaultMessage = "The opportunity to perform this action has expired.";

        public OpportunityExpiredException() : this(DefaultMessage) { }
        public OpportunityExpiredException(string message) : base(message) { }
        public OpportunityExpiredException(string message, System.Exception inner) : base(message, inner) { }
        protected OpportunityExpiredException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class FriendshipStatusException : Exception
    {
        public const string DefaultMessage = "The operation could not be completed because of the current status of the friendship.";

        public FriendshipStatusException(FriendshipStatus status) : this(DefaultMessage) { }
        public FriendshipStatusException(string message) : base(message) { }
        public FriendshipStatusException(string message, System.Exception inner) : base(message, inner) { }
        protected FriendshipStatusException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public FriendshipStatus Status { get; private set; }
    }

    [Serializable]
    public class RequestStatusException : Exception
    {
        public RequestStatusException(string message) : base(message) { }
        public RequestStatusException(string message, System.Exception inner) : base(message, inner) { }
        protected RequestStatusException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
