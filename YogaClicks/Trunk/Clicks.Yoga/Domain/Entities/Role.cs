using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class Role : Entity
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }
    }
}