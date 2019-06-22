using SchoolManagement.DAL;
using SchoolManagement.Models;
using System.Net;
using System.Web.Mvc;

namespace SchoolManagement.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Home
        private RolesDAL dal = new RolesDAL();

        private MessageDAL messageDAL = new MessageDAL();

        public ActionResult Index(int? page, int pageSize = 20)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(messageDAL.getMessage(page, pageSize));
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        public ActionResult RepMessage(int id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    idMessage = id;

                    var admin = messageDAL.getAdmin(Session["ID"].ToString());
                    return View(admin);
                }
                else
                {
                    return View("Error");
                }
            }
            catch { return View("Error"); }
        }

        private static int idMessage = 0;

        [HttpPost]
        public ActionResult ResponseMessage(string email, string password, string subject, string message)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    var contact = messageDAL.getContactById(idMessage);
                    if (contact == null)
                        return View("Index");

                    contact.isRep = true;
                    messageDAL.AddRep(idMessage, Session["ID"].ToString(), message);
                    messageDAL.SendMail(email, password, contact.Email, subject, message);
                    messageDAL.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }
            catch { return View("Error"); }
        }

        public ActionResult RoleUser()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(dal.getAll());
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }

        public ActionResult RoleUserCreate()
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                    return View(new Roles());
                else
                    return View("Error");
            }
            catch { return View("Error"); }
        }
        [HttpPost]
        public ActionResult RoleUserCreate(Roles role)
        {
            try
            {
                dal.Add(role);
                return RedirectToAction("RoleUser", "Admin");
            }
            catch { ViewBag.Error = "Role is unique"; return View(new Roles()); }
        }

        public ActionResult RoleUserEdit(int? id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (id.HasValue)
                    {
                        var role = dal.getByID(id.Value);
                        if (role != null)
                            return View(role);
                    }
                    return RedirectToAction("RoleUser", "Admin");
                }

                else
                {
                    return View("Error");
                }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult RoleUserEdit(int? id, Roles role)
        {
            try
            {
                if (id.HasValue)
                {
                    role.ID = id.Value;
                    dal.Update(role);
                }
                return RedirectToAction("RoleUser", "Admin");
            }
            catch { role.ErrorMessage = "Role is unique!"; return View(role); }
        }

        public ActionResult RoleUserDelete(int? id)
        {
            try
            {
                if (CheckDAL.CheckRole((int)Session["IDRole"]) == 1)
                {
                    if (id.HasValue)
                    {
                        var role = dal.getByID(id.Value);
                        if (role != null)
                            return View(role);
                    }
                    return RedirectToAction("RoleUser", "Admin");
                }

                else
                {
                    return View("Error");
                }
            }
            catch { return View("Error"); }
        }

        [HttpPost]
        public ActionResult RoleUserDelete(int? id, Roles role)
        {
            if (id.HasValue)
                dal.Delete(id.Value);
            return RedirectToAction("RoleUser", "Admin");
        }

        //Edit Account
        //private UsersDAL dalUser = new UsersDAL();

        //public ActionResult Edit()
        //{
        //    try
        //    {
        //        string id = Session["ID"].ToString();
        //        if (id == null)
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        Users user = dalUser.getByID(id);
        //        if (user == null)
        //            return HttpNotFound();
        //        return View(user);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        //[HttpPost]
        //public ActionResult Edit(Users user)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            user.IDClass = Session["IDCLass"].ToString();
        //            user.IDRole = int.Parse(Session["IDRole"].ToString());
        //            dalUser.Update(user);
        //            return RedirectToAction("Index");
        //        }
        //        return View(user);
        //    }
        //    catch { user.LoginErrorMessage = "Error! Check input(Password)"; return View(user); }
        //}
    }
}