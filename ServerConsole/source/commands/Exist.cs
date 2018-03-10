using System;
using System.Collections.Generic;
using System.IO;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    class Exist : Command
    {
        public Exist(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "Exist";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
        }

        protected override void Execute(List<string> args)
        {
            try
            {
                T.Divide(args, Var);
                string path = @"Missions\" + Var.MissionName + ".db";
                Answer(File.Exists(path) ? "Yes" : "No");
            }
            catch (Exception e)
            {
                Answer("!" + e.Message);
            }
        }
    }
}
