using System;
using System.Collections.Generic;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    class ReciveSettings : Command
    {
        public ReciveSettings(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "ReciveSettings";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
        }

        protected override void Execute(List<string> args)
        {
            try
            {
                T.Divide(args, Var, true);
                T.ConnectWithMission(Var.MissionName, Var);
                string notEmpty = Var.Db.Query("SELECT * FROM Settings");
                var splited = new List<string>(notEmpty.Split(' '));
                splited.RemoveAt(splited.Count - 2);
                string makeAnswer = "";
                foreach (var s in splited)
                {
                    makeAnswer += s + " ";
                }

                makeAnswer = makeAnswer.Remove(makeAnswer.Length - 2);
                Answer(makeAnswer);
            }
            catch (NullReferenceException e)
            {
                Answer(e.Message);
            }
            catch (Exception e)
            {
                ExceptionTransform(e.Message);
                recive = Me + "_" + Var.MissionName;
                respond = exceptionMsg;
                Answer("!" + e.Message);
            }
        }
    }
}
