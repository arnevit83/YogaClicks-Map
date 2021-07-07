using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IVideoService
    {
        IList<AbilityLevel> GetAbilityLevels();
        IList<Video> GetAssociatedVideos(int entityId, string entityTypeName);
        AbilityLevel GetAbilityLevel(int id);
        Video GetVideo(int id);
        Video CreateVideo(string url);
        Video CreateVideo(string url, int entityId, string entityTypeName);
        void AssociateVideo(Video video, int handleId);
        void DisassociateVideo(Video video, int handleId);
        void DeleteVideo(int id);
    }
}