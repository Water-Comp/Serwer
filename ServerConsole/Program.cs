using System;
using ServerConsole.source;
using ServerConsole.source.commands;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ReturnAnswer returnAnswer = new ReturnAnswer();
            
            //Initialize connection
            bool tmp = true;
            Console.WriteLine("IP:");
            Connection connection = new Connection();

            while (tmp)
            {
                var IP = Console.ReadLine();
                try
                {
                    connection.init(IP, 8001, returnAnswer);
                    tmp = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong IP!");
                }
            }

            //Initailize commands
            Command create = new Create(connection, returnAnswer);
            Command sendupdate = new SendUpdate(connection, returnAnswer);
            Command reciveupdate = new ReciveUpdate(connection, returnAnswer);
            Command getlastimage = new GetLastImage(connection, returnAnswer);
            Command checktopicality = new CheckTopicality(connection, returnAnswer);
            Command getlastest = new GetLastest(connection, returnAnswer);
            Command list = new List(connection, returnAnswer);
            Command checkmemory = new CheckMemory(connection, returnAnswer);
            Command exist = new Exist(connection, returnAnswer);
            Command sendlogs = new SendLogs(connection, returnAnswer);
            Command set = new Set(connection, returnAnswer);
            Command checksettings = new CheckSettings(connection, returnAnswer);
            Command recivesettings = new ReciveSettings(connection, returnAnswer);

            //Make a one-way list
            create.SetNext(sendupdate);
            sendupdate.SetNext(reciveupdate);
            reciveupdate.SetNext(getlastimage);
            getlastimage.SetNext(checktopicality);
            checktopicality.SetNext(getlastest);
            getlastest.SetNext(list);
            list.SetNext(checkmemory);
            checkmemory.SetNext(exist);
            exist.SetNext(sendlogs);
            sendlogs.SetNext(set);
            set.SetNext(checksettings);
            checksettings.SetNext(recivesettings);

            string insert;


            do
            {
                connection.Connect();
                insert = connection.Recive();
                Console.WriteLine(insert);
                create.Next(insert);
            } while (insert != "exit");
            
        }
    }
}
