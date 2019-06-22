using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.DAL
{
    public class ResetAllDAL
    {
        private SchoolManagementEntities db;

        public ResetAllDAL()
        {
            db = new SchoolManagementEntities();
        }

        public IEnumerable<RegistrationSubjects> getSubjects()
        {
            return db.RegistrationSubjects.ToList();
        }

        public void ResetSubject()
        {
            db.RegistrationSubjects.RemoveRange(db.RegistrationSubjects.ToList());
            Save();
        }

        public IEnumerable<RegistrationClasses> getClasses()
        {
            return db.RegistrationClasses.ToList();
        }

        public void ResetClass()
        {
            db.RegistrationClasses.RemoveRange(db.RegistrationClasses.ToList());
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<funClass_Result> Search(string _search)
        {
            if (_search == null)
                return db.funClass().ToList();

            return db.funClass().Where(f => f.IDClass.Contains(_search) || f.SubjectName.Contains(_search)).ToList();
        }

        public IEnumerable<funClass_Result> Search(string _search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var ListFinal = Search(_search).OrderBy(s => s.IDClass);
            return ListFinal.ToPagedList(page.Value, pageSize);
        }

        //public IEnumerable<funClass_Result> CountClasses()
        //{
        //    return db.funClass();
        //}

        //public IEnumerable<funClass_Result> CountClasses(int page, int pageSize)
        //{
        //    return CountClasses().OrderBy(c => c.IDClass).ToPagedList(page, pageSize);
        //}

        //public IEnumerable<funSubject_Result> CountSubjects()
        //{
        //    return db.funSubject().OrderBy(s => s.IDSubject);
        //}

        //public IEnumerable<funSubject_Result> CountSubjects(int page, int pageSize)
        //{
        //    return CountSubjects().ToPagedList(page, pageSize);
        //}

        public IEnumerable<funSubject_Result> SearchSubject(string _search)
        {
            if (_search == null)
                return db.funSubject().ToList();

            return db.funSubject().Where(f => f.IDSubject.Contains(_search) || f.SubjectName.Contains(_search)).ToList();
        }

        public IEnumerable<funSubject_Result> SearchSubject(string _search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var ListFinal = SearchSubject(_search).OrderBy(s => s.IDSubject);
            return ListFinal.ToPagedList(page.Value, pageSize);
        }
    }

}