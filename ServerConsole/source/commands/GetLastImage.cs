using System;
using System.Collections.Generic;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;

namespace ServerConsole.source.commands
{
    class GetLastImage : Command
    {
        public GetLastImage(Connection connection)
        {
            Me = "GetLastImage";
            this.connection = connection;
        }
        //Send lastest image of mission: arguments: table name
        protected override void Execute(List<string> args)
        {
            try
            {
                if (!T.Exist(args[0], Var)) throw new InvalidNameOfMissionException("Mission do not exist");
                T.ConnectWithMission(args[0], Var);
                const string sqlT = "SELECT MAX(Time) FROM Images";
                var sql = "SELECT Image FROM Images WHERE Time = " + Var.Db.Query(sqlT);
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
