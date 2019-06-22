using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.DAL
{
    public class TeacherDAL
    {
        private SchoolManagementEntities db;

        public TeacherDAL()
        {
            db = new SchoolManagementEntities();
        }

        //return by idTeacher
        public IEnumerable<DivisionClasses> getSchedule(string idTeacher)
        {
            return db.DivisionClasses.Where(t => t.IDTeacher == idTeacher).OrderBy(t => t.Class_Subjects.Time).ToList();
        }

        //Return list class student
        public IEnumerable<RegistrationClasses> ListOfClasses(string idClass)
        {
            return db.RegistrationClasses.Where(r => r.IDClass == idClass).ToList();
        }

        public IEnumerable<RegistrationClasses> ListOfClasses(string idClass, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var ListFinal = ListOfClasses(idClass).OrderBy(i => i.Users.Name);
            return ListFinal.ToPagedList(page.Value, pageSize);
        }

        //Grades student
        //if not have score => auto create QT = 0, Final = 0
        public IEnumerable<Grades> GradeStudent(string idClass)
        {
            List<RegistrationClasses> listClass = ListOfClasses(idClass).ToList();

            List<Grades> listGrades = new List<Grades>();
            string mssv, idSubject;
            foreach (var item in listClass)
            {
                mssv = item.IDStudent; idSubject = item.Class_Subjects.SubjectID;
                var grade = db.Grades.Where(g => g.IDStudent == mssv && g.IDSubject == idSubject).FirstOrDefault();
                if(grade == null) //check null?
                {
                    Grades _gd = new Grades();
                    _gd.IDStudent = mssv;
                    _gd.IDSubject = idSubject;
                    _gd.ScoreQT = 0;
                    _gd.ScoreFinal = 0;
                    _gd.Semester = "20151";
                    db.Grades.Add(_gd);
                    db.SaveChanges();
                }
                listGrades.Add(grade);
                db.SaveChanges();
            }

            return listGrades.ToList();
        }

        public IEnumerable<Grades> GradeStudent(string idClass, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var ListFinal = GradeStudent(idClass).OrderBy(g => g.Users.Name);
            return ListFinal.ToPagedList(page.Value, pageSize);
        }

    }
}