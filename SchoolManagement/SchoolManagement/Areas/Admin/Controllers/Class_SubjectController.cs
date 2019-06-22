using System.Linq;
using System.Web.Mvc;
using SchoolManagement.Models;
using SchoolManagement.DAL;
using System.Net;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class Class_SubjectController : Controller
    {
        // GET: Admin/Class_Subject
        private Class_SubjectsDAL dal = new Class_SubjectsDAL();

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
                try
                {
                    if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    {
                        return View(new Class_Subjects());
                    }
                    else { return View("Error"); }
                }
                catch { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Create(Class_Subjects class_Subjects)
        {
            try
            {
                if (class_Subjects.Time == null || class_Subjects.Time == "Không có")
                {
                    class_Subjects.Time = "Không có";
                }
                else
                {
                    if (!ChangeString.CheckFomat(class_Subjects.Time))
                    {
                        ViewBag.ErrorCreateClass = "Error. Check input: Time(example:T2, 6h45-9h05)";
                        return View(new Class_Subjects());
                    }
                }
                dal.Add(class_Subjects);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorCreateClass = "Error. Check input: IDClass or IDSubject";
                return View(new Class_Subjects());
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
                    Class_Subjects class_Subjects = dal.getByID(id);
                    if (class_Subjects == null)
                    {
                        return HttpNotFound();
                    }
                    return View(class_Subjects);
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Edit(string id, Class_Subjects class_Subjects)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (class_Subjects.Time == null || class_Subjects.Time == "Không có")
                    {
                        class_Subjects.Time = "Không có";
                    }
                    else
                    {
                        if (!ChangeString.CheckFomat(class_Subjects.Time))
                        {
                            ViewBag.ErrorCreateClass = "Error. Check input: Time(example:T2, 6h45-9h05)";
                            return View(new Class_Subjects());
                        }
                    }
                    class_Subjects.ID = id;
                    dal.Update(class_Subjects);
                    return RedirectToAction("Index");
                }
                return View(class_Subjects);
            }
            catch
            {
                ViewBag.ErrorClass_Subject = "Error. Check input";
                return View(class_Subjects);
            }
        }

        public ActionResult Delete(string id, Class_Subjects class_Subjects)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var divisions = dal.getByID(id);
            if (divisions == null)
            {
                return HttpNotFound();
            }
            // Delete FK
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                db.DivionProjects.RemoveRange(db.DivionProjects.Where(r => r.RegistrationClasses.IDClass == id).ToList());
                db.DivisionClasses.RemoveRange(db.DivisionClasses.Where(r => r.IDClass == id).ToList());
                db.RegistrationClasses.RemoveRange(db.RegistrationClasses.Where(r => r.IDClass == id).ToList());
                db.SaveChanges();
            }
            dal.Delete(id);
            return RedirectToAction("Index");
        }
    }
}