using System;
using System.Collections.Generic;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    internal class SendUpdate : Command
    {
        public SendUpdate(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "SendUpdate";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
        }
        protected override void Execute(List<string> args)
        {
            try
            {
                T.Divide(args, Var);
                if (!T.Exist(args[0], Var)) throw new InvalidNameOfMissionException("Mission does not exist");
                T.ConnectWithMission(Var.MissionName, Var);
                for (var i = 0; i < Var.Arguments.Count; i++)
                {
                    if (i % 2 != 1) continue;
                    string sql;
                    var time = Var.Arguments[i - 1];
                    if (Var.Parameter == "Images")
                    {
                        var valueImg = Var.Arguments[i];
                        sql = "INSERT INTO " + Var.Parameter + " VALUES (" + time + ", '" + valueImg + "')";
                    }
                    else
                    {
                        var value = Var.Arguments[i];
                        sql = "INSERT INTO " + Var.Parameter + " VALUES (" + time + ", " + value + ")";
                    }

                    Var.Db.Query(sql);
                }

                recive = Me + "_" + Var.Parameter;
                respond = "OK";
                Answer(Answers.Succesful);
            }
            catch (Exception e)
            {
                ExceptionTransform(e.Message);
                recive = Me + "_" + Var.Parameter;
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
