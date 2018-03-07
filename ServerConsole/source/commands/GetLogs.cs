using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerConsole.source.lib;

namespace ServerConsole.source.commands
{
    class GetLogs : Command
    {
        public GetLogs(Connection connection)
        {
            Me = "GetLogs";
            this.connection = connection;
        }

        protected override void Execute(List<string> args)
        {
            T.Divide(args, Var);
            T.ConnectWithMission(Var.MissionName, Var);
            Log.Recive("Hej", Var.Db);
        }
    }
}
