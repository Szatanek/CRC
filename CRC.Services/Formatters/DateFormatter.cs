using System;

namespace CRC.Services.Formatters
{
    public static class DateFormatter
    {
        public static string ToShortDateTimeString(DateTime dateToFormat)
        {
            return dateToFormat.ToString("dd-MM-yyyy HH:mm");
        }
    }
}