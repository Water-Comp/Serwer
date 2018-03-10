using System;
using System.Collections.Generic;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    class CheckSettings : Command
    {
        public CheckSettings(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "CheckSettings";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
        }

        protected override void Execute(List<string> args)
        {
            try
            {
                Var.MissionName = args[0];
                T.ConnectWithMission(Var.MissionName, Var);
                recive = Me + "_" + Var.MissionName;
                string actual = Var.Db.Query("SELECT actual FROM Settings");
                int intActual = int.Parse(actual);
                if (intActual == 1)
                {
                    Answer("Empty");
                    respond = "Empty";
                }
                else
                {
                    string notEmpty = Var.Db.Query("SELECT * FROM Settings");
                    var splited = new List<string>(notEmpty.Split(' '));
                    splited.RemoveAt(splited.Count - 2);
                    string makeAnswer = "";
                    foreach (string s in splited)
                    {
                        makeAnswer += s + " ";
                    }

                    makeAnswer = makeAnswer.Remove(makeAnswer.Length - 2);

                    Answer(makeAnswer);
                    Var.Db.Query("UPDATE Settings SET actual = 1");
                    respond = makeAnswer.Replace(' ', '-');
                }

                Log.Add(returnAnswer.stringIP, recive, respond, Var.Db);
            }
            catch (NullReferenceException e)
            {
                Answer("!" + e.Message);
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
