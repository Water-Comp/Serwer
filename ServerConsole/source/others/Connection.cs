using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ServerConsole.source.others;

namespace ServerConsole.source.lib
{
    class Connection
    {
        private static Socket s;
        private TcpListener myList;
        public ReturnAnswer returnAnswer;

        public IPEndPoint localIpEndPoint;
        // Constructor
        public Connection()
        {
            
        }

        public void init(string ip, int port, ReturnAnswer returnAnswer)
        {
            IPAddress ipAd = IPAddress.Parse(ip);
            /* Initializes the Listener */
            myList = new TcpListener(ipAd, port);
            /* Start Listeneting at the specified port */
            myList.Start();
            this.returnAnswer = returnAnswer;
        }


        /*Creating connection*/
        public void Connect()
        {
            /* Accept connection*/
            try
            {
                s = myList.AcceptSocket();
                returnAnswer.stringIP = IPAddress.Parse(((IPEndPoint) s.LocalEndPoint).Address.ToString()).ToString();
            }
            catch (Exception)
            {
                
            }
        }

        /* Reciving messeage from client*/
        public string Recive()
        {
            try
            {
                byte[] b = new byte[500000000];
                int k = s.Receive(b);
                string wynik = "";
                for (int i = 0; i < k; i++)
                    wynik = wynik + Convert.ToChar(b[i]);
                localIpEndPoint = (IPEndPoint) s.LocalEndPoint;
                wynik = wynik.Replace(',', '.');
                return wynik;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /* Sending respond to client*/
        public void Respond(string respond)
        {
            try
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes(respond));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public int Requests()
        {
            return s.Available;
        }
    }
}

