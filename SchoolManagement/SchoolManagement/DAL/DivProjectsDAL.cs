using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.DAL
{
    public class DivProjectsDAL
    {
        private SchoolManagementEntities db;

        public DivProjectsDAL()
        {
            db = new SchoolManagementEntities();
        }

        // get all Project 1,2,3
        public IEnumerable<Subjects> Project()
        {
            return db.Subjects.Where(p => p.ID == "ET3170" || p.ID == "ET4210" || p.ID == "ET5020").ToList();
        }

        // get Project 1 or 2 or 3
        public IEnumerable<DivionProjects> getProject(string IDProject)
        {
            return db.DivionProjects.Where(p => p.RegistrationClasses.Class_Subjects.SubjectID == IDProject).ToList();
        }

        // get table by ID(Student, Teacher, Project)
        public DivionProjects getByID(int id)
        {
            return db.DivionProjects.Find(id);
        }

        // Search by Student or Teacher
        public IEnumerable<DivionProjects>  getSearch(string idProject, string search)
        {
            if(search == null)
                return db.DivionProjects.Where(p => p.RegistrationClasses.Class_Subjects.SubjectID == idProject).ToList();

            return db.DivionProjects.Where(s => s.RegistrationClasses.Class_Subjects.SubjectID == idProject &&
            ( s.Users.Name.Contains(search) || s.RegistrationClasses.Users.Name.Contains(search) || 
            s.RegistrationClasses.IDStudent.Contains(search) || s.RegistrationClasses.Users.Classes.ClassName.Contains(search)
            )).ToList();
        }

        public IEnumerable<DivionProjects> getSearch(string idProject, string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var ListFinal = getSearch(idProject, search).OrderBy(p => p.IDTeacher);
            return ListFinal.ToPagedList(page.Value, pageSize);
        }

        public void Add(DivionProjects divionProjects)
        {
            db.DivionProjects.Add(divionProjects);
            Save();
        }

        public void Update(DivionProjects divionProjects)
        {
            db.Entry(divionProjects).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            var divProject = db.DivionProjects.Find(id);
            if (divProject != null)
            {
                db.DivionProjects.Remove(divProject);
                Save();
            }
        }

        public void Delete(string idSubject)
        {
            if (db.DivionProjects.ToList().Count > 0)
            {
                db.DivionProjects.RemoveRange(getProject(idSubject));
                Save();
            }
        }

        private void Save()
        {
            db.SaveChanges();
        }

        // Phan cong
        //Mix Teacher
        public IEnumerable<Users> Mix(List<Users> list)
        {
            Random ran = new Random();
            var m = list.Count - 1;

            for (int i = 0; i < list.Count; i++)
            {
                var x = ran.Next(i, m);
                var mix = list[x];
                list[x] = list[i];
                list[i] = mix;

            }
            return list;
        }
        //over load mix student registration
        public IEnumerable<RegistrationClasses> Mix(List<RegistrationClasses> list)
        {
            Random ran = new Random();
            var m = list.Count - 1;

            for (int i = 0; i < list.Count; i++)
            {
                var x = ran.Next(i, m);
                var mix = list[x];
                list[x] = list[i];
                list[i] = mix;

            }
            return list;
        }

        // Register Class
        public IEnumerable<RegistrationClasses> getBySubject(string idSubject)
        {
            return db.RegistrationClasses.Where(s => s.Class_Subjects.SubjectID == idSubject).ToList();
        }

        public void RandomDivision(string idSubject)
        {
            //get random student and teacher
            UsersDAL usersDAL = new UsersDAL();
            List<Users> listTeacher = Mix(usersDAL.getTeacher().ToList()).ToList();
            List<RegistrationClasses> listStudent = Mix(getBySubject(idSubject).ToList()).ToList();

            //Loop and division
            int demTeacher = 0;
            foreach (var item in listStudent)
            {
                if (demTeacher >= listTeacher.Count)
                    demTeacher = 0;
                var division = new DivionProjects();
                division.IDTeacher = listTeacher[demTeacher++].ID;
                division.IDRegistrationClass = item.ID;
                Add(division);
            }
            Save();
            //return getProject(idSubject);
        }
    }
}