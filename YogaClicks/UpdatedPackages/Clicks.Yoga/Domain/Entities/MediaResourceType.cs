using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class MediaResourceType : Entity
    {
        public string Tag { get; set; }

        public virtual EntityType EntityType { get; set; }

        public string UriPattern { get; set; }

        public int Priority { get; set; }

        public string ScannerTypeName { get; set; }

        public int? ExpirySeconds { get; set; }

        public Type GetScannerType()
        {
            return Type.GetType(ScannerTypeName);
        }
    }
}