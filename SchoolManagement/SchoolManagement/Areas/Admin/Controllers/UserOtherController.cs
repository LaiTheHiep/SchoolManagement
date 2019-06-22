using SchoolManagement.DAL;
using SchoolManagement.Models;
using System.Net;
using System.Web.Mvc;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class UserOtherController : Controller
    {
        // GET: Admin/UserOther
        private UsersDAL dal = new UsersDAL();

        public ActionResult Index(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;
                int idRole = 4;

                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.getSearchUsers(idRole, searchString, page, pageSize));
                else
                    return View("Error");


            }
            catch { return View("Error"); }
        }

        public ActionResult Edit(string id)
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
            catch { user.LoginErrorMessage = "Error! Check input(Password, IDClass, IDRole)"; return View(user); }
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
                //using(SchoolManagementEntities db = new SchoolManagementEntities())
                //{
                //    db.RegistrationSubjects.RemoveRange(db.RegistrationSubjects.Where(u => u.IDStudent == id));
                //    db.RegistrationClasses.RemoveRange(db.RegistrationClasses.Where(u => u.IDStudent == id));
                //    db.Grades.RemoveRange(db.Grades.Where(u => u.IDStudent == id));
                //}
                dal.Delete(id);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    return View(new Users());
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Create(Users user)
        {
            try
            {
                dal.Add(user);
                return RedirectToAction("Index");
            }
            catch { ViewBag.ErrorCreateOther = "Error! Check input"; return View(new Users()); }
        }
    }
}