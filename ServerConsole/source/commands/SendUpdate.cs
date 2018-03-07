using System;
using System.Collections.Generic;
using System.Globalization;
using ServerConsole.source.lib;

namespace ServerConsole.source.commands
{
    internal class SendUpdate : Command
    {
        public SendUpdate(Connection connection)
        {
            Me = "SendUpdate";
            this.connection = connection;
        }
        protected override void Execute(List<string> args)
        {
            try
            {
                T.Divide(args, Var);
                T.ConnectWithMission(Var.MissionName, Var);
                for (var i = 0; i < Var.Arguments.Count; i++)
                {
                    if (i % 2 != 1) continue;
                    string sql;
                    var time = int.Parse(Var.Arguments[i - 1]);
                    if (Var.Parameter == "Images")
                    {
                        var valueImg = Var.Arguments[i];
                        sql = "INSERT INTO " + Var.Parameter + " VALUES (" + time + ", '" + valueImg + "')";
                    }
                    else
                    {
                        var value = float.Parse(Var.Arguments[i], CultureInfo.InvariantCulture.NumberFormat);
                        sql = "INSERT INTO " + Var.Parameter + " VALUES (" + time + ", " + value + ")";
                    }

                    Var.Db.Query(sql);
                }
                Answer(Answers.Succesful);
            }
            catch (Exception e)
            {
                Answer("!" + e.Message);
            }
        }
    }
}
