using SchoolManagement.DAL;
using SchoolManagement.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class UserTeacherController : Controller
    {
        // GET: Admin/UserTeacher
        private UsersDAL dal = new UsersDAL();

        public ActionResult Index(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;
                int idRole = 2;
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.getSearchUsers(idRole, searchString, page, pageSize));
                else
                    return View("Error", "Admin");
            }
            catch { return View("Error", "Admin"); }
        }

        public ActionResult Edit(string id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    Users user = dal.getByID(id);
                    if (user == null)
                        return HttpNotFound();
                    return View(user);
                }
                else
                    return View("Error");
            }
            catch { return View("Error", "Admin"); }
        }

        [HttpPost]
        public ActionResult Edit(Users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dal.Update(user);
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch
            {
                user.LoginErrorMessage = "Error! Check input(Password, IDClass, IDRole)";
                return View(user);
            }
        }

        public ActionResult Delete(string id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Users user = dal.getByID(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
                else
                    return View("Error");
            }
            catch { return View("Error", "Admin"); }
        }

        [HttpPost]
        public ActionResult Delete(string id, Users user)
        {
            if (id != null)
            {
                using (SchoolManagementEntities db = new SchoolManagementEntities())
                {
                    db.DivionProjects.RemoveRange(db.DivionProjects.Where(u => u.IDTeacher == id));
                    db.DivisionClasses.RemoveRange(db.DivisionClasses.Where(u => u.IDTeacher == id));
                }
                dal.Delete(id);
            }
            return RedirectToAction("Index");
        }
    }
}