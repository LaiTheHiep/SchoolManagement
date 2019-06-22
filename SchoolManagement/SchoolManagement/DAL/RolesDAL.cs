using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolManagement.Models;

namespace SchoolManagement.DAL
{
    public class RolesDAL
    {
        private SchoolManagementEntities db;

        public RolesDAL()
        {
            db = new SchoolManagementEntities();
        }

        public void Add(Roles role)
        {
            db.Roles.Add(role);
            Save();
        }

        public IEnumerable<Roles> getAll()
        {
            return db.Roles.ToList();
        }

        public Roles getByID(object id)
        {
            return db.Roles.Find(id);
        }

        public void Update(Roles role)
        {
            db.Entry(role).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            var role = db.Roles.Find(id);
            if (role != null)
            {
                db.Roles.Remove(role);
                Save();
            }
        }

        private void Save()
        {
            db.SaveChanges();
        }
    }
}