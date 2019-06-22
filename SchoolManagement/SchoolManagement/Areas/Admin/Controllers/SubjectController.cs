using SchoolManagement.DAL;
using SchoolManagement.Models;
using System.Net;
using System.Web.Mvc;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Admin/Subject
        private SubjectsDAL dal = new SubjectsDAL();

        public ActionResult Index(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;

                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.getSearch(searchString, page, pageSize));
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        public ActionResult Create()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(new Subjects());
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Create(Subjects subject)
        {
            try
            {
                dal.Add(subject);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorCreateSubject = "Error. Check ID or Name";
                return View(new Subjects());
            }
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
                    Subjects subject = dal.getByID(id);
                    if (subject == null)
                    {
                        return HttpNotFound();
                    }
                    return View(subject);
                }

                else
                {
                    return View("Error");
                }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Edit(string id, Subjects subject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    subject.ID = id;
                    dal.Update(subject);
                    return RedirectToAction("Index");
                }
                return View(subject);
            }
            catch
            {
                subject.ErrorSubject = "Error. Check input";
                return View(subject);
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
                    Subjects subject = dal.getByID(id);
                    if (subject == null)
                    {
                        return HttpNotFound();
                    }
                    return View(subject);
                }

                else
                {
                    return View("Error");
                }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Delete(string id, Subjects subject)
        {
            if (id != null)
                dal.Delete(id);
            return RedirectToAction("Index");
        }

    }
}