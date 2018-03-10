using System;
using System.Collections.Generic;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    class GetLastest : Command
    {
        public GetLastest(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "GetLastest";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
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
                ExceptionTransform(e.Message);
                recive = Me + "_" + Var.Parameter;
                respond = exceptionMsg;
                Answer("!" + e.Message);
                Log.Add(returnAnswer.stringIP, recive, respond, Var.Db);
            }
        }
    }
}
