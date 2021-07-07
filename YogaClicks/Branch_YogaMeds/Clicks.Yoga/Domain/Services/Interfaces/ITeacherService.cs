using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface ITeacherService
    {
        IList<Teacher> LucyDip();
        bool CurrentUserHasTeacher();
        Teacher GetTeacherForCurrentUser();
        IList<Entities.TeacherService> GetTeacherServices();
        Entities.TeacherService GetTeacherService(int id);
        Teacher GetTeacher(int id);
        List<Teacher> GetTeachers(List<int> ids);
        Teacher GetTeacherForDisplay(int id);
        Teacher GetTeacherForEditing(int id);
        Teacher CreateTeacher(User owner);
        Teacher CreateStubbed(string name, Location location);
        void AddTeacher(Teacher teacher);
        void AddPlacement(Teacher teacher, Venue venue);
        void RemovePlacement(TeacherPlacement placement);
        Teacher GetTeacherForUser(int id);
    }
}