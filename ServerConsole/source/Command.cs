using System;
using System.Collections.Generic;
using ServerConsole.source.lib;

namespace ServerConsole.source
{
    internal abstract class Command
    {
        public static List<string> Commands = new List<string>();
        public  Variables Var = new Variables();
        public Tools T = new Tools();
        protected Command command;
        protected string Me;
        protected Connection connection;

        public void SetNext(Command command)
        {
            this.command = command;
        }

        public void Answer(string message)
        {
            Console.WriteLine(message);
            connection.Respond(message);
            //have to add answer to logs
        }

        public void Next(string insert)
        {
            var args = new List<string>();
            var splited = insert.Split(' ');
            for (var i = 1; i < splited.Length; i++) args.Add(splited[i]);

            if (splited[0] == Me)
            {
                Execute(args);
            }
            else if (command != null) command.Next(insert);
            else Console.WriteLine("No such command");
        }

        protected abstract void Execute(List<string> args);
    }

    class Variables
    {
        //global variable using to send answer to client
        public string Answer = "";
        public List<string> Arguments;
        public DB Db;

        //global variables using in method Devide(), GetTime() or GetIMG()
        public string MissionName = "";
        public string Parameter;
        public double Time;

        public Variables()
        {
            ;
        }
    }
}