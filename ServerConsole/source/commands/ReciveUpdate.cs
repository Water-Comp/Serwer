using System;
using System.Collections.Generic;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    class ReciveUpdate : Command
    {
        public ReciveUpdate(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "ReciveUpdate";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
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
                var maxTime = double.Parse(tmp);
                string time = Var.Time.ToString();
                time = time.Replace(',', '.');
                if (maxTime < Var.Time) throw new InvalidParametersException("Time is bigger than max");
                var sql = "SELECT * FROM " + Var.Parameter + " WHERE time > " + time;
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
