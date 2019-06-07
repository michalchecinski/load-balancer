using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Logs.Web.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static DateTime TruncateToWholeMinute(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 99);
        }
    }
}
