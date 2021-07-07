using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Styles
{
    public class StyleTraitsIndexModel
    {
        public StyleTraitsIndexModel(string traitIdString, string traitNameString)
        {
            TraitIdString = traitIdString;
            TraitNameString = traitNameString;
        }

        public string TraitIdString { get; set; }
        public string TraitNameString { get; set; }
    }
}