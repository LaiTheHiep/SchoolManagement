using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SchoolManagement.Models;
using SchoolManagement.DAL;
using OfficeOpenXml;

namespace SchoolManagement.Controllers
{
    public class InformationPublicController : Controller
    {
        // GET: InformationPublic
        private InformationPublicDAL dal = new InformationPublicDAL();

        #region Users
        public ActionResult Teacher(string searchString, int? page, int pageSize = 10)
        {
            ViewBag.SearchString = searchString;
            return View(dal.getUser(searchString, 2, page, pageSize));
        }

        public ActionResult Student(string searchString, int? page, int pageSize = 10)
        {
            ViewBag.SearchString = searchString;
            return View(dal.getUser(searchString, 3, page, pageSize)); // 3-IDRole -Student
        }

        public ActionResult ListofClass(string searchString, int? page, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            return View(dal.getStudentClass(searchString, page, pageSize));
        }

        public ActionResult ViewListClass(string id, int? page, int pageSize = 10)
        {
            return View(dal.getViewStudentClass(id, page, pageSize));
        }

        #endregion

        #region All List
        public ActionResult ListSubject(string searchString, int? page, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            return View(dal.ListSubject(searchString, page, pageSize));
        }

        public ActionResult ListClass(string searchString, int? page, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            return View(dal.ListClass(searchString, page, pageSize));
        }

        public ActionResult ViewClass(string id, int? page, int pageSize = 20)
        {
            if (id == null)
                return RedirectToAction("ListClass");
            if (!page.HasValue)
                page = 1;
            return View(dal.ViewClass(id, page.Value, pageSize));
        }

        #endregion

        #region Div
        public ActionResult Project1(string searchString, int? page, int pageSize = 15)
        {
            ViewBag.searchString = searchString;
            return View(dal.DivProject("ET3170", searchString, page, pageSize));
        }

        public ActionResult Project2(string searchString, int? page, int pageSize = 15)
        {
            ViewBag.searchString = searchString;
            return View(dal.DivProject("ET4210", searchString, page, pageSize));
        }

        public ActionResult Project3(string searchString, int? page, int pageSize = 15)
        {
            ViewBag.searchString = searchString;
            return View(dal.DivProject("ET5020", searchString, page, pageSize));
        }

        public ActionResult DivClasses(string searchString, int? page, int pageSize = 15)
        {
            ViewBag.searchString = searchString;
            return View(dal.DivClass(searchString, page, pageSize));
        }
        #endregion

        #region Download file excel
        public void DownloadViewListClass(string idClass)
        {
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                List<Users> list = db.Users.Where(c => c.IDClass == idClass).OrderBy(c => c.Name).ToList();

                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("List");

                worksheet.Cells["A1"].Value = "Course";
                worksheet.Cells["B1"].Value = list[0].Classes.Course;

                worksheet.Cells["A2"].Value = "Class";
                worksheet.Cells["B2"].Value = list[0].Classes.ClassName;

                worksheet.Cells["A3"].Value = "Date";
                worksheet.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

                worksheet.Cells["A5"].Value = "STT";
                worksheet.Cells["B5"].Value = "MSSV";
                worksheet.Cells["C5"].Value = "Student";
                worksheet.Cells["D5"].Value = "Class";
                worksheet.Cells["E5"].Value = "Phone";
                worksheet.Cells["F5"].Value = "Email";
                worksheet.Cells["G5"].Value = "Address";

                int rowStart = 6;
                foreach (var item in list.ToList())
                {
                    worksheet.Cells[string.Format("A{0}", rowStart)].Value = rowStart - 5;
                    worksheet.Cells[string.Format("B{0}", rowStart)].Value = item.ID;
                    worksheet.Cells[string.Format("C{0}", rowStart)].Value = item.Name;
                    worksheet.Cells[string.Format("D{0}", rowStart)].Value = item.Classes.ClassName;
                    worksheet.Cells[string.Format("E{0}", rowStart)].Value = item.Phone;
                    worksheet.Cells[string.Format("F{0}", rowStart)].Value = item.Email;
                    worksheet.Cells[string.Format("G{0}", rowStart)].Value = item.Address;

                    rowStart++;
                }
                worksheet.Cells["A:AZ"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }
        }

        public void DownloadViewClass(string idClass)
        {
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                List<RegistrationClasses> list = db.RegistrationClasses.Where(c => c.IDClass == idClass).OrderBy(c => c.Users.Name).ToList();

                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("List");

                worksheet.Cells["A1"].Value = "Subject";
                worksheet.Cells["B1"].Value = list[0].Class_Subjects.Subjects.SubjectName;

                worksheet.Cells["A2"].Value = "IDSubject";
                worksheet.Cells["B2"].Value = list[0].Class_Subjects.SubjectID;

                worksheet.Cells["A3"].Value = "IDClass";
                worksheet.Cells["B3"].Value = list[0].IDClass;

                worksheet.Cells["A5"].Value = "STT";
                worksheet.Cells["B5"].Value = "MSSV";
                worksheet.Cells["C5"].Value = "Student";
                worksheet.Cells["D5"].Value = "Class";
                worksheet.Cells["E5"].Value = "Course";
                worksheet.Cells["F5"].Value = "Phone";
                worksheet.Cells["G5"].Value = "Email";
                worksheet.Cells["H5"].Value = "Address";

                int rowStart = 6;
                foreach (var item in list.ToList())
                {
                    worksheet.Cells[string.Format("A{0}", rowStart)].Value = rowStart - 5;
                    worksheet.Cells[string.Format("B{0}", rowStart)].Value = item.ID;
                    worksheet.Cells[string.Format("C{0}", rowStart)].Value = item.Users.Name;
                    worksheet.Cells[string.Format("D{0}", rowStart)].Value = item.Users.Classes.ClassName;
                    worksheet.Cells[string.Format("E{0}", rowStart)].Value = item.Users.Classes.Course;
                    worksheet.Cells[string.Format("F{0}", rowStart)].Value = item.Users.Phone;
                    worksheet.Cells[string.Format("G{0}", rowStart)].Value = item.Users.Email;
                    worksheet.Cells[string.Format("H{0}", rowStart)].Value = item.Users.Address;

                    rowStart++;
                }
                worksheet.Cells["A:AZ"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }
        }

        public void DownloadProject(string idProject)

        {
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                List<DivionProjects> list = db.DivionProjects.Where(c => c.RegistrationClasses.Class_Subjects.SubjectID == idProject).OrderBy(c => c.Users.Name).ToList();

                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("List");

                worksheet.Cells["A1"].Value = "Subject";
                worksheet.Cells["B1"].Value = list[0].RegistrationClasses.Class_Subjects.Subjects.SubjectName;

                worksheet.Cells["A2"].Value = "IDSubject";
                worksheet.Cells["B2"].Value = list[0].RegistrationClasses.Class_Subjects.SubjectID;

                worksheet.Cells["A3"].Value = "IDClass";
                worksheet.Cells["B3"].Value = list[0].RegistrationClasses.IDClass;

                worksheet.Cells["A5"].Value = "STT";
                worksheet.Cells["B5"].Value = "MSSV";
                worksheet.Cells["C5"].Value = "Student";
                worksheet.Cells["D5"].Value = "Class";
                worksheet.Cells["E5"].Value = "Teacher";
                worksheet.Cells["F5"].Value = "Bomon";
                worksheet.Cells["G5"].Value = "Phone";
                worksheet.Cells["H5"].Value = "Eamil";

                int rowStart = 6;
                foreach (var item in list.ToList())
                {
                    worksheet.Cells[string.Format("A{0}", rowStart)].Value = rowStart - 5;
                    worksheet.Cells[string.Format("B{0}", rowStart)].Value = item.RegistrationClasses.IDStudent;
                    worksheet.Cells[string.Format("C{0}", rowStart)].Value = item.RegistrationClasses.Users.Name;
                    worksheet.Cells[string.Format("D{0}", rowStart)].Value = item.RegistrationClasses.Users.Classes.ClassName;
                    worksheet.Cells[string.Format("E{0}", rowStart)].Value = item.Users.Name;
                    worksheet.Cells[string.Format("F{0}", rowStart)].Value = item.Users.Classes.ClassName;
                    worksheet.Cells[string.Format("G{0}", rowStart)].Value = item.Users.Phone;
                    worksheet.Cells[string.Format("H{0}", rowStart)].Value = item.Users.Email;

                    rowStart++;
                }
                worksheet.Cells["A:AZ"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }
        }
        #endregion
        
    }
}