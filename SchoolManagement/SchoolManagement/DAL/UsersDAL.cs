using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;
using PagedList;

namespace SchoolManagement.DAL
{
    public class UsersDAL
    {
        private SchoolManagementEntities db;

        public UsersDAL()
        {
            db = new SchoolManagementEntities();
        }

        // get List All
        #region Get List User
        public IEnumerable<Users> getAll()
        {
            return db.Users.ToList();
        }

        public IEnumerable<Users> getAdmin()
        {
            return db.Users.Where(a => a.IDRole == 1).ToList();
        }

        public IEnumerable<Users> getTeacher()
        {
            return db.Users.Where(a => a.IDRole == 2).ToList();
        }

        public IEnumerable<Users> getGuest()
        {
            return db.Users.Where(a => a.IDRole > 3).ToList();
        }
        #endregion

        // use pageList << 1 2 3...>>
        #region Get List use pageList
        public IEnumerable<Users> getTeacher(int page, int pageSize)
        {
            return db.Users.Where(a => a.IDRole == 2).OrderBy(a => a.Classes.ClassName).ToPagedList(page, pageSize);
        }

        public IEnumerable<Users> getStudent(int page, int pageSize)
        {
            return db.Users.Where(a => a.IDRole == 3).OrderBy(a => a.Classes.ClassName).ToPagedList(page, pageSize);
        }

        public IEnumerable<Users> getGuest(int page, int pageSize)
        {
            return db.Users.Where(a => a.IDRole > 3).OrderBy(a => a.Classes.ClassName).ToPagedList(page, pageSize);
        }
        #endregion

        // Search
        #region Search users
        public Users getByID(string id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<Users> getSearchUsers(int idRole, string search)
        {
            if (search == null)
                return db.Users.Where(u => u.IDRole == idRole).ToList();

            return db.Users.Where(u => u.IDRole == idRole && (u.ID.Contains(search) || u.Classes.ClassName.Contains(search) ||
            u.Name.Contains(search) || u.Address.Contains(search)
            )).ToList();
        }

        public IEnumerable<Users> getSearchUsers(int idRole, string search, int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            var ListFinal = getSearchUsers(idRole, search).OrderBy(a => a.Name);

            return ListFinal.ToPagedList(page.Value, pageSize);
        }
        #endregion

        // Edit, delete, create
        #region Action with database
        public void Add(Users user)
        {
            user.Password = CryptographyDAL.EncryptMD5(user.Password);
            db.Users.Add(user);
            Save();
        }

        public void Update(Users user)
        {
            bool changePass = false;
            string pass = user.Password;
            using (SchoolManagementEntities data = new SchoolManagementEntities())
            {
                var member = data.Users.Where(u => u.Password == pass).FirstOrDefault();
                if (member == null)
                    changePass = true;
            }
            if (changePass)
                user.Password = CryptographyDAL.EncryptMD5(user.Password);

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void Delete(string id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                Save();
            }
        }
        #endregion

        // Save database
        public void Save()
        {
            db.SaveChanges();
        }

    }
}