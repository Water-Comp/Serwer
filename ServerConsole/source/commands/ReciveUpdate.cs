using System;
using System.Collections.Generic;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;

namespace ServerConsole.source.commands
{
    class ReciveUpdate : Command
    {
        public ReciveUpdate(Connection connection)
        {
            Me = "ReciveUpdate";
            this.connection = connection;
        }
        //Send new data to client, arguments: table name and lastest value of time
        protected override void Execute(List<string> args)
        {
            try
            {

                T.GetTime(args, Var);
                if (Var.MissionName == "") throw new InvalidNameOfMissionException("Name of mission was not found");
                T.ConnectWithMission(Var.MissionName, Var);
                string tmp = Var.Db.Query("Select MAX(time) from " + Var.Parameter);
                var maxTime = int.Parse(tmp);
                if (maxTime < Var.Time) throw new InvalidParametersException("Time is bigger than max");
                
                var sql = "SELECT * FROM " + Var.Parameter + " WHERE time > " + Var.Time;
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
