using System;
using System.Collections.Generic;
using System.IO;
using ServerConsole.source.exceptions;

namespace ServerConsole.source
{
    class Tools
    {
        public Tools()
        {
            ;
        }

        public bool Exist(string table, Variables var)
        {
            string path = @"Missions\" + var.MissionName + ".db";
            return (File.Exists(path));
        }

        //Cut table name off rest of arguments
        public void Divide(List<string> args, Variables var)
        {
            var.MissionName = "";
            var.Arguments = new List<string>();

            for (var i = 0; i < args.Count; i++)
                switch (i)
                {
                    case 0:
                        var.MissionName = args[i];
                        break;
                    case 1:
                        var.Parameter = args[i];
                        break;
                    default:
                        var.Arguments.Add(args[i]);
                        break;
                }
        }

        //Cut table name off time (using in ReciveUpdate)
        public void GetTime(List<string> args, Variables var)
        {
            try
            {
                var.MissionName = args[0];
                var.Parameter = args[1];
                double.TryParse(args[2], out var time0);
                var.Time = Convert.ToDouble(time0);
            }
            catch
            {
                throw new InvalidParametersException("Invalid parameter was given");
            }
        }

        public void ConnectWithMission(string mission, Variables var)
        {
            try
            {
                var path = @"Missions\" + mission + ".db";
                var.Db = new DB(path);
            }
            catch (Exception e)
            {
                var.Answer = "!" + e.Message;
            }
        }

        public string ToAnswer(string ans, Variables var)
        {
            var.Answer = "";
            return ans;
        }
    }
}
