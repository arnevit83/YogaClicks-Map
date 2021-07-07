using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Requests
{
    public interface IRequestCompletionHandler
    {
        void Accept(Request request);
        void Reject(Request request);
    }
}