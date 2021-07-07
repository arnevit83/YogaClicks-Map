using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Entities
{
    public class SearchLink
    {
        public SearchLink()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
    }
}
