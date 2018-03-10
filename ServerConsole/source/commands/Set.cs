using System;
using System.Collections.Generic;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    class Set : Command
    {
        public Set(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "Set";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
        }

        protected override void Execute(List<string> args)
        {
            try
            {
                T.Divide(args, Var, true);
                T.ConnectWithMission(Var.MissionName, Var);
                string sqlRemove = "DELETE FROM Settings";
                Var.Db.Query(sqlRemove);
                string sql = "INSERT INTO Settings VALUES (";
                for (int i = 0; i < 10; i++)
                {
                    if (i < Var.Arguments.Count) sql += Var.Arguments[i];
                    else sql += "0";
                    sql += ", ";
                }

                sql += "0)";

                Var.Db.Query(sql);
                recive = Me + "_";
                respond = "OK";
                Answer("OK");
            }
            catch (Exception e)
            {
                ExceptionTransform(e.Message);
                recive = Me + "_" + Var.MissionName;
                respond = exceptionMsg;
                Answer("!" + e.Message);
            }
            finally
            {
                //Make log
                Log.Add(returnAnswer.stringIP, recive, respond, Var.Db);
            }
        }
    }
}
