using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class AddressService : ServiceBase, IAddressService
    {
        public AddressService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) {}

        public IList<Country> GetCountries()
        {
            return EntityContext.Countries.OrderBy(e => e.EnglishName).ToList();
        }

        public Country GetCountry(int id)
        {
            return EntityContext.Countries.Get(id);
        }
    }
}