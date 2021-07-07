using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IAddressService
    {
        IList<Country> GetCountries();
        Country GetCountry(int id);
    }
}