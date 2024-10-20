using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Extensions
{
    public static class DateTimeExtension
    {
        public static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy HH:mm:ss");
        }
    }
}
