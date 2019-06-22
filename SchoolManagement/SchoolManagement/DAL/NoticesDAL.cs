using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.DAL
{
    public class NoticesDAL
    {
        private SchoolManagementEntities db;

        public NoticesDAL()
        {
            db = new SchoolManagementEntities();
        }

        public IEnumerable<Users> getUser(string search, int role)
        {
            if (search != null)
            {
                return db.Users.Where(s => s.IDRole == role &&
                (s.ID.Contains(search) || s.Name.Contains(search) || s.Classes.ClassName.Contains(search))).OrderBy(s => s.IDClass);
            }
            return db.Users.Where(s => s.IDRole == role).OrderBy(s => s.IDClass);
        }

        public IEnumerable<Users> getUser(string search, int role, int page, int pageSize)
        {
            return getUser(search, role).ToPagedList(page, pageSize);
        }

        public IEnumerable<Classes> getStudentClass(string search, int page, int pageSize)
        {
            if (search == null)
                return db.Classes.OrderBy(c => c.ID).ToPagedList(page, pageSize);
            int _s = 0;
            int.TryParse(search, out _s);
            return db.Classes.Where(c => c.ClassName.Contains(search) || c.Course == _s).OrderBy(c => c.ID).ToPagedList(page, pageSize);
        }

        public IEnumerable<Users> getViewStudentClass(string id, int page, int pageSize)
        {
            return db.Users.Where(c => c.IDClass == id).OrderBy(c => c.Name).ToPagedList(page, pageSize);
        }

        //List all
        public IEnumerable<Subjects> ListSubject()
        {
            return db.Subjects.OrderBy(s => s.ID);
        }

        public IEnumerable<Subjects> ListSubject(int page, int pageSize)
        {
            return ListSubject().ToPagedList(page, pageSize);
        }

        //Search
        public IEnumerable<Subjects> ListSubject(string search)
        {
            return db.Subjects.Where(s => s.ID.Contains(search) || s.SubjectName.Contains(search) || s.Note.Contains(search)).OrderBy(s => s.ID);
        }

        public IEnumerable<Subjects> ListSubject(string search, int page, int pageSize)
        {
            return ListSubject(search).ToPagedList(page, pageSize);
        }

        public IEnumerable<Class_Subjects> ListClass()
        {
            return db.Class_Subjects.OrderBy(c => c.ID);
        }

        public IEnumerable<Class_Subjects> ListClass(int page, int pageSize)
        {
            return ListClass().ToPagedList(page, pageSize);
        }

        public IEnumerable<Class_Subjects> ListClass(string search)
        {
            return db.Class_Subjects.Where(c => c.ID.Contains(search) || c.SubjectID.Contains(search) ||
            c.Subjects.SubjectName.Contains(search) || c.Subjects.Note.Contains(search)).OrderBy(c => c.SubjectID);
        }

        public IEnumerable<Class_Subjects> ListClass(string search, int page, int pageSize)
        {
            return ListClass(search).ToPagedList(page, pageSize);
        }

        public IEnumerable<DivisionClasses> DivClass(string search)
        {
            if (search == null)
                return db.DivisionClasses.OrderBy(u => u.Class_Subjects.SubjectID);
            else
                return db.DivisionClasses.Where(c => c.IDClass.Contains(search) || c.Users.Name.Contains(search) ||
                c.Users.Classes.ClassName.Contains(search) || c.Class_Subjects.SubjectID.Contains(search) ||
                c.Class_Subjects.Subjects.SubjectName.Contains(search)
                ).OrderBy(u => u.Class_Subjects.SubjectID);
        }

        public IEnumerable<DivisionClasses> DivClass(string search, int page, int pageSize)
        {
            return DivClass(search).ToPagedList(page, pageSize);
        }

        public IEnumerable<RegistrationClasses> ViewClass(string idClass)
        {
            return db.RegistrationClasses.Where(u => u.IDClass == idClass).OrderBy(u => u.Users.Name);
        }

        public IEnumerable<RegistrationClasses> ViewClass(string idClass, int page, int pageSize)
        {
            return ViewClass(idClass).ToPagedList(page, pageSize);
        }

        public IEnumerable<DivionProjects> DivProject(string idProject, string search)
        {
            if (search == null)
                return db.DivionProjects.Where(p => p.RegistrationClasses.Class_Subjects.SubjectID == idProject).OrderBy(p => p.IDTeacher);
            else
                return db.DivionProjects.Where(p => p.RegistrationClasses.Class_Subjects.SubjectID == idProject &&
                (p.Users.Name.Contains(search) || p.Users.Classes.ClassName.Contains(search) ||
                p.RegistrationClasses.Users.Name.Contains(search) || p.RegistrationClasses.Users.ID.Contains(search)
                )).OrderBy(p => p.IDTeacher);
        }

        public IEnumerable<DivionProjects> DivProject(string idProject, string search, int page, int pageSize)
        {
            return DivProject(idProject, search).ToPagedList(page, pageSize);
        }

    }
}