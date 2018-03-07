using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerConsole.source.exceptions;
using ServerConsole.source.lib;

namespace ServerConsole.source.commands
{
    class List : Command
    {
        public List(Connection connection)
        {
            Me = "List";
            this.connection = connection;
        }

        protected override void Execute(List<string> args)
        {
            try
            {
                var something = new List<string>();
                const string path = "Missions";
                var files = Directory.GetFiles(path, "*.db");

                var file = files.ToArray();
                foreach (var t1 in file)
                {
                    var oneFile = new List<char>(t1.ToArray());
                    for (var j = 0; j < 3; j++) oneFile.RemoveAt(oneFile.Count - 1);
                    oneFile.RemoveRange(0, path.ToArray().Length + 1);
                    var tmp = oneFile.Aggregate("", (current, t2) => current + t2);

                    something.Add(tmp);
                }

                var toAns = "";

                for (var i = 0; i < something.Count; i++)
                {
                    toAns += something[i];
                    if (i + 1 != something.Count) toAns += " ";
                }

                Answer(toAns);
            }
            catch (Exception e)
            {
                Answer("!" + e.Message);
            }
        }
    }
}
