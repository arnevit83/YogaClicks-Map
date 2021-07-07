using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class NewsStoryModel : EntityModel<NewsStory>
    {
        public NewsStoryModel(NewsStory entity) : base(entity) { }

        public NewsStoryTypeModel Type { get; private set; }

        public EntityHandleModel Subject { get; private set; }

        public EntityHandleModel Actor { get; private set; }

        public EntityHandleModel Target { get; private set; }

        public EntityHandleModel Context { get; private set; }

        public int EntityId { get; private set; }

        public virtual EntityTypeModel EntityType { get; private set; }

        public string Text { get; private set; }

        public IList<MediaResourceModel> Resources { get; private set; }

        public NewsStoryModel ShareOriginal { get; private set; }

        public bool IsShared { get; private set; }

        public string Time
        {
            get {
                var time = DateTime.Now - CreationTime;

                if (time < new TimeSpan(0, 0, 2, 0))
                {
                    return "Just now";
                }
                else if (time < new TimeSpan(0, 1, 0, 0))
                {
                    return string.Format("{0} minutes ago", Convert.ToInt32(time.TotalMinutes));
                }
                else if (time < new TimeSpan(0, 2, 0, 0))
                {
                    return "1 hour ago";
                }
                else if (time < new TimeSpan(0, 12, 0, 0))
                {
                    return string.Format("{0} hours ago", Convert.ToInt32(time.TotalHours));
                }
                else if (CreationTime.Date == DateTime.Now.Date)
                {
                    return "Today at " + CreationTime.ToShortTimeString();
                }
                else if (CreationTime.Date == DateTime.Now.Date.AddDays(-1))
                {
                    return "Yesterday at " + CreationTime.ToShortTimeString();
                }
                else if (CreationTime.Date >= DateTime.Now.Date.AddDays(-7))
                {
                    return CreationTime.Date.ToString("dddd") + " at " + CreationTime.ToShortTimeString();
                }
                else
                {
                    return CreationTime.ToString("D") + " at " + CreationTime.ToShortTimeString();
                }
            }
        }

        public string ViewPath
        {
            get { return string.Format("~/Views/News/Stories/{0}.cshtml", Type.Tag); }
        }

        public override void Populate(NewsStory entity)
        {
            Type = new NewsStoryTypeModel(entity.Type);
            Subject = new EntityHandleModel(entity.Subject);
            Actor = new EntityHandleModel(entity.Actor);
            Target = new EntityHandleModel(entity.Target);
            EntityId = entity.EntityId.HasValue ? entity.EntityId.Value : 0;
            EntityType = new EntityTypeModel(entity.EntityType);
            Text = entity.Text;
            Resources = entity.Resources.Select(r => new MediaResourceModel(r)).ToList();
            ShareOriginal = new NewsStoryModel(entity.ShareOriginal);
            ShareOriginal.IsShared = true;
        }
    }
}