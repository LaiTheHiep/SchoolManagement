using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.DAL
{
    public class ClassesDAL
    {
        private SchoolManagementEntities db;

        public ClassesDAL()
        {
            db = new SchoolManagementEntities();
        }

        // List data Class
        public IEnumerable<Classes> getSearch()
        {
            return db.Classes.OrderByDescending(c => c.Course);
        }

        public IEnumerable<Classes> getAll(int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;
            return getSearch().ToPagedList(page.Value, pageSize);
        }

        public Classes getByID(string id)
        {
            return db.Classes.Find(id);
        }

        public IEnumerable<Classes> getSearch(string search)
        {
            int _course;
            if (search != null)
            {
                if (int.TryParse(search, out _course))
                    return db.Classes.Where(c => c.Course == _course);
                else
                    return db.Classes.Where(c => c.ClassName.Contains(search));
            }
            return db.Classes.ToList();
        }

        public IEnumerable<Classes> getSearch(string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;
            var ListFinall = getSearch(search).OrderBy(c => c.ClassName);
            return ListFinall.ToPagedList(page.Value, pageSize);
        }

        // Edit database
        public void Add(Classes classes)
        {
            db.Classes.Add(classes);
            Save();
        }

        public void Update(Classes classes)
        {
            db.Entry(classes).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void Delete(string id)
        {
            var classes = db.Classes.Find(id);
            if (classes != null)
            {
                db.Classes.Remove(classes);
                Save();
            }
        }

        private void Save()
        {
            db.SaveChanges();
        }
    }
}