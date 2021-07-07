
using System;
using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class YmStoryLifeChallenge : Entity, INamed
    {
        public string Name { get; set; }
        public virtual ICollection<YmStoryLifeChallengeBucket> LifeChallengebucket { get; set; }
    }
}