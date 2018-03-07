using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;

namespace ServerConsole.source.commands
{
    class CheckTopicality : Command
    {
        public CheckTopicality(Connection connection)
        {
            Me = "CheckTopicality";
            this.connection = connection;
        }
        protected override void Execute(List<string> args)
        {
            try
            {
                T.GetTime(args, Var);
                T.ConnectWithMission(Var.MissionName, Var);
                var sql = "SELECT * FROM " + Var.Parameter + " WHERE time > " + Var.Time;
                string answer = Var.Db.Query(sql) == "" ? "Yes" : "No";
                Answer(answer);
            }
            catch (Exception e)
            {
                string answer = "!" + e.Message;
                Answer(answer);
            }
        }
    }
}
