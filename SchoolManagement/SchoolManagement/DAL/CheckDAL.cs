using System.Linq;
using System.Text.RegularExpressions;
using SchoolManagement.Models;

namespace SchoolManagement.DAL
{
    public static class CheckDAL
    {
        public static int CheckRole(int role)
        {
            if (role == 1)
                return 1;
            else if (role == 2)
                return 2;
            else if (role == 3)
                return 3;
            else
                return 4;
        }

        public static bool CheckActive(string _mssv, bool _class)
        {
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                var student = db.Users.Find(_mssv);
                if (_class)
                {
                    if (student == null)
                        return false;
                    return (bool)student.Classes.Active_Class;
                }
                else
                {
                    if (student == null)
                        return false;
                    return (bool)student.Classes.Active_Subject;
                }
            }
        }

        public static bool CheckLogin(string acc, string pass, out Users AccountLogin)
        {
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                var users = db.Users.Where(u => u.ID == acc && u.Password == pass).FirstOrDefault();
                AccountLogin = users;
                if (users != null)
                    return true;
                else
                    return false;
            }
        }

        public static bool CheckEmail(string email)
        {

            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
                return true;
            else
                return false;

        }

        //JavaScript : alert
        public static void MessageAlert(string message)
        {
            System.Web.HttpContext.Current.Response.Write(@"<script language=""JavaScript"">alert(""" + message + @""")</script>");
        }
    }
}