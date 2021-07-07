using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class LifeChallengeChooserModel : MultipleEntitySelectorModel<YmStoryLifeChallenge, NamedSummaryModel<YmStoryLifeChallenge>>
    {
        public LifeChallengeChooserModel() { }

        public LifeChallengeChooserModel(IEnumerable<YmStoryLifeChallenge> entities) : base(entities) { }

        public LifeChallengeChooserModel(IEnumerable<YmStoryLifeChallenge> selections, IEnumerable<YmStoryLifeChallenge> options) : base(selections, options) { }

        public void PopulateCollection(ICollection<YmStoryLifeChallenge> collection, IYmStoryService ymStoryServiceService)
        {
            PopulateCollection(collection, ymStoryServiceService.GetLifeChallenge);
        }
    }
}