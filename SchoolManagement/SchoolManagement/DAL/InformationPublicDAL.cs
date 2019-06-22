using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;
using PagedList;

namespace SchoolManagement.DAL
{
    public class InformationPublicDAL
    {
        private SchoolManagementEntities db;

        public InformationPublicDAL()
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

        public IEnumerable<Users> getUser(string search, int role, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;
            var ListUsers = getUser(search, role).ToPagedList(page.Value, pageSize);
            return ListUsers;
        }

        public IEnumerable<Classes> getStudentClass(string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            if (search == null)
                return db.Classes.OrderBy(c => c.ID).ToPagedList(page.Value, pageSize);
            int _s = 0;
            int.TryParse(search, out _s);
            return db.Classes.Where(c => c.ClassName.Contains(search) || c.Course == _s).OrderBy(c => c.ID).ToPagedList(page.Value, pageSize);
        }

        public IEnumerable<Users> getViewStudentClass(string id, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;
            return db.Users.Where(c => c.IDClass == id).OrderBy(c => c.Name).ToPagedList(page.Value, pageSize);
        }
       
        public IEnumerable<Subjects> ListSubject(string search)
        {
            if (search == null)
                return db.Subjects.ToList();

            return db.Subjects.Where(s => s.ID.Contains(search) || s.SubjectName.Contains(search)).ToList();
        }

        public IEnumerable<Subjects> ListSubject(string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var ListFinal = ListSubject(search).OrderByDescending(s => s.ID);

            return ListFinal.ToPagedList(page.Value, pageSize);
        }
        

        public IEnumerable<Class_Subjects> ListClass(string search)
        {
            if (search == null)
                return db.Class_Subjects.ToList();

            return db.Class_Subjects.Where(c => c.ID.Contains(search) || c.SubjectID.Contains(search) ||
            c.Subjects.SubjectName.Contains(search) || c.Subjects.Note.Contains(search)).ToList();
        }

        public IEnumerable<Class_Subjects> ListClass(string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var list = ListClass(search).OrderBy(c => c.SubjectID);
            return list.ToPagedList(page.Value, pageSize);
        }

        public IEnumerable<DivisionClasses> DivClass(string search)
        {
            if (search == null)
                return db.DivisionClasses.OrderBy(u => u.Class_Subjects.SubjectID).ToList();
            else
                return db.DivisionClasses.Where(c => c.IDClass.Contains(search) || c.Users.Name.Contains(search) ||
                c.Users.Classes.ClassName.Contains(search) || c.Class_Subjects.SubjectID.Contains(search) ||
                c.Class_Subjects.Subjects.SubjectName.Contains(search)
                ).ToList();
        }

        public IEnumerable<DivisionClasses> DivClass(string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var list = DivClass(search).OrderBy(u => u.Class_Subjects.SubjectID);
            return list.ToPagedList(page.Value, pageSize);
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
                return db.DivionProjects.Where(p => p.RegistrationClasses.Class_Subjects.SubjectID == idProject).ToList();
            else
                return db.DivionProjects.Where(p => p.RegistrationClasses.Class_Subjects.SubjectID == idProject &&
                (p.Users.Name.Contains(search) || p.Users.Classes.ClassName.Contains(search) ||
                p.RegistrationClasses.Users.Name.Contains(search) || p.RegistrationClasses.Users.ID.Contains(search)
                )).ToList();
        }

        public IEnumerable<DivionProjects> DivProject(string idProject, string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var list = DivProject(idProject, search).OrderBy(p => p.IDTeacher);
            return DivProject(idProject, search).ToPagedList(page.Value, pageSize);
        }

    }
}