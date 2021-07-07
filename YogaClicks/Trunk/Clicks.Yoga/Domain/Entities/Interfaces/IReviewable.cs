using System;
using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public interface IReviewable : IEntityHandle
    {
        bool Active { get; }
        IEnumerable<T> FilterReviewDetailTypes<T>(IEnumerable<T> types) where T : IReviewDetailType;
        IEnumerable<Tuple<string, string>> GetReviewHelperDetails();
    }
}