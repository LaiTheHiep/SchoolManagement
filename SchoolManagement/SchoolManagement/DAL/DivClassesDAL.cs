using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.DAL
{
    public class DivClassesDAL
    {
        private SchoolManagementEntities db;

        public DivClassesDAL()
        {
            db = new SchoolManagementEntities();
        }

        public DivisionClasses getByID(int id)
        {
            return db.DivisionClasses.Find(id);
        }

        //public IEnumerable<DivisionClasses> GetAll()
        //{
        //    return db.DivisionClasses.ToList();
        //}

        //public IEnumerable<DivisionClasses> GetAll(int page, int pageSize)
        //{
        //    return GetAll().OrderBy(d => d.IDTeacher).ToPagedList(page, pageSize);
        //}

        public IEnumerable<DivisionClasses> GetSearch(string search)
        {
            if (search == null)
                return db.DivisionClasses.ToList();

            return db.DivisionClasses.Where(s => s.IDClass.Contains(search) || s.Users.Name.Contains(search) ||
            s.Class_Subjects.Subjects.SubjectName.Contains(search) || s.Class_Subjects.SubjectID.Contains(search)
            ).ToList();
        }

        public IEnumerable<DivisionClasses> GetSearch(string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var ListFinal = GetSearch(search).OrderBy(d => d.IDTeacher);
            return ListFinal.ToPagedList(page.Value, pageSize);
        }

        public void Add(string idClass, string idTearch)
        {
            var divClass = db.DivisionClasses.Where(d => d.IDClass == idClass).FirstOrDefault();
            if (divClass == null)
            {
                DivisionClasses division = new DivisionClasses();
                //division.ID = int.Parse(idClass);
                division.IDClass = idClass;
                division.IDTeacher = idTearch;
                db.DivisionClasses.Add(division);
                db.SaveChanges();
            }
            else
            {
                divClass.IDTeacher = idTearch;
                Update(divClass);
            }
        }

        public void Update(DivisionClasses divisionClasses)
        {
            db.Entry(divisionClasses).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void Delete(int? id)
        {
            if (id != null)
            {
                var divClass = db.DivisionClasses.Find(id);
                if (divClass != null)
                {
                    db.DivisionClasses.Remove(divClass);
                    Save();
                }
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}