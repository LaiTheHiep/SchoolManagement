using System.Linq;
using System.Web.Mvc;
using SchoolManagement.DAL;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class ResetAllController : Controller
    {
        // GET: Admin/ResetAll
        private ResetAllDAL dal = new ResetAllDAL();

        public ActionResult ResetSubjects(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;

                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.SearchSubject(searchString, page, pageSize));
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        public ActionResult DeleteSubject()
        {
            using (Models.SchoolManagementEntities db = new Models.SchoolManagementEntities())
            {
                db.RegistrationSubjects.RemoveRange(db.RegistrationSubjects);

                return RedirectToAction("ResetSubjects");
            }
        }

        public ActionResult ResetClasses(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;

                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.Search(searchString, page, pageSize));
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        public ActionResult DeleteClass()
        {
            using (Models.SchoolManagementEntities db = new Models.SchoolManagementEntities())
            {
                db.DivionProjects.RemoveRange(db.DivionProjects);
                db.DivisionClasses.RemoveRange(db.DivisionClasses);
                db.RegistrationClasses.RemoveRange(db.RegistrationClasses);
                db.Class_Subjects.RemoveRange(db.Class_Subjects);

                return RedirectToAction("ResetClasses");
            }
        }
    }
}