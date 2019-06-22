using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.DAL;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(Users user)
        {
            try
            {
                using (SchoolManagementEntities db = new SchoolManagementEntities())
                {
                    string account = user.ID;
                    string password = CryptographyDAL.EncryptMD5(user.Password);
                    var userDetail = db.Users.Where(x => x.ID == account && x.Password == password).FirstOrDefault();
                    if (userDetail == null)
                    {
                        user.LoginErrorMessage = "Wrong Account or Password";
                        return View("Login", user);
                    }
                    else
                    {
                        Session["ID"] = userDetail.ID;
                        Session["Name"] = userDetail.Name;
                        Session["IDRole"] = userDetail.IDRole;
                        Session["IDCLass"] = userDetail.IDClass;
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            catch
            {
                return View("Login", user);
            }
        }


        public ActionResult LogOut()
        {
            Session.Abandon();

            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        private UsersDAL dal = new UsersDAL();

        public ActionResult Edit()
        {
            try
            {
                string id = Session["ID"].ToString();
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Users user = dal.getByID(id);
                if (user == null)
                    return HttpNotFound();
                return View(user);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(Users user)
        {
            try
            {
                if(!CheckDAL.CheckEmail(user.Email))
                {
                    user.LoginErrorMessage = "Check Email";
                    return View(user);
                }
                if (ModelState.IsValid)
                {
                    user.IDClass = Session["IDCLass"].ToString();
                    user.IDRole = int.Parse(Session["IDRole"].ToString());
                    dal.Update(user);
                    return RedirectToAction("Index", "Home");
                }
                return View(user);
            }
            catch { user.LoginErrorMessage = "Error! Check input(Password)"; return View(user); }
        }
    }
}