using System;
using System.Collections.Generic;
using System.IO;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;
using ServerConsole.source.others;

namespace ServerConsole.source.commands
{
    class CheckMemory : Command
    {
        public CheckMemory(Connection connection, ReturnAnswer returnAnswer)
        {
            Me = "CheckMemory";
            this.connection = connection;
            this.returnAnswer = returnAnswer;
        }

        protected override void Execute(List<string> args)
        {
            try
            {
                var path = Path.GetFullPath("ServerLogic.exe");
                var disk = "";
                for (var i = 0; i < 3; i++) disk += path[i];
                var drive = new DriveInfo(disk);
                var freeSpace = drive.AvailableFreeSpace;
                float freeSpaceF = freeSpace / 1024; /*from bytes to kilobytes*/
                freeSpaceF = freeSpaceF / 1024; /*from kilobytes to megabytes*/
                if (freeSpaceF > 1024)
                {
                    freeSpaceF = freeSpaceF / 1024; /*from megabytes to gigabytes*/
                    string answer = Math.Round(freeSpaceF, 2) + " GB of free space";
                    Answer(answer);
                }
                else if (freeSpaceF < 10)
                {
                    throw new LowMemoryException("It is only " + Math.Round(freeSpaceF) + " MB of free space");
                }
                else if (freeSpaceF < 1)
                {
                    throw new LowMemoryException("It is only " + Math.Round(1024*freeSpaceF) + " KB of free space!");
                }
                else
                {
                    string answer = Math.Round(freeSpaceF) + " MB of free space";
                    Answer(answer);
                }
            }
            catch (Exception e)
            {
                ExceptionTransform(e.Message);
                recive = Me + "_";
                respond = exceptionMsg;
                Answer("!" + e.Message);
                Log.Add(returnAnswer.stringIP, recive, respond, Var.Db);
            }
        }
    }
}
