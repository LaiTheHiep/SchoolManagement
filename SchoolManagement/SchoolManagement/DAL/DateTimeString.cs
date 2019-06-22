using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SchoolManagement.DAL
{
    public class DateTimeString
    {
        public string Day { get; set; }

        public string TimeStart { get; set; }

        public string TimeEnd { get; set; }

    }

    public class ChangeString
    {

        //Covert string example:  T2, 6h45-9h05
        public static DateTimeString CovertDate(string date)
        {
            string s = date;
            //day
            string day = s.Substring(0, 2);
            //TimeStrart and TimeEnd
            string time = s.Substring(4);
            List<string> listTime = time.Split('-').ToList();

            DateTimeString dateTime = new DateTimeString();
            dateTime.Day = day;
            dateTime.TimeStart = listTime[0];
            dateTime.TimeEnd = listTime[1];
            return dateTime;
        }

        //Check fomat example: T2, 00h00-00h00 or T7, 0h00-0h00
        //Check TimeStrart < TimeEnd
        public static bool CheckFomat(string s)
        {
            string strRegex = @"T[2-7], ([1-9]|(1[0-7]))h[0-9]{2}-([1-9]|(1[0-7]))h[0-9]{2}";
            Regex regex = new Regex(strRegex);
            if (regex.IsMatch(s))
            {
                var date = CovertDate(s);
                if (StringToMinutes(date.TimeStart) >= StringToMinutes(date.TimeEnd))
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        //GhPP => int = G * 60 + PP
        public static int StringToMinutes(string s)
        {
            List<string> list = s.Split('h').ToList();

            int Hours = int.Parse(list[0]);
            int Minutes = int.Parse(list[1]);

            return Hours * 60 + Minutes;
        }

        //2 DateTimeString compare TimeStrart
        public static bool CheckAdd(DateTimeString oldTime, DateTimeString newTime)
        {
            //Different day
            if (oldTime.Day != newTime.Day)
                return true;
            //Time Before
            if (StringToMinutes(newTime.TimeEnd) < StringToMinutes(oldTime.TimeStart))
                return true;
            //Time After
            if (StringToMinutes(newTime.TimeStart) > StringToMinutes(oldTime.TimeEnd))
                return true;

            return false;
        }

    }
}
