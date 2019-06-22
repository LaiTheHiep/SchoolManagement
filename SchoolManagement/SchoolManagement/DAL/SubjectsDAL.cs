using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.DAL
{
    public class SubjectsDAL
    {
        private SchoolManagementEntities db;

        public SubjectsDAL()
        {
            db = new SchoolManagementEntities();
        }

        //public IEnumerable<Subjects> getAll()
        //{
        //    return db.Subjects.ToList();
        //}

        //public IEnumerable<Subjects> getAll(int page, int pageSize)
        //{
        //    return getAll().OrderByDescending(s => s.ID).ToPagedList(page, pageSize);
        //}

        public IEnumerable<Subjects> getSearch(string search)
        {
            if(search == null)
                return db.Subjects.ToList();

            return db.Subjects.Where(s => s.ID.Contains(search) || s.SubjectName.Contains(search)).ToList();
        }

        public IEnumerable<Subjects> getSearch(string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var ListFinal = getSearch(search).OrderByDescending(s => s.ID);

            return ListFinal.ToPagedList(page.Value, pageSize);
        }

        public Subjects getByID(string id)
        {
            return db.Subjects.Find(id);
        }

        public void Add(Subjects subject)
        {
            db.Subjects.Add(subject);
            Save();
        }

        public void Update(Subjects subject)
        {
            db.Entry(subject).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void Delete(string id)
        {
            var subject = db.Subjects.Find(id);
            if (subject != null)
            {
                db.Subjects.Remove(subject);
                Save();
            }
        }

        private void Save()
        {
            db.SaveChanges();
        }
    }
}