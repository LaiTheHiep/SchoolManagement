using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Models;
using SchoolManagement.DAL;

namespace SchoolManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SendMessage(string ResponseEmail, string ResponseMessage)
        {
            try
            {
                ContactDAL dal = new ContactDAL();
                if (ResponseEmail == string.Empty || ResponseMessage == string.Empty)
                {
                    CheckDAL.MessageAlert("Check Email or Message. They not null!");
                    return View("Index");                   
                }
                else if (!CheckDAL.CheckEmail(ResponseEmail))
                {
                    CheckDAL.MessageAlert("Check Email");
                    return View("Index");
                }
                else
                {                   
                    dal.Add(ResponseEmail, ResponseMessage);
                    CheckDAL.MessageAlert("Sucessful! We will response after 1 day");
                    return View("Index");
                }
            }
            catch
            {
                CheckDAL.MessageAlert("Error! Please check Email!");
                return View("Index");
            }
        }
    }
}