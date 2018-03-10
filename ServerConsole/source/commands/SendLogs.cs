using System;
using System.Collections.Generic;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    class SendLogs : Command
    {
        public SendLogs(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "SendLogs";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
        }

        protected override void Execute(List<string> args)
        {
            try
            {
                T.Divide(args, Var, true);
                T.ConnectWithMission(Var.MissionName, Var);
                for (var i = 0; i < Var.Arguments.Count; i++)
                {
                    if (i % 2 != 1) continue;
                    string message = returnAnswer.stringIP+"_"+Var.Arguments[i];
                    var time = Var.Arguments[i - 1];
                    string sql = "INSERT INTO Logs VALUES (" + time + ", '" + message + "')";

                    Var.Db.Query(sql);
                }

                recive = Me + "_";
                respond = "OK";
                Answer(Answers.Succesful);
            }
            catch (Exception e)
            {
                ExceptionTransform(e.Message);
                recive = Me + "_";
                respond = exceptionMsg;
                Answer("!" + e.Message);
            }
            finally
            {
                Log.Add(returnAnswer.stringIP, recive, respond, Var.Db);
            }
        }
    }
}
