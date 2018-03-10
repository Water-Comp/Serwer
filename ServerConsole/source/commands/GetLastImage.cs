using System;
using System.Collections.Generic;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    class GetLastImage : Command
    {
        public GetLastImage(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "GetLastImage";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
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
                ExceptionTransform(e.Message);
                recive = Me + "_" + Var.MissionName;
                respond = exceptionMsg;
                Answer("!" + e.Message);
                Log.Add(returnAnswer.stringIP, recive, respond, Var.Db);
            }
        }
    }
}
