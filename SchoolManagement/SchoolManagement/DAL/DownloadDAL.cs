using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using SchoolManagement.Models;

namespace SchoolManagement.DAL
{
    public class DownloadDAL
    {
        private SchoolManagementEntities db;
        public DownloadDAL()
        {
            db = new SchoolManagementEntities();
        }

    }
}