using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolManagement.Models;

namespace SchoolManagement.DAL
{
    public class ActiveDAL : ClassesDAL
    {
        private SchoolManagementEntities db;

        public ActiveDAL()
        {
            db = new SchoolManagementEntities();
        }

        public void ActiveRegister(int course, bool activeClass)
        {
            List<Classes> list = getSearch().Where(c => c.Course == course).ToList();
            if (activeClass)
            {
                foreach (var item in list)
                {
                    item.Active_Class = true;
                    Update(item);
                }
            }
            else
            {
                foreach (var item in list)
                {
                    item.Active_Subject = true;
                    Update(item);
                }
            }
        }

        public void ResetRegister(int course, bool resetClass)
        {
            List<Classes> list = getSearch().Where(c => c.Course == course).ToList();
            if (resetClass)
            {
                foreach (var item in list)
                {
                    item.Active_Class = false;
                    Update(item);
                }
            }
            else
            {
                foreach (var item in list)
                {
                    item.Active_Subject = false;
                    Update(item);
                }
            }
        }
    }
}