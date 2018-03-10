using System;
using System.Collections.Generic;
using ServerConsole.source;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole
{
    class Create : Command
    {
        public Create(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "Create";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
        }

        protected override void Execute(List<string> args)
        {
            try
            {
                Var.MissionName = args[0];
                if (Var.MissionName == "") throw new InvalidParametersException("Name of mission was not found");
                if (int.TryParse(Var.MissionName, out int exc))
                    throw new InvalidNameOfMissionException("Invalid name of mission");
                if (T.Exist(Var.MissionName, Var)) throw new InvalidNameOfMissionException("Mission already exist");
                T.ConnectWithMission(Var.MissionName, Var);
                //Create new database in folder bin\Debug\Missions
                var path = @"Missions\" + Var.MissionName + ".db";
                Var.Db = new DB(path);

                //String structure
                string structure = "Images Logs tmp1 tmp2 press1 press2 gyrox gyroy gyroz";

                string[] parameters = structure.Split(' ');

                /*Create tables of parameters*/
                foreach (var parameter in parameters)
                {
                    string sql;
                    if (parameter != "Logs")
                    {
                        if (parameter == "Images")
                            sql = "CREATE TABLE " + parameter + " (Time real, Value text)";
                        else sql = "CREATE TABLE " + parameter + " (Time real, Value real)";
                        Var.Db.Query(sql);
                        var sqlFill = "INSERT INTO " + parameter + " VALUES ('0', '0')";
                        Var.Db.Query(sqlFill);
                    }
                    else
                    {
                        sql = "CREATE TABLE Logs (Time real, Value text)";
                        Var.Db.Query(sql);
                    }
                }

                /*Create table of other things*/
                string sqlOth = "CREATE TABLE Other (param text, value text)";
                Var.Db.Query(sqlOth);

                /*Initialize time of start*/
                string time = DateTime.Now.ToString("hh:mm:ss");
                string sqlTime = "INSERT INTO Other VALUES ('start', '" + time + "')";
                Var.Db.Query(sqlTime);

                /*Create table of settings*/
                string sqlSettings = "CREATE TABLE Settings (";
                int settingsSize = 10;

                for (int i = 0; i < settingsSize; i++)
                {
                    sqlSettings += "value" + (i+1) + " real";
                    if (i + 1 < settingsSize) sqlSettings += ", ";
                    else sqlSettings += ", actual integer)";
                }
                Var.Db.Query(sqlSettings);

                string sqlSettingsFill = "INSERT INTO Settings VALUES (";
                for (int i = 0; i < settingsSize; i++)
                {
                    sqlSettingsFill += "0";
                    if (i + 1 < settingsSize) sqlSettingsFill += ", ";
                    else sqlSettingsFill += ", 0)";
                }

                Var.Db.Query(sqlSettingsFill);



                //Make an answer
                recive = Me + "_" + Var.MissionName;
                respond = "OK";
                Answer(Answers.Succesful);
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
