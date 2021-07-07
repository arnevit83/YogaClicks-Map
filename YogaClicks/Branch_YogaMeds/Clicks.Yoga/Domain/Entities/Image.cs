using System;
using System.IO;
using System.Reflection;

namespace Clicks.Yoga.Domain.Entities
{
    public class Image : PrincipalEntity
    {
        public Image()
        {
            Guid = Guid.NewGuid();
        }

        public Image(Stream source, ImageType type) : this()
        {
            Source = source;
            Type = type;
        }

        public Image(PropertyInfo property, Stream source, ImageType type, User owner) : this(source, type)
        {
            Path = string.Format("{0}.{1}", property.DeclaringType.Name, property.Name);
            Source = source;
            Type = type;
            Owner = owner;
        }

        public string Path { get; set; }

        public Guid Guid { get; set; }

        public virtual ImageType Type { get; set; }

        public bool Temporary { get; set; }

        public Stream Source { get; set; }

        public string Name
        {
            get { return string.Format("{0}.{1}", Guid.ToString(), Type.Extension); }
        }

        public string Uri
        {
            get { return string.Format("{0}/{1}", Path, Name); }
        }
    }
}