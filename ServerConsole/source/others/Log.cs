using System;

namespace ServerConsole.source.lib
{
    static class Log
    {
        public static void Add(string IP, string recive, string respond, DB mission)
        {
            try
            {
                string time = mission.Query("SELECT value FROM Other WHERE param = 'start'");
                DateTime start = DateTime.Parse(time);
                DateTime now = DateTime.Parse(DateTime.Now.ToString("hh:mm:ss"));

                TimeSpan substraction = now - start;
                int hour = int.Parse(substraction.Hours.ToString());
                int minute = int.Parse(substraction.Minutes.ToString());
                int seconds = int.Parse(substraction.Seconds.ToString());

                seconds += minute * 60;
                seconds += hour * 3600;

                string message = IP + "_recive:" + recive + "_respond:" + respond;


                var sqlLogAdd = "INSERT INTO Logs (Time, Value) VALUES ('" + seconds + "', '" + message + "')";
                mission.Query(sqlLogAdd);
            }
            catch (Exception)
            {
                ;
            }

            /*****************
             message pattern:
             recive:#########_respond:########
             *****************/ 
        }
    }
}
