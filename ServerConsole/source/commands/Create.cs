using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerConsole.source;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;

namespace ServerConsole
{
    class Create : Command
    {
        public Create(Connection connection)
        {
            Me = "Create";
            this.connection = connection;
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
                    var sql = "CREATE TABLE " + parameter + " (Time real, Value real)";
                    var sqlFill = "INSERT INTO " + parameter + " VALUES ('0', '0')";
                    Var.Db.Query(sql);
                    Var.Db.Query(sqlFill);
                }

                //Make an answer
                Answer(Answers.Succesful);
            }
            catch (Exception e)
            {
                Answer("!" + e.Message);
            }
            finally
            {
                //Make log
                string log_content;
            }
        }
    }
}
