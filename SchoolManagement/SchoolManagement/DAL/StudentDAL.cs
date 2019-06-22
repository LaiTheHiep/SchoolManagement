using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SchoolManagement.Models;

namespace SchoolManagement.DAL
{
    public class StudentDAL
    {
        private SchoolManagementEntities db;

        public StudentDAL()
        {
            db = new SchoolManagementEntities();
        }

        public IEnumerable<RegistrationClasses> Schedule(string mssv)
        {
            return db.RegistrationClasses.Where(s => s.IDStudent == mssv).ToList();
        }

        public IEnumerable<Grades> GredeByID(string mssv)
        {
            return db.Grades.Where(g => g.IDStudent == mssv).OrderBy(g => g.Semester);
        }

        public IEnumerable<RegistrationSubjects> ListRegSubject(string mssv)
        {
            return db.RegistrationSubjects.Where(r => r.IDStudent == mssv).OrderBy(r => r.IDSubject);
        }

        public void RegisterSubject(string mssv, string idSubject)
        {
            var _register = db.RegistrationSubjects.Where(r => r.IDStudent == mssv &&
            r.IDSubject == idSubject).FirstOrDefault();
            if (_register == null)
            {
                RegistrationSubjects reg = new RegistrationSubjects();
                reg.IDStudent = mssv; reg.IDSubject = idSubject;
                db.RegistrationSubjects.Add(reg);
                db.SaveChanges();
            }
        }

        public void DeleteSubject(int id)
        {
            var value = db.RegistrationSubjects.Find(id);
            if (value != null)
            {
                db.RegistrationSubjects.Remove(value);
                db.SaveChanges();
            }
        }

        //Check IDClass
        //Check Register IDClass
        //Check Time
        public bool CheckRegClass(string mssv, string idClass)
        {
            //Check register IDClass
            var _register = db.RegistrationClasses.Where(r => r.IDStudent == mssv && r.IDClass == idClass).FirstOrDefault();
            if (_register != null)
                return false;           

            //Check IDClass
            var _classSubject = db.Class_Subjects.Where(c => c.ID == idClass).FirstOrDefault();
            if (_classSubject == null)
                return false;

            //Check register subject by IDClass different
            var _subjectReg = db.RegistrationClasses.Where(r => r.IDStudent == mssv && r.Class_Subjects.SubjectID == _classSubject.SubjectID).FirstOrDefault();
            if (_subjectReg != null)
                return false;

            //Check Time
            DateTimeString newTime = ChangeString.CovertDate(_classSubject.Time);

            List<RegistrationClasses> listPlan = Schedule(mssv).ToList();
            foreach (var item in listPlan)
            {
                string s = item.Class_Subjects.Time.ToString();
                if (!ChangeString.CheckFomat(s))
                    continue;
                DateTimeString oldTime = ChangeString.CovertDate(s);
                if (!ChangeString.CheckAdd(oldTime, newTime))
                    return false;
            }

            return true;
        }

        //Check n/Quantity?
        // n < Quantity => register
        // else => do not register
        public bool CheckQuantity(string idClass)
        {
            List<Class_Subjects> table = db.Class_Subjects.Where(c => c.ID == idClass).ToList();
            if (table.Count == 0)
                return false;
            if (table.Count < int.Parse(table[0].Quantity.ToString()))
                return true;

            return false;
        }

        public void RegisterClass(string mssv, string idClass)
        {           
            if (CheckRegClass(mssv, idClass) && CheckQuantity(idClass))
            {
                RegistrationClasses reg = new RegistrationClasses();
                reg.IDStudent = mssv; reg.IDClass = idClass;
                db.RegistrationClasses.Add(reg);
                db.SaveChanges();
            }
        }

        public void DeleteClass(int id)
        {
            var value = db.RegistrationClasses.Find(id);
            if (value != null)
            {
                db.RegistrationClasses.Remove(value);
                db.SaveChanges();
            }
        }
    }
}