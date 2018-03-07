using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerConsole.source.lib
{
    static class Log
    {
        public static void Recive(string message, DB mission)
        {
            DateTime time = DateTime.Now;

            var culture = new CultureInfo("fr-FR");
            string timeString = time.ToString(culture);
            string timeToDB = "";
            for (var i = 11; i<timeString.Length; i++)
            {
                timeToDB += timeString[i];
            }
            Console.WriteLine(timeToDB);

            string sqlLogAdd = "INSERT INTO Logs (Time, Value) VALUES ('" + timeToDB + "', '" + message + "')";
            mission.Query(sqlLogAdd);
        }
    }
}
