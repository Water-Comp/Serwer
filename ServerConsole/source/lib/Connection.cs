using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerConsole.source.lib
{
    class Connection
    {
        private static Socket s;
        private TcpListener myList;

        public IPEndPoint localIpEndPoint;
        // Constructor
        public Connection()
        {
            
        }

        public void init(string ip, int port)
        {
            IPAddress ipAd = IPAddress.Parse(ip);
            /* Initializes the Listener */
            myList = new TcpListener(ipAd, port);
            /* Start Listeneting at the specified port */
            myList.Start();
        }


        /*Creating connection*/
        public void Connect()
        {
            /* Accept connection*/
            s = myList.AcceptSocket();
            

        }

        /* Reciving messeage from client*/
        public string Recive()
        {
            byte[] b = new byte[100];
            int k = s.Receive(b);
            string wynik = "";
            for (int i = 0; i < k; i++)
                wynik = wynik + Convert.ToChar(b[i]);
            localIpEndPoint = (IPEndPoint) s.LocalEndPoint;
            Console.WriteLine(localIpEndPoint.ToString());
            return wynik;

        }
        /* Sending respond to client*/
        public void Respond(string respond)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            s.Send(asen.GetBytes(respond));
        }

        public int Requests()
        {
            return s.Available;
        }
    }
}

