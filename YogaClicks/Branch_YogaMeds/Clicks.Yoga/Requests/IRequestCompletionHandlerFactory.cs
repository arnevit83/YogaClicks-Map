using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Requests
{
    public interface IRequestCompletionHandlerFactory
    {
        IRequestCompletionHandler GetHandler(RequestType requestType);
    }
}