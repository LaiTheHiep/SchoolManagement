using System.Net;
using System.Web.Mvc;
using SchoolManagement.DAL;
using SchoolManagement.Models;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class ClassController : Controller
    {
        // GET: Admin/Class
        private ClassesDAL dal = new ClassesDAL();

        public ActionResult Index(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (searchString == null)
                        return View(dal.getAll(page, pageSize));
                    else
                        return View(dal.getSearch(searchString, page, pageSize));
                }
                else
                {
                    return View("Error");
                }
            }
            catch { return View("Error"); }
        }

        public ActionResult Create()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    return View(new Classes());
                }
                else
                {
                    return View("Error");
                }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Create(Classes classes)
        {
            try
            {
                dal.Add(classes);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorCreateClass = "Error. Check IDClass or IDSubject";
                return View(new Classes());
            }
        }

        public ActionResult Edit(string id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    Classes classes = dal.getByID(id);
                    if (classes == null)
                        return HttpNotFound();
                    return View(classes);
                }
                else
                {
                    return View("Error");
                }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Edit(Classes classes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dal.Update(classes);
                    return RedirectToAction("Index");
                }
                return View(classes);
            }
            catch
            {
                classes.ErrorClass = "Check input. Retry!";
                return View(classes);
            }
        }

        public ActionResult Delete(string id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    Classes classes = dal.getByID(id);
                    if (classes == null)
                        return HttpNotFound();
                    return View(classes);
                }
                else
                {
                    return View("Error");
                }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Delete(string id, Classes classes)
        {
            if (id != null)
                dal.Delete(id);
            return RedirectToAction("Index");
        }
    }
}