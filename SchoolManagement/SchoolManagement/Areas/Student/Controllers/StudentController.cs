using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.DAL;

namespace SchoolManagement.Areas.Student.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student/Student
        private StudentDAL dal = new StudentDAL();

        #region Study
        public ActionResult Index()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 3)
                {
                    return View();
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        public ActionResult Schedule()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 3)
                {
                    string mssv = Session["ID"].ToString();
                    return View(dal.Schedule(mssv).OrderBy(s => s.Class_Subjects.Time));
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        public ActionResult Grades()
        {

            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 3)
                {
                    string mssv = Session["ID"].ToString();
                    return View(dal.GredeByID(mssv));
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }
        #endregion

        #region Register Subjects
        public ActionResult RegSubject()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 3)
                {
                    string mssv = Session["ID"].ToString();
                    ViewBag.Check = CheckDAL.CheckActive(Session["ID"].ToString(), false);
                    return View(dal.ListRegSubject(mssv));
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        public ActionResult CreateRegSubject(string idSubject)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 3 && idSubject != null)
                {
                    string mssv = Session["ID"].ToString();
                    dal.RegisterSubject(mssv, idSubject);
                    //ViewBag.MessageReg = "Register successfull!";
                    //CheckDAL.MessageAlert("Register successfull!");
                    return RedirectToAction("RegSubject");
                }
                else { /*CheckDAL.MessageAlert("Code not true. Please try it"); */return View("Error"); }
            }
            catch
            {
                ViewBag.MessageReg = "Wrong IdSubject :(";
                return RedirectToAction("RegSubject");
            }
        }

        public ActionResult DeleteSubject(int? id)
        {
            if (id.HasValue)
            {
                dal.DeleteSubject(id.Value);
            }
            return RedirectToAction("RegSubject");
        }
        #endregion

        #region Register Classes
        public ActionResult RegClass()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 3)
                {
                    string mssv = Session["ID"].ToString();
                    ViewBag.Check = CheckDAL.CheckActive(Session["ID"].ToString(), true);
                    return View(dal.Schedule(mssv));
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        public ActionResult CreateRegClass(string idClass)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 3 && idClass != null)
                {
                    string mssv = Session["ID"].ToString();
                    dal.RegisterClass(mssv, idClass);

                    return RedirectToAction("RegClass");
                }
                else { return View("Error"); }
            }
            catch
            { return RedirectToAction("RegClass"); }
        }

        public ActionResult DeleteClass(int? id)
        {
            if (id.HasValue)
            {
                dal.DeleteClass(id.Value);
            }
            return RedirectToAction("RegClass");
        }
        #endregion

    }
}