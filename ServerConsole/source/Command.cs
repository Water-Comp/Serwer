using System;
using System.Collections.Generic;
using ServerConsole.source.lib;
using ServerConsole.source.others;

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
        protected ReturnAnswer returnAnswer;
        protected string exceptionMsg;
        protected string recive = "";
        protected string respond = "";

        public void SetNext(Command command)
        {
            this.command = command;
        }

        public string Answer(string message)
        {
            Console.WriteLine(message);
            connection.Respond(message);
            //have to add answer to logs
            return message;
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
            else Answer("No such command");
        }

        protected void ExceptionTransform(string message)
        {
            exceptionMsg = "";
            foreach (var ch in message)
            {
                if (ch == ' ') exceptionMsg += '-';
                else exceptionMsg += ch;
            }
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