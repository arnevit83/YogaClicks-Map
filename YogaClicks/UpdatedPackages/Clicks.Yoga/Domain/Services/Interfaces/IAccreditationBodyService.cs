using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IAccreditationBodyService
    {
        IList<AccreditationBody> LuckyDip();
        IList<AccreditationBody> GetAccreditationBodies();
        AccreditationBody GetAccreditationBody(int id);
    }
}