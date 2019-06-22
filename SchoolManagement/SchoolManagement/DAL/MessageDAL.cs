using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using SchoolManagement.Models;
using PagedList;

namespace SchoolManagement.DAL
{
    public class MessageDAL
    {
        private SchoolManagementEntities db;

        public MessageDAL()
        {
            db = new SchoolManagementEntities();
        }

        public IEnumerable<MessageContact> getMessage()
        {
            return db.MessageContact.OrderBy(m => m.DateSend).ToList();
        }

        public IEnumerable<MessageContact> getMessage(int? page, int pageSize)
        {
            if (!page.HasValue)
                page = 1;

            return db.MessageContact.OrderByDescending(m => m.DateSend).ToList().ToPagedList(page.Value, pageSize);
        }


        public Users getAdmin(string ID)
        {
            return db.Users.Find(ID);
        }

        public MessageContact getContactById(int id)
        {
            return db.MessageContact.Find(id);
        }

        public void AddRep(int id, string idAdmin, string message)
        {
            MessageReponse value = new MessageReponse();

            value.ID = id;
            value.IDAdmin = idAdmin;
            value.MessageAdmin = message;
            value.DateRep = DateTime.Now;

            db.MessageReponse.Add(value);

            db.SaveChanges();
        }

        public void SendMail(string Mailfrom, string Password, string MailTo, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage(Mailfrom, MailTo, subject, message);

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(Mailfrom, Password);
            smtp.Send(mailMessage);

        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}