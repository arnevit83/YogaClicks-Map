using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Security;
using System.Transactions;
using Clicks.Yoga.Context;
using Clicks.Yoga.Data.Repositories;
using Clicks.Yoga.Data.Search;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;
using Clicks.Yoga.Domain.Services;
using Common;
using Ninject;
using TeacherService = Clicks.Yoga.Domain.Entities.TeacherService;
using VenueService = Clicks.Yoga.Domain.Entities.VenueService;

namespace Clicks.Yoga.Data
{
    public class YogaEntityContext : IEntityContext, IWorkUnit
    {
        public YogaEntityContext(YogaDataContext context)
        {
            TransientEntityDependencies = new List<TransientEntityDependency>();

            Context = context;
            Context.CreatingEntity += Context_CreatingEntity;
            Context.ModifyingEntity += Context_ModifyingEntity;
            Context.DeletingEntity += Context_DeletingEntity;
            Context.EntityCreated += Context_EntityCreated;
            Context.EntityModified += Context_EntityModified;
            Context.EntityDeleted += Context_EntityDeleted;
        }

        private YogaDataContext Context { get; set; }

        private List<TransientEntityDependency> TransientEntityDependencies { get; set; }
        [Inject]
        public ISearchEngine SearchEngine { get; set; }

        [Inject]
        public ISecurityContext SecurityContext { get; set; }
            
        [Inject]
        public IEntityService EntityService { get; set; }

        [Inject]
        public INewsService NewsService { get; set; }

        [Inject]
        public ISearchService SearchService { get; set; }

        [Inject]
        public IImageStore ImageStore { get; set; }

        public IRepository GetRepository(Type entityType)
        {
            return new Repository(Context, entityType);
        }

        public void RegisterTransientEntityDependency<TEntity, TReference>(
            TEntity entity,
            Expression<Func<TEntity, TReference>> expression,
            TReference reference)
            where TEntity : class, IEntity
            where TReference : class, IEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (expression == null )
                throw new ArgumentNullException("expression");
            if (reference == null)
                throw new ArgumentNullException("reference");
            if (!reference.IsTransient)
                throw new ArgumentException("Reference must be transient.", "reference");

            var property = ReflectionFunctions.GetPropertyInfo(expression);

            if (property == null)
                throw new ArgumentException("Expression must be a property.", "expression");

            TransientEntityDependencies.Add(new TransientEntityDependency(entity, property, reference));
        }

        public void RegisterTransientEntityDependency<TEntity, TReference>(
            TEntity entity,
            Expression<Func<TEntity, int?>> expression,
            TReference reference)
            where TEntity : class, IEntity
            where TReference : class, IEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (expression == null)
                throw new ArgumentNullException("expression");
            if (reference == null)
                throw new ArgumentNullException("reference");
            if (!reference.IsTransient)
                throw new ArgumentException("Reference must be transient.", "reference");

            var property = ReflectionFunctions.GetPropertyInfo(expression);

            if (property == null)
                throw new ArgumentException("Expression must be a property.", "expression");

            TransientEntityDependencies.Add(new TransientEntityDependency(entity, property, reference));
        }

        public void Commit()
        {
            var options = new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TransactionManager.MaximumTimeout };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                Context.SaveChanges();

                if (TransientEntityDependencies.Count > 0)
                {
                    foreach (var dependency in TransientEntityDependencies)
                    {
                        dependency.Update();
                    }

                    Context.SaveChanges();

                    TransientEntityDependencies.Clear();
                }

                scope.Complete();
            }
        }

        private void IndexEntity(Entity entity)
        {
            var relationship = entity as IRelationship;

            if (relationship != null)
            {
                foreach (var target in relationship.Targets)
                {
                    IndexEntity(target);
                }
            }

            if (SearchService != null)
            {
                var searchable = entity as ISearchable;

                if (searchable != null) SearchService.Index(searchable);

                var review = entity as Review;

                if (review != null) SearchService.Index(review);
            }

            var handle = entity as IEntityHandle;

            if (handle != null && !(handle is EntityHandle) && EntityService != null)
            {
                if (handle.Owner != null)
                {
                    EntityService.EnsureEntityHandle(handle);
                }

                EntityService.UpdateEntityHandle(handle);
            }

            if (SecurityContext != null)
            {
                var actor = entity as IActor;

                if (actor != null && SecurityContext.IsOwner(actor))
                {
                    SecurityContext.RefreshActors();
                }

                var profile = entity as Profile;

                if (profile != null && SecurityContext.IsOwner(profile))
                {
                    SecurityContext.RefreshProfile();
                }
            }
        }

        public void IndexEntityElastic(Entity entity)
        {
            if (SearchService != null)
            {
                var searchable = entity as ISearchable;

                if (searchable != null) SearchEngine.Index(searchable);

                var review = entity as Review;

                if (review != null) SearchEngine.Index(review);
            }
        }

        public void DeleteEntityElastic(Entity entity)
        {
            if (SearchService != null)
            {
                var searchable = entity as ISearchable;

                if (searchable != null) SearchEngine.Delete(searchable);
            }
        }

        private void UpdateNews(Entity entity, bool deleted)
        {
            var principal = entity as PrincipalEntity;

            deleted = deleted || (principal != null && !principal.Active);

            if (deleted)
            {
                NewsService.DeleteStories(entity.Id, entity.GetEntityTypeName());
            }

            var link = entity as INewsLink;

            if (link == null) return;

            if (!link.NewsRequired || deleted)
            {
                NewsService.Unsubscribe(link.NewsSubscriber, link.NewsSubject);
            }
            else if (link.NewsSubject != null && link.NewsSubscriber != null)
            {
                NewsService.Subscribe(link.NewsSubscriber, link.NewsSubject);
            }
        }

        private void Context_CreatingEntity(object sender, Entity entity)
        {
            var now = DateTime.Now;

            entity.CreationTime = now;
            entity.ModificationTime = now;

            IndexEntity(entity);
            UpdateNews(entity, false);
        }

        private void Context_ModifyingEntity(object sender, Entity entity)
        {
            entity.ModificationTime = DateTime.Now;

            IndexEntity(entity);
            UpdateNews(entity, false);
        }
        void Context_EntityModified(object sender, Entity entity)
        {
            IndexEntityElastic(entity);
        }

        private void Context_DeletingEntity(object sender, Entity entity)
        {
            if (entity is ISearchable) SearchService.Remove(entity as ISearchable);

            UpdateNews(entity, true);
        }
        void Context_EntityDeleted(object sender, Entity entity)
        {
            DeleteEntityElastic(entity);
        }

        private void Context_EntityCreated(object sender, Entity entity)
        {
            var image = entity as Image;

            if (image != null && image.Source != null)
            {
                ImageStore.SaveImage(image);
            }

            IndexEntityElastic(entity);
        }

        private class TransientEntityDependency
        {
            public TransientEntityDependency(IEntity entity, PropertyInfo property, IEntity reference)
            {
                Entity = entity;
                Property = property;
                Reference = reference;
            }

            private IEntity Entity { get; set; }

            private PropertyInfo Property { get; set; } 

            private IEntity Reference { get; set; }

            public void Update()
            {
                if (Reference.IsTransient)
                    throw new InvalidOperationException("Entity should not be transient.");

                if (Property.PropertyType == typeof(int) || Property.PropertyType == typeof(int?))
                {
                    Property.SetValue(Entity, Reference.Id);
                }
                else
                {
                    Property.SetValue(Entity, Reference);
                }
            }
        }

        [Inject]
        public IRepository<AbilityLevel> AbilityLevels { get; private set; }

        [Inject]
        public IRepository<AccreditationBody> AccreditationBodies { get; private set; }

        [Inject]
        public IRepository<Activity> Activities { get; private set; }

        [Inject]
        public IRepository<ActivityAttendee> ActivityAttendees { get; private set; }

        [Inject]
        public IRepository<ActivityRepeatFrequency> ActivityRepeatFrequencies { get; private set; }

        [Inject]
        public IRepository<ActivityTeacher> ActivityTeachers { get; private set; }

        [Inject]
        public IRepository<ActivityType> ActivityTypes { get; private set; }

        [Inject]
        public IRepository<Comment> Comments { get; private set; }

        [Inject]
        public IRepository<Country> Countries { get; private set; }

        [Inject]
        public IRepository<EntityHandle> EntityHandles { get; private set; }

        [Inject]
        public IRepository<EntityHandleInvite> EntityHandleInvites { get; private set; }

        [Inject]
        public IEntityTypeRepository EntityTypes { get; private set; }

        [Inject]
        public IRepository<Fan> Fans { get; private set; }

        [Inject]
        public IRepository<FederatedLogin> FederatedLogins { get; private set; }

        [Inject]
        public IRepository<FederatedLoginProvider> FederatedLoginProviders { get; private set; }

        [Inject]
        public IRepository<Friendship> Friendships { get; private set; }

        [Inject]
        public IRepository<Gallery> Galleries { get; private set; }

        [Inject]
        public IRepository<GalleryAssociation> GalleryAssociations { get; private set; }

        [Inject]
        public IRepository<GalleryEntry> GalleryEntries { get; private set; }

        [Inject]
        public IRepository<Gender> Genders { get; private set; }

        [Inject]
        public IRepository<GlossaryEntry> GlossaryEntries { get; private set; }

        [Inject]
        public IRepository<Group> Groups { get; private set; }

        [Inject]
        public IRepository<GroupMember> GroupMembers { get; private set; }

        [Inject]
        public IRepository<Image> Images { get; private set; }

        [Inject]
        public IRepository<ImageType> ImageTypes { get; private set; }

        [Inject]
        public IRepository<Invitation> Invitations { get; private set; }

        [Inject]
        public IRepository<MediaResource> MediaResources { get; private set; }

        [Inject]
        public IRepository<MediaResourceType> MediaResourceTypes { get; private set; }

        [Inject]
        public IRepository<NewsStory> NewsStories { get; private set; }

        [Inject]
        public IRepository<NewsStoryType> NewsStoryTypes { get; private set; }

        [Inject]
        public IRepository<NewsSubscription> NewsSubscriptions { get; private set; }

        [Inject]
        public IRepository<Notification> Notifications { get; private set; }

        [Inject]
        public IRepository<NotificationCategory> NotificationCategories { get; private set; }

        [Inject]
        public IRepository<NotificationDelivery> NotificationDeliveries { get; private set; }

        [Inject]
        public IRepository<NotificationPreferences> NotificationPreferences { get; private set; }

        [Inject]
        public IRepository<NotificationType> NotificationTypes { get; private set; }

        [Inject]
        public IRepository<NotificationCategoryPreferences> NotificationCategoryPreferences { get; private set; }

        [Inject]
        public IRepository<PasswordLogin> PasswordLogins { get; private set; }

        [Inject]
        public IRepository<PasswordResetAction> PasswordResetActions { get; private set; }

        [Inject]
        public IRepository<PasswordResetRequest> PasswordResetRequests { get; private set; }

        [Inject]
        public IProfileRepository Profiles { get; private set; }

        [Inject]
        public IRepository<Pose> Poses { get; private set; }

        [Inject]
        public IRepository<PoseCategory> PoseCategories { get; private set; }

        [Inject]
        public IRepository<PrivateConversation> PrivateConversations { get; private set; }

        [Inject]
        public IRepository<PrivateMessage> PrivateMessages { get; private set; }

        [Inject]
        public IRepository<PrivateMessageDelivery> PrivateMessageDeliveries { get; private set; }

        [Inject]
        public IRepository<ProfileAnswer> ProfileAnswers { get; private set; }

        [Inject]
        public IRepository<ProfileQuestion> ProfileQuestions { get; private set; }

        [Inject]
        public IRepository<Quote> Quotes { get; private set; }

        [Inject]
        public IRepository<Request> Requests { get; private set; }

        [Inject]
        public IRepository<RequestType> RequestTypes { get; private set; }

        [Inject]
        public IRepository<Review> Reviews { get; private set; }

        [Inject]
        public IRepository<ReviewExperience> ReviewExperiences { get; private set; }

        [Inject]
        public IRepository<ReviewFeedbackResponse> ReviewFeedbackResponses { get; private set; }

        [Inject]
        public IRepository<ReviewRatingSubject> ReviewRatingSubjects { get; private set; }

        [Inject]
        public IRepository<SearchRecord> SearchRecords { get; private set; }

        [Inject]
        public IStyleRepository Styles { get; private set; }

        [Inject]
        public IStyleTraitRepository StyleTraits { get; private set; }

        [Inject]
        public IRepository<StyleOrganisation> StyleOrganisations { get; private set; }

        [Inject]
        public IRepository<StyleTraitGroup> StyleTraitGroups { get; private set; }

        [Inject]
        public IRepository<Teacher> Teachers { get; private set; }

        [Inject]
        public IRepository<TeacherService> TeacherServices { get; private set; }

        [Inject]
        public IRepository<TeacherTrainingOrganisation> TeacherTrainingOrganisations { get; private set; }

        [Inject]
        public IRepository<TeacherTrainingCourse> TeacherTrainingCourses { get; private set; }

        [Inject]
        public IRepository<TeacherTrainingCourseTeacher> TeacherTrainingCourseTeachers { get; private set; }

        [Inject]
        public IRepository<TeacherTrainingCourseVenue> TeacherTrainingCourseVenues { get; private set; }

        [Inject]
        public IRepository<Timescale> Timescales { get; private set; }

        [Inject]
        public IRepository<UserActivationRequest> UserActivationRequests { get; private set; }

        [Inject]
        public IRepository<UserEmailAddressChangeRequest> UserEmailAddressChangeRequests { get; private set; }

        [Inject]
        public IRepository<UserEmailAddressChangeAction> UserEmailAddressChangeActions { get; private set; }

        [Inject]
        public IRepository<User> Users { get; private set; }

        [Inject]
        public IRepository<Venue> Venues { get; private set; }

        [Inject]
        public IRepository<VenueAmenity> VenueAmenities { get; private set; }

        [Inject]
        public IRepository<VenueService> VenueServices { get; private set; }

        [Inject]
        public IRepository<VenueType> VenueTypes { get; private set; }

        [Inject]
        public IRepository<Video> Videos { get; private set; }

        [Inject]
        public IRepository<VideoAssociation> VideoAssociations { get; private set; }

        [Inject]
        public IRepository<VideoType> VideoTypes { get; private set; }

        [Inject]
        public IRepository<Wall> Walls { get; private set; }

        [Inject]
        public IRepository<WallPost> WallPosts { get; private set; }

        [Inject]
        public IRepository<Website> Websites { get; private set; }

        [Inject]
        public IRepository<Qualification> Qualifications { get; private set; }

        [Inject]
        public IRepository<TeacherPlacement> TeacherPlacements { get; private set; }

        [Inject]
        public IRepository<Author> Authors { get; private set; }

        [Inject]
        public IRepository<Condition> Conditions { get; private set; }

        [Inject]
        public IRepository<Study> Studies { get; private set; }

        [Inject]
        public IRepository<UserStory> UserStories { get; private set; }

        [Inject]
        public IRepository<TherapyType> TherapyTypes { get; private set; }

        [Inject]
        public IRepository<WhatTheScienceSays> WhatTheScienceSayses { get; private set; }

        [Inject]
        public IRepository<Follow> Followers { get; private set; }
    }
}