using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolManagement.Models;

namespace SchoolManagement.DAL
{
    public class ContactDAL
    {
        private SchoolManagementEntities db = new SchoolManagementEntities();

        public ContactDAL()
        {
            db = new SchoolManagementEntities();
        }

        public void Add(string email, string message)
        {
            MessageContact messageContact = new MessageContact();
            messageContact.Email = email;
            messageContact.DateSend = DateTime.Now;
            messageContact.MessageGuest = message;
            messageContact.isRep = false;

            db.MessageContact.Add(messageContact);
            Save();
        }

        private void Save()
        {
            db.SaveChanges();
        }
    }
}