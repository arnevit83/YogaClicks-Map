using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clicks.Yoga.Domain.Entities
{
    public class ImageCrop
    {
        //Used for cropping down an image, if the user has cropped there will be an W and H
        public decimal XCoord { get; set; }
        public decimal YCoord { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        
        public bool ShouldCrop
        {
            get { return (Width > 0 && Height > 0); }
        }
    }
}
