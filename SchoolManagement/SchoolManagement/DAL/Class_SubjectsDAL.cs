using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.DAL
{
    public class Class_SubjectsDAL
    {
        private SchoolManagementEntities db;

        public Class_SubjectsDAL()
        {
            db = new SchoolManagementEntities();
        }

        //public IEnumerable<Class_Subjects> getAll()
        //{
        //    return db.Class_Subjects.OrderBy(cs => cs.SubjectID).ToList();
        //}

        //public IEnumerable<Class_Subjects> getAll(int page, int pageSize)
        //{
        //    return getAll().ToPagedList(page, pageSize);
        //}

        public Class_Subjects getByID(string id)
        {
            return db.Class_Subjects.Find(id);
        }

        public IEnumerable<Class_Subjects> getSearch(string search)
        {
            if (search == null)
                return db.Class_Subjects.ToList();

            return db.Class_Subjects.Where(cs => cs.Subjects.SubjectName.Contains(search) || 
            cs.SubjectID.Contains(search) || cs.Note.Contains(search) || cs.ID.Contains(search)
            ).ToList();
        }

        public IEnumerable<Class_Subjects> getSearch(string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;           
            var ListFinal = getSearch(search).OrderBy(c => c.ID);
            return ListFinal.ToPagedList(page.Value, pageSize);
        }

        public void Add(Class_Subjects class_Subjects)
        {
            db.Class_Subjects.Add(class_Subjects);
            Save();
        }

        public void Update(Class_Subjects class_Subjects)
        {
            db.Entry(class_Subjects).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void Delete(string id)
        {
            var _class = db.Class_Subjects.Find(id);
            if(_class != null)
            {
                db.Class_Subjects.Remove(_class);
                Save();
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}