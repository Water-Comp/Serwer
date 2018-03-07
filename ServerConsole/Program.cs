using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerConsole.source;
using ServerConsole.source.commands;
using ServerConsole.source.lib;

namespace ServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {   
            
            //Initialize connection
            string IP;
            bool tmp = true;
            Console.WriteLine("Podaj IP:");
            Connection connection = new Connection();

            while (tmp)
            {
                IP = Console.ReadLine();
                try
                {
                    connection.init(IP, 8001);
                    tmp = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("wrong ip");
                }
            }

            //Initailize commands
            Command create = new Create(connection);
            Command sendupdate = new SendUpdate(connection);
            Command reciveupdate = new ReciveUpdate(connection);
            Command getlastimage = new GetLastImage(connection);
            Command checktopicality = new CheckTopicality(connection);
            Command getlastest = new GetLastest(connection);
            Command list = new List(connection);
            Command checkmemory = new CheckMemory(connection);
            Command exist = new Exist(connection);
            Command getlogs = new GetLogs(connection);

            //Make a one-way list
            create.SetNext(sendupdate);
            sendupdate.SetNext(reciveupdate);
            reciveupdate.SetNext(getlastimage);
            getlastimage.SetNext(checktopicality);
            checktopicality.SetNext(getlastest);
            getlastest.SetNext(list);
            list.SetNext(checkmemory);
            checkmemory.SetNext(exist);
            exist.SetNext(getlogs);

            string insert;


            do
            {
                //connection.Connect();
                //insert = connection.Recive();
                insert = Console.ReadLine();
                Console.WriteLine(insert);
                create.Next(insert);
            } while (insert != "exit");
            
        }
    }
}
