using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Helper
{
    public static class DateHelper
    {
        public static string DateToString(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();

            try

            {
                return (pc.GetYear(dt) + "/" + pc.GetMonth(dt) + "/" + pc.GetDayOfMonth(dt));
            }

            catch

            {
                throw;
            }
        }

        public static string ToPersianDate(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();

            try

            {
                return (pc.GetYear(date) + "/" + pc.GetMonth(date) + "/" + pc.GetDayOfMonth(date));
            }

            catch

            {
                return "";
            }
        }

        public static string ToPersianTime(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();

            try

            {
                return (pc.GetHour(date) + ":" + pc.GetMinute(date));
            }

            catch

            {
                throw;
            }
        }

        public static string ConvertNumbersToPersian(this string str)
        {
            try
            {
                return str.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "v").Replace("8", "۸").Replace("9", "۹");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

    }
}
