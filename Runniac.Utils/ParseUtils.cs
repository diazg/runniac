using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Runniac.Utils
{
    public static class ParseUtils
    {
        /// <summary>
        /// Convierte una fecha y hora en el formato utilizado por la Web finixer.com en un objeto fecha de 
        /// .NET. El formato de una fecha en esta web sería 2014-4-6T09:30+01:00
        /// </summary>
        /// <param name="dateTime">Cadena de texto con la fecha y hora.</param>
        /// <returns>La fecha y hora en formato DateTime.</returns>
        public static DateTime? ParseFinixerDateFormat(string dateTime)
        {
            var date = dateTime.Split('T', '+');
            DateTime? dateConverted;

            try
            {
                dateConverted = DateTime.ParseExact(String.Format("{0} {1}", date[0], date[1]), "yyyy-M-d hh:mm",
                    CultureInfo.InvariantCulture);
            }
            catch
            {
                dateConverted = DateTime.ParseExact(date[0], "yyyy-M-d", CultureInfo.InvariantCulture);
            }
            return dateConverted;
        }

        public static DateTime? ParseDate(string dateTime)
        {
            DateTime? dateConverted = null;

            if (!String.IsNullOrEmpty(dateTime))
            {
                try
                {
                    dateConverted = DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm", null);
                }
                catch
                {
                    try
                    {
                        dateConverted = DateTime.ParseExact(dateTime, "dd/MM/yyyy", null);
                    }
                    catch
                    {
                        dateConverted = DateTime.Parse(dateTime);
                    }
                }
            }
            return dateConverted;
        }


        public static float ParseVamosACorrerDistance(string distanceKms)
        {
            if (String.IsNullOrEmpty(distanceKms))
                return 0;

            var distance = distanceKms.Trim().Split(' ');

            if (distance == null || distance.Length == 0)
                return 0;

            return float.Parse(Regex.Match(distance[0], @"\d+").Value);
        }

        public static float ParseFinixerDistance(string distanceKms)
        {
            if (String.IsNullOrEmpty(distanceKms))
                return 0;

            var distance = distanceKms.Trim().Split(' ');
            return float.Parse(distance[0].Replace(".", string.Empty)) / 1000;
        }

        public static int ParseTodoCarrerasDistance(string distance)
        {
            if (String.IsNullOrEmpty(distance))
                return 0;

            var auxDistance = distance.Trim();

            switch (auxDistance)
            {
                case "10km":
                    return 10;

                case "media maratón":
                    return 21;

                case "maratón":
                    return 42;
            }
            return 0;
        }

        public static DateTime? ParseTodoCarrerasDateFormat(string textDate)
        {
            var auxDate = textDate.Replace("[", "").Remove(textDate.IndexOf("]") - 1).Trim();
            DateTime? dateConverted = null;

            try
            {
                dateConverted = DateTime.ParseExact(auxDate, "dd/MM/yyyy", null);
            }
            catch
            {
                try
                {
                    dateConverted = DateTime.ParseExact(auxDate, "dd/M/yyyy", null);
                }
                catch
                {
                    dateConverted = null;
                }
            }

            return dateConverted;
        }


        public static DateTime? ParseVamosACorrerDateFormat(string date)
        {
            DateTime? dateConverted = null;

            try
            {
                dateConverted = DateTime.Parse(date);
            }
            catch
            {
                dateConverted = null;
            }
            return dateConverted;
        }

        public static float ParseTodoCarrerasFee(string fee)
        {
            if (String.IsNullOrEmpty(fee))
                return 0;

            try
            {
                return float.Parse(fee.Trim().Replace("€", ""));
            }
            catch
            {
                return 0;
            }
        }



        private static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return string.Concat(char.ToUpper(s[0]), s.Substring(1).ToLower());
        }


    }
}
