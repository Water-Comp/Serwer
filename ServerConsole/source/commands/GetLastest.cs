using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerConsole.source.lib;

namespace ServerConsole.source.commands
{
    class GetLastest : Command
    {
        public GetLastest(Connection connection)
        {
            Me = "GetLastest";
            this.connection = connection;
        }

        protected override void Execute(List<string> args)
        {
            try
            {
                T.Divide(args, Var);
                T.ConnectWithMission(Var.MissionName, Var);
                var sqlTime = "SELECT MAX(time) FROM " + Var.Parameter;
                var maxTime = int.Parse(Var.Db.Query(sqlTime));
                var sql = "SELECT * FROM " + Var.Parameter + " WHERE time = " + maxTime;
                string answer = Var.Db.Query(sql);
                Answer(answer);
            }
            catch (Exception e)
            {
                Answer("!" + e.Message);
            }
        }
    }
}
