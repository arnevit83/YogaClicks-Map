using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface ITeacherTrainingService
    {
        IList<TeacherTrainingCourse> LuckyDip();
        IList<TeacherTrainingCourse> SearchCourses(TeacherTrainingCourseSearchCriteria criteria);
        IList<TeacherTrainingOrganisation> GetOrganisations();
        IList<TeacherTrainingOrganisation> GetOrganisationsById(List<int> ids);
        TeacherTrainingOrganisation GetOrganisation(int id);
        TeacherTrainingOrganisation GetOrganisationForUser(int id);
        TeacherTrainingOrganisation GetOrganisationForDisplay(int id);
        TeacherTrainingOrganisation GetOrganisationForEditing(int id);
        TeacherTrainingCourse GetCourse(int id);
        TeacherTrainingCourse GetCourseForDisplay(int id);
        TeacherTrainingCourse GetCourseForEditing(int id);
        void AddOrganisation(TeacherTrainingOrganisation organisation);
        void AddCourse(TeacherTrainingCourse course);

        void AddFaculty(TeacherTrainingOrganisation tto, Teacher teacher);

        void AddCourseTeacher(TeacherTrainingCourse course, Teacher teacher);
        void RemoveCourseTeacher(TeacherTrainingCourse course, Teacher teacher);
        void AddCourseVenue(TeacherTrainingCourse course, Venue venue);
        void RemoveCourseVenue(TeacherTrainingCourse course, Venue venue);
        void DeleteCourse(int id);
        void RemoveFaculty(TeacherTrainingOrganisation tto, Teacher teacher);
    }
}