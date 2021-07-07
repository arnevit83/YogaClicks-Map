using System.Collections.Generic;
using System.Linq;

namespace Clicks.Yoga.Domain.Entities
{
    public class PoseUserImages : Entity
    {
        public virtual Pose Pose { get; set; }


        public virtual Image Image { get; set; }


      
    }
}