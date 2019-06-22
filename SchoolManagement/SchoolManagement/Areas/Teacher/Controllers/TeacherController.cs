using System;
using System.Linq;
using System.Web.Mvc;
using SchoolManagement.DAL;
using PagedList;
using PagedList.Mvc;


namespace SchoolManagement.Areas.Teacher.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/Teacher
        private TeacherDAL dal = new TeacherDAL();

        public ActionResult Index()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 2)
                    return View();
                else return View("Error");
            }
            catch { return View("Error"); }
        }

        // Schedule teacher
        public ActionResult PlanTeacher()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 2)
                {
                    return View(dal.getSchedule(Session["ID"].ToString()));
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        private static string idClass = null; //idClass load page

        //List  studentClass
        public ActionResult ListClass(string id, int? page, int pageSize = 10)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 2)
                {
                    idClass = id;
                    return View(dal.ListOfClasses(id, page, pageSize));
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        //List grades of idClass
        public ActionResult Mark(int? page, int pageSize = 10)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 2)
                {
                    return View(dal.GradeStudent(idClass, page, pageSize));
                }
                else { return View("Error"); }
            }
            catch
            {
                ViewBag.ErrorMark = "Error. Check code input";
                return RedirectToAction("Mark");
            }
        }

        //Edit score for student
        public ActionResult CreateScore(string MSSV, string IDSubject, string Score_QT, string Score_Final)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 2)
                {
                    using (SchoolManagement.Models.SchoolManagementEntities db = new Models.SchoolManagementEntities())
                    {
                        var grade = db.Grades.Where(g => g.IDStudent == MSSV && g.IDSubject == IDSubject).FirstOrDefault();
                        if (grade == null)
                            throw new Exception("Wrong ID Student or Subject");
                        else
                        {
                            grade.ScoreQT = double.Parse(Score_QT);
                            grade.ScoreFinal = double.Parse(Score_Final);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Mark");
                }
                else { return View("Error"); }
            }
            catch
            {
                ViewBag.ErrorMark = "Error. Check code input";
                return RedirectToAction("Mark");
            }
        }
    }
}