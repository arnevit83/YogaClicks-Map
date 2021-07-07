using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class TeacherTrainingService : ServiceBase, ITeacherTrainingService
    {
        public TeacherTrainingService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            INewsService newsService,
            ISearchService searchService,
            IRequestService requestService)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            NewsService = newsService;
            SearchService = searchService;
            RequestService = requestService;
        }

        private IEntityService EntityService { get; set; }

        private INewsService NewsService { get; set; }

        private ISearchService SearchService { get; set; }

        private IRequestService RequestService { get; set; }

        public IList<TeacherTrainingCourse> LuckyDip()
        {
            return EntityContext.TeacherTrainingCourses.OrderBy(c => Guid.NewGuid()).Take(5).ToList();
        }

        public IList<TeacherTrainingCourse> SearchCourses(TeacherTrainingCourseSearchCriteria criteria)
        {
            var query = EntityContext.TeacherTrainingCourses.Where(e => e.Organisation.Active);

            query = query.Where(e => e.Organisation.Id == criteria.OrganisationId);

            if (criteria.Completed.HasValue)
            {
                query = criteria.Completed.Value ? query.Where(e => e.FinishDate <= DateTime.Now) : query.Where(e => e.FinishDate > DateTime.Now);
            }

            return query.OrderBy(e => e.Name).ToList();
        }

        public IList<TeacherTrainingOrganisation> GetOrganisations()
        {
            return EntityContext.TeacherTrainingOrganisations.OrderBy(o => o.Name).ToList();
        }

        public IList<TeacherTrainingOrganisation> GetOrganisationsById(List<int> ids)
        {
            return EntityContext.TeacherTrainingOrganisations.Where(x => ids.Contains(x.Id)).ToList();
        }

        public TeacherTrainingOrganisation GetOrganisation(int id)
        {
            return EntityContext.TeacherTrainingOrganisations.Get(e => e.Id == id);
        }
        public TeacherTrainingOrganisation GetOrganisationForUser(int id)
        {
            return EntityContext.TeacherTrainingOrganisations.Get(e => e.Active && e.Owner.Id == id);
        }

        public TeacherTrainingOrganisation GetOrganisationForDisplay(int id)
        {
            var organisation = GetOrganisation(id);

            if (organisation == null) throw new UnknownEntityException();

            return organisation;
        }

        public TeacherTrainingOrganisation GetOrganisationForEditing(int id)
        {
            var organisation = GetOrganisationForDisplay(id);

            if (!SecurityContext.CanUpdate(organisation)) throw new PermissionDeniedException();

            return organisation;
        }

        public TeacherTrainingCourse GetCourse(int id)
        {
            return EntityContext.TeacherTrainingCourses.Get(id);
        }

        public TeacherTrainingCourse GetCourseForDisplay(int id)
        {
            var course = GetCourse(id);

            if (course == null) throw new UnknownEntityException();

            return course;
        }

        public TeacherTrainingCourse GetCourseForEditing(int id)
        {
            var course = GetCourseForDisplay(id);

            if (!SecurityContext.CanUpdate(course)) throw new PermissionDeniedException();

            return course;
        }

        public void AddOrganisation(TeacherTrainingOrganisation organisation)
        {
            if (organisation == null)
                throw new ArgumentNullException("organisation");
            if (!SecurityContext.CanUpdate(organisation.Owner))
                throw new PermissionDeniedException();

            EntityContext.TeacherTrainingOrganisations.Add(organisation);

            NewsService.PostAction(NewsStoryType.FriendAddedEntity, SecurityContext.CurrentProfile, organisation);
        }

        public void AddCourse(TeacherTrainingCourse course)
        {
            if (course == null)
                throw new ArgumentNullException("course");
            if (!SecurityContext.CanUpdate(course.Organisation))
                throw new PermissionDeniedException();

            EntityContext.TeacherTrainingCourses.Add(course);

            NewsService.PostAction(NewsStoryType.FriendAddedEntity, SecurityContext.CurrentProfile, course);
        }

        public void AddCourseTeacher(TeacherTrainingCourse course, Teacher teacher)
        {
            if (course == null)
                throw new ArgumentNullException("course");
            if (teacher == null)
                throw new ArgumentNullException("teacher");

            if (course.Teachers.Any(e => e.Teacher.Id == teacher.Id)) return;

            var confirmed = teacher.Owner == null || teacher.Owner.Id == course.Owner.Id;

            var association = new TeacherTrainingCourseTeacher
            {
                Course = course,
                Teacher = teacher,
                Confirmed = confirmed
            };

            course.Teachers.Add(association);

            if (!confirmed)
            {
                RequestService.Request("TeacherTrainingCourseTeacherAdded", teacher.Owner, SecurityContext.CurrentActor, association.Course, teacher, association);
            }
        }

        public void RemoveCourseTeacher(TeacherTrainingCourse course, Teacher teacher)
        {
            if (course == null)
                throw new ArgumentNullException("course");
            if (teacher == null)
                throw new ArgumentNullException("teacher");

            var association = course.Teachers.FirstOrDefault(e => e.Teacher.Id == teacher.Id);

            if (association == null) return;

            EntityContext.TeacherTrainingCourseTeachers.Remove(association);
            course.Teachers.Remove(association);
        }

        public void AddCourseVenue(TeacherTrainingCourse course, Venue venue)
        {
            if (course == null)
                throw new ArgumentNullException("course");
            if (venue == null)
                throw new ArgumentNullException("venue");

            if (course.Venues.Any(e => e.Venue.Id == venue.Id)) return;

            var confirmed = venue.Owner == null || venue.Owner.Id == course.Owner.Id;

            var association = new TeacherTrainingCourseVenue
            {
                Course = course,
                Venue = venue,
                Confirmed = confirmed
            };

            course.Venues.Add(association);

            if (!confirmed)
            {
                RequestService.Request("TeacherTrainingCourseVenueAdded", venue.Owner, SecurityContext.CurrentActor, association.Course, venue, association);
            }
        }

        public void RemoveCourseVenue(TeacherTrainingCourse course, Venue venue)
        {
            if (course == null)
                throw new ArgumentNullException("course");
            if (venue == null)
                throw new ArgumentNullException("venue");

            var association = course.Venues.FirstOrDefault(e => e.Venue.Id == venue.Id);

            if (association == null) return;

            EntityContext.TeacherTrainingCourseVenues.Remove(association);
            course.Venues.Remove(association);
        }

        public void DeleteCourse(int id)
        {
            var course = EntityContext.TeacherTrainingCourses.Get(id);

            if (course == null)
                throw new ArgumentOutOfRangeException("id");
            if (!SecurityContext.CanDelete(course))
                throw new PermissionDeniedException();

            course.Active = false;
            EntityContext.TeacherTrainingCourses.Remove(course);
        }
    }
}