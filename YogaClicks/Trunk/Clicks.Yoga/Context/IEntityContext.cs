using System;
using System.Linq.Expressions;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;

namespace Clicks.Yoga.Context
{
    public interface IEntityContext
    {
        IRepository<AbilityLevel> AbilityLevels { get; }
        IRepository<AccreditationBody> AccreditationBodies { get; }
        IRepository<Activity> Activities { get; }
        IRepository<ActivityAttendee> ActivityAttendees { get; }
        IRepository<ActivityRepeatFrequency> ActivityRepeatFrequencies { get; }
        IRepository<ActivityTeacher> ActivityTeachers { get; }
        IRepository<ActivityType> ActivityTypes { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Country> Countries { get; }
        IRepository<EntityHandle> EntityHandles { get; }
        IRepository<EntityHandleInvite> EntityHandleInvites { get; }
        IEntityTypeRepository EntityTypes { get; }
        IRepository<Fan> Fans { get; }
        IRepository<FederatedLogin> FederatedLogins { get; }
        IRepository<FederatedLoginProvider> FederatedLoginProviders { get; }
        IRepository<Friendship> Friendships { get; }
        IRepository<Gallery> Galleries { get; }
        IRepository<GalleryAssociation> GalleryAssociations { get; }
        IRepository<GalleryEntry> GalleryEntries { get; }
        IRepository<Gender> Genders { get; }
        IRepository<GlossaryEntry> GlossaryEntries { get; }
        IRepository<Group> Groups { get; }
        IRepository<GroupMember> GroupMembers { get; }
        IRepository<Image> Images { get; }
        IRepository<ImageType> ImageTypes { get; }
        IRepository<Invitation> Invitations { get; }
        IRepository<MediaResource> MediaResources { get; }
        IRepository<MediaResourceType> MediaResourceTypes { get; }
        IRepository<NewsStory> NewsStories { get; }
        IRepository<NewsStoryType> NewsStoryTypes { get; }
        IRepository<NewsSubscription> NewsSubscriptions { get; }
        IRepository<Notification> Notifications { get; }
        IRepository<NotificationCategory> NotificationCategories { get; }
        IRepository<NotificationDelivery> NotificationDeliveries { get; }
        IRepository<NotificationPreferences> NotificationPreferences { get; }
        IRepository<NotificationCategoryPreferences> NotificationCategoryPreferences { get; }
        IRepository<NotificationType> NotificationTypes { get; }
        IRepository<PasswordLogin> PasswordLogins { get; }
        IRepository<PasswordResetAction> PasswordResetActions { get; }
        IRepository<PasswordResetRequest> PasswordResetRequests { get; }
        IProfileRepository Profiles { get; }
        IRepository<Pose> Poses { get; }
        IRepository<PoseCategory> PoseCategories { get; }
        IRepository<PrivateConversation> PrivateConversations { get; }
        IRepository<PrivateMessage> PrivateMessages { get; }
        IRepository<PrivateMessageDelivery> PrivateMessageDeliveries { get; }
        IRepository<ProfileAnswer> ProfileAnswers { get; }
        IRepository<ProfileQuestion> ProfileQuestions { get; }
        IRepository<Quote> Quotes { get; }
        IRepository<Request> Requests { get; }
        IRepository<RequestType> RequestTypes { get; }
        IRepository<Review> Reviews { get; }
        IRepository<ReviewExperience> ReviewExperiences { get; }
        IRepository<ReviewFeedbackResponse> ReviewFeedbackResponses { get; }
        IRepository<ReviewRatingSubject> ReviewRatingSubjects { get; }
        IRepository<SearchRecord> SearchRecords { get; }
        IStyleRepository Styles { get; }
        IStyleTraitRepository StyleTraits { get; }
        IRepository<StyleOrganisation> StyleOrganisations { get; }
        IRepository<StyleTraitGroup> StyleTraitGroups { get; }
        IRepository<Teacher> Teachers { get; }
        IRepository<TeacherPlacement> TeacherPlacements { get; }
        IRepository<TeacherService> TeacherServices { get; }
        IRepository<TeacherTrainingOrganisation> TeacherTrainingOrganisations { get; }
        IRepository<TeacherTrainingCourse> TeacherTrainingCourses { get; }
        IRepository<TeacherTrainingCourseTeacher> TeacherTrainingCourseTeachers { get; }
        IRepository<TeacherTrainingCourseVenue> TeacherTrainingCourseVenues { get; }
        IRepository<Timescale> Timescales { get; }
        IRepository<UserActivationRequest> UserActivationRequests { get; }
        IRepository<UserEmailAddressChangeRequest> UserEmailAddressChangeRequests { get; }
        IRepository<UserEmailAddressChangeAction> UserEmailAddressChangeActions { get; }
        IRepository<User> Users { get; }
        IRepository<Venue> Venues { get; }
        IRepository<VenueAmenity> VenueAmenities { get; }
        IRepository<VenueService> VenueServices { get; }
        IRepository<VenueType> VenueTypes { get; }
        IRepository<Video> Videos { get; }
        IRepository<VideoAssociation> VideoAssociations { get; }
        IRepository<VideoType> VideoTypes { get; }
        IRepository<Wall> Walls { get; }
        IRepository<WallPost> WallPosts { get; }
        IRepository<Website> Websites { get; }
        IRepository<Author> Authors { get; }
        IRepository<Condition> Conditions { get; }
        IRepository<Study> Studies { get; }
        IRepository<TherapyType> TherapyTypes { get; }
        IRepository<UserStory> UserStories { get; }
        IRepository<WhatTheScienceSays> WhatTheScienceSayses { get; }
        IRepository<Follow> Followers { get; }
        IRepository<Qualification> Qualifications { get; }

        IRepository GetRepository(Type entityType);

        void RegisterTransientEntityDependency<TEntity, TReference>(
            TEntity entity,
            Expression<Func<TEntity, TReference>> expression,
            TReference reference)
            where TEntity : class, IEntity
            where TReference : class, IEntity;

        void RegisterTransientEntityDependency<TEntity, TReference>(
            TEntity entity,
            Expression<Func<TEntity, int?>> expression,
            TReference reference)
            where TEntity : class, IEntity
            where TReference : class, IEntity;
    }
}