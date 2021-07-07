using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class WhatTheScienceSaysesModel
    {
        public WhatTheScienceSaysesModel(IEnumerable<WhatTheScienceSays> whatTheScienceSayses)
        {
            WhatTheScienceSayses = new List<WhatTheScienceSaysModel>();

            foreach (var whatTheScienceSays in whatTheScienceSayses)
            {
                WhatTheScienceSayses.Add(new WhatTheScienceSaysModel(whatTheScienceSays));
            }
        }

        public IList<WhatTheScienceSaysModel> WhatTheScienceSayses { get; private set; }
    }
}