using Example_2.TheClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example_2.TheServer
{
    public class Server
    {

       
        private Socket serverSocket;
        private byte[] buffer = new byte[512];
        private List<Clients> theClients = new List<Clients>();
        Thread acceptingThread;
        private Action<string> GuiUpdater;
        


        public Server(string ip, int port, Action<string> guiUpdater)
        {
            
            GuiUpdater = guiUpdater;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            serverSocket.Listen(5);
            //clientSocket = serverSocket.Accept();

            StartAccepting();
        }

        private void StartAccepting()
        {
            acceptingThread = new Thread(new ThreadStart(Accept));
            acceptingThread.IsBackground = true;
            acceptingThread.Start();
        }

        private void Accept()
        {
            while (acceptingThread.IsAlive)
            {
                try
                {
                    theClients.Add(new Clients(serverSocket.Accept(), new Action<Socket, string>(NewMessageReceived)));
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void StopAccepting()
        {
            serverSocket.Close();
            acceptingThread.Abort();

            foreach (var item in theClients)
            {
                item.Close();
            }

            theClients.Clear();
        }

        public void NewMessageReceived(Socket senderSocket, string message)
        {
            GuiUpdater(message);

            foreach (var item in theClients)
            {
                if (item.ClientSocket != senderSocket)
                {
                    item.Send(message);
                }
            }
        }
    }
}
