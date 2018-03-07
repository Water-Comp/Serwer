using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ServerConsole.source.lib;

namespace ServerConsole.source.commands
{
    class Exist : Command
    {
        public Exist(Connection connection)
        {
            Me = "Exist";
            this.connection = connection;
        }

        protected override void Execute(List<string> args)
        {
            T.Divide(args, Var);
            string path = @"Missions\" + Var.MissionName + ".db";
            Answer(File.Exists(path) ? "Yes" : "No");
        }
    }
}
