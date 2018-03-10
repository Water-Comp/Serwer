using System;
using System.Collections.Generic;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    class CheckTopicality : Command
    {
        public CheckTopicality(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "CheckTopicality";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
        }
        protected override void Execute(List<string> args)
        {
            try
            {
                T.GetTime(args, Var);
                if (!T.Exist(Var.MissionName, Var))
                    throw new InvalidNameOfMissionException("Name of mission was not found or was incorrect");
                T.ConnectWithMission(Var.MissionName, Var);
                var sql = "SELECT MAX (time) FROM" + Var.Parameter;
                string answer = double.Parse(Var.Db.Query(sql)) <= Var.Time ? "Yes" : "No";
                Answer(answer);
            }
            catch (InvalidNameOfMissionException e)
            {
                Answer("!" + e.Message);
            }
            catch (InvalidNumberOfParametersException e)
            {
                Answer("!" + e.Message);
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
