using System.Web.Mvc;
using SchoolManagement.DAL;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class ActiveController : Controller
    {
        // GET: Admin/Active
        private ActiveDAL dal = new ActiveDAL();

        public ActionResult Register(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.getSearch(searchString, page, pageSize));
                else
                    return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult ActiveSubject(string activeSubject)
        {
            try
            {
                ViewBag.activeSubject = activeSubject;
                dal.ActiveRegister(int.Parse(activeSubject), false);
                return RedirectToAction("Register");
            }
            catch
            {
                ViewBag.ErrorActive = "Check input";
                return RedirectToAction("Register");
            }
        }

        public ActionResult ResetSubject(string resetSubject)
        {
            try
            {
                ViewBag.resetSubject = resetSubject;
                dal.ResetRegister(int.Parse(resetSubject), false);
                return RedirectToAction("Register");
            }
            catch
            {
                ViewBag.ErrorActive = "Check input";
                return RedirectToAction("Register");
            }
        }

        public ActionResult ActiveClass(string activeClass)
        {
            try
            {
                ViewBag.activeClass = activeClass;
                dal.ActiveRegister(int.Parse(activeClass), true);
                return RedirectToAction("Register");
            }
            catch
            {
                ViewBag.ErrorActive = "Check input";
                return RedirectToAction("Register");
            }
        }

        public ActionResult ResetClass(string resetClass)
        {
            try
            {
                ViewBag.resetClass = resetClass;
                dal.ResetRegister(int.Parse(resetClass), true);
                return RedirectToAction("Register");
            }
            catch
            {
                ViewBag.ErrorActive = "Check input";
                return RedirectToAction("Register");
            }
        }
    }
}