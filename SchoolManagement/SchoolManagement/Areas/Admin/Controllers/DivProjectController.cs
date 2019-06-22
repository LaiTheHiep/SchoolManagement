using System.Web.Mvc;
using SchoolManagement.Models;
using SchoolManagement.DAL;
using System.Net;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class DivProjectController : Controller
    {
        // GET: Admin/DivProject
        private DivProjectsDAL dal = new DivProjectsDAL();

        public ActionResult Index()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    return View(dal.Project());
                }
                else { return View("Error"); }
            }
            catch { return View("Error"); }
        }

        #region Project1
        //Project 1
        public ActionResult Project1(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;

                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.getSearch("ET3170", searchString, page, pageSize));
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        // Auto div
        public ActionResult CreateRandom1()
        {
            dal.RandomDivision("ET3170");
            return RedirectToAction("Project1");
        }

        public ActionResult DeleteAll1()
        {
            dal.Delete("ET3170");
            return RedirectToAction("Project1");
        }

        public ActionResult Edit1(int? id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var divionProject = dal.getByID(id.Value);
                    if (divionProject == null)
                    {
                        return HttpNotFound();
                    }
                    return View(divionProject);
                }
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult Edit1(int? id, DivionProjects divionProject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dal.Update(divionProject);
                    return RedirectToAction("Project1");
                }
                return View(divionProject);
            }
            catch
            {
                divionProject.ErrorDivProject = "Error. Check input";
                return View(divionProject);
            }
        }

        #endregion

        #region Project2
        //Project 2
        public ActionResult Project2(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;

                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.getSearch("ET4210", searchString, page, pageSize));
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        // Auto div
        public ActionResult CreateRandom2()
        {
            dal.RandomDivision("ET4210");
            return RedirectToAction("Project2");
        }

        public ActionResult DeleteAll2()
        {
            dal.Delete("ET4210");
            return RedirectToAction("Project2");
        }

        public ActionResult Edit2(int? id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var divionProject = dal.getByID(id.Value);
                    if (divionProject == null)
                    {
                        return HttpNotFound();
                    }
                    return View(divionProject);
                }
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2(int? id, DivionProjects divionProject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dal.Update(divionProject);
                    return RedirectToAction("Project2");
                }
                return View(divionProject);
            }
            catch
            {
                divionProject.ErrorDivProject = "Error. Check input";
                return View(divionProject);
            }
        }

        #endregion

        #region Project3
        //Project 3
        public ActionResult Project3(string searchString, int? page, int pageSize = 10)
        {
            try
            {
                ViewBag.searchString = searchString;

                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.getSearch("ET5020", searchString, page, pageSize));
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        // Auto div
        public ActionResult CreateRandom3()
        {
            dal.RandomDivision("ET5020");
            return RedirectToAction("Project3");
        }

        public ActionResult DeleteAll3()
        {
            dal.Delete("ET5020");
            return RedirectToAction("Project3");
        }

        public ActionResult Edit3(int? id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var divionProject = dal.getByID(id.Value);
                    if (divionProject == null)
                    {
                        return HttpNotFound();
                    }
                    return View(divionProject);
                }
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit3(int? id, DivionProjects divionProject)
        {
            if (ModelState.IsValid)
            {
                dal.Update(divionProject);
                return RedirectToAction("Project3");
            }
            return View(divionProject);
        }

        #endregion
    }
}