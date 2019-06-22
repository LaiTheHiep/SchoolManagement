using SchoolManagement.DAL;
using SchoolManagement.Models;
using System.Net;
using System.Web.Mvc;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class UserAdminController : Controller
    {
        // GET: Admin/UserAdmin
        private UsersDAL dal = new UsersDAL();

        public ActionResult Index(string searchString)
        {
            try
            {
                ViewBag.searchString = searchString;

                if (searchString == null)
                {
                    if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                        return View(dal.getAdmin());
                    else
                        return View("Error", "Admin");
                }
                else
                {
                    int idRole = 1;
                    return View(dal.getSearchUsers(idRole, searchString));
                }
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
            catch
            {
                return View("Error", "Admin");
            }
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
                user.LoginErrorMessage = "Check input(Password, Email, Phone unique). Or check IDClass or IDRole";
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
                dal.Delete(id);
            return RedirectToAction("Index");
        }

    }
}