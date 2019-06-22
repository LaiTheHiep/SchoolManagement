using System.Web.Mvc;
using SchoolManagement.Models;
using SchoolManagement.DAL;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class DivOtherController : Controller
    {
        // GET: Admin/DivOther
        private DivClassesDAL dal = new DivClassesDAL();

        public ActionResult Index(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.GetSearch(searchString, page, pageSize));
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        public ActionResult Create(string IDClass, string IDTeacher)
        {
            try
            {
                dal.Add(IDClass, IDTeacher);
                return RedirectToAction("Index");
            }
            catch
            {
                CheckDAL.MessageAlert("Error. Check input");
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (id.HasValue)
                    {
                        var division = dal.getByID(id.Value);
                        if (division == null)
                            return RedirectToAction("Index");
                        else
                            return View(division);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Edit(int? id, DivisionClasses division)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    division.ID = id.Value;
                    dal.Update(division);
                    return RedirectToAction("Index");
                }
                return View(division);
            }
            catch
            {
                division.ErrorDivClass = "Error. Check input";
                return View(division);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                dal.Delete(id);
                return RedirectToAction("Index");
            }
            return View("Error");
        }
    }
}