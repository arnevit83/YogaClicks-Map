using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.MailingLists;

namespace Clicks.Yoga.Domain.Services
{
    public class TeacherService : ServiceBase, ITeacherService
    {
        public TeacherService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            IImageService imageService,
            ISearchService searchService,
            IRequestService requestService,
            IMailingListProvider mailingListProvider)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            ImageService = imageService;
            SearchService = searchService;
            RequestService = requestService;
            MailingListProvider = mailingListProvider;
        }

        private IEntityService EntityService { get; set; }

        private IImageService ImageService { get; set; }

        private ISearchService SearchService { get; set; }

        private IRequestService RequestService { get; set; }

        private IMailingListProvider MailingListProvider { get; set; }

        public IList<Teacher> LucyDip()
        {
            return EntityContext.Teachers.OrderBy(t => Guid.NewGuid()).Take(5).ToList();
        }

        public bool CurrentUserHasTeacher()
        {
            if (SecurityContext.CurrentUser == null)
                return false;

            return EntityContext.Teachers.Any(GetCurrentUserExpression());
        }

        public Teacher GetTeacherForCurrentUser()
        {
            if (SecurityContext.CurrentUser == null)
                return null;

            return EntityContext.Teachers.FirstOrDefault(GetCurrentUserExpression());
        }

        private Expression<Func<Teacher, bool>> GetCurrentUserExpression()
        {
            return e => e.Owner.Id == SecurityContext.CurrentUser.Id && e.Active;
        }

        public IList<Entities.TeacherService> GetTeacherServices()
        {
            return EntityContext.TeacherServices.OrderBy(e => e.Name).ToList();
        }

        public Entities.TeacherService GetTeacherService(int id)
        {
            return EntityContext.TeacherServices.Get(id);
        }

        public Teacher GetTeacher(int id)
        {
            return EntityContext.Teachers.Get(e => e.Id == id);
        }

        public Teacher GetTeacherString(string name)
        {
            return EntityContext.Teachers.Get(e => e.Name.Replace(" ", string.Empty) == name);
        }

        public List<Teacher> GetTeachers(List<int> ids) 
        {
            return EntityContext.Teachers
                .Where(x => ids.Contains(x.Id))
                .ToList();
        }

        public Teacher GetTeacherForDisplay(int id)
        {
            var teacher = EntityContext.Teachers.Get(id);
            if (teacher == null) throw new UnknownEntityException();
            return teacher;
        }

        public Teacher GetTeacherForDisplayString(string name)
        {
            var teacher = EntityContext.Teachers.Get(e => e.Name.Replace(" ", string.Empty) == name);
            if (teacher == null) throw new UnknownEntityException();
            return teacher;
        }

        public Teacher GetTeacherForEditing(int id)
        {
            var teacher = GetTeacherForDisplay(id);
            if (!SecurityContext.CanUpdate(teacher)) throw new PermissionDeniedException();
            return teacher;
        }

        public Teacher CreateTeacher(User owner)
        {
            var teacher = new Teacher { Owner = owner };
            AddTeacher(teacher);
            return teacher;
        }

        public Teacher CreateStubbed(string name, Location location)
        {
            var teacher = new Teacher
            {
                Name = name,
                Location = location,
                Stubbed = true
            };

            EntityContext.Teachers.Add(teacher);

            return teacher;
        }
        public Teacher CreateStubbed(string name, Location location, string email)
        {
            var teacher = new Teacher
            {
                Name = name,
                Location = location,
                Stubbed = true,
                EmailAddress = email
            };

            EntityContext.Teachers.Add(teacher);

            return teacher;
        }

        public void AddTeacher(Teacher teacher)
        {
            EntityContext.Teachers.Add(teacher);

            if (teacher.Owner != null)
            {
                MailingListProvider.AddToGroup(MailingList.Newsletter, teacher.Owner.EmailAddress, "Type", "Teacher");
            }
        }

        public void AddPlacement(Teacher teacher, Venue venue)
        {
            if (venue == null)
                throw new ArgumentNullException("venue");
            if (teacher == null)
                throw new ArgumentNullException("teacher");

            var placement = new TeacherPlacement
            {
                Teacher = teacher,
                Venue = venue,
                Confirmed = venue.Owner == null || teacher.Owner == null || venue.Owner.Id == teacher.Owner.Id
            };

            teacher.Placements.Add(placement);

            if (venue.Owner != null && teacher.Owner != null && venue.Owner.Id != teacher.Owner.Id)
            {
                RequestService.Request("TeacherPlacementAdded", venue.Owner, SecurityContext.CurrentActor, teacher, venue, placement);
            }
        }

        public void RemovePlacement(TeacherPlacement placement)
        {
            if (placement == null)
                throw new ArgumentNullException("placement");

            EntityContext.TeacherPlacements.Remove(placement);
        }

        public Teacher GetTeacherForUser(int id)
        {
            return EntityContext.Teachers.Get(e => e.Active && e.Owner.Id == id);
        }
    }
}