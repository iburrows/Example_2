using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example_2.TheClients
{
    class Clients
    {
        Action<Socket, string> Action;
        byte[] buffer = new byte[512];
        Thread clientReceiveThread;
        public Socket ClientSocket { get; set; }
        const string endMessage = "@quit";
        private int msgCount = 0;
        public string Name { get; set; }

        public int MsgCount
        {
            get { return msgCount; }
            set { msgCount = value; }
        }


        public Clients(Socket socket, Action<Socket, string> action)
        {
            this.ClientSocket = socket;
            this.Action = action;
            clientReceiveThread = new Thread(Receive);
            clientReceiveThread.Start();
            //StartReceiving();
        }

        private void StartReceiving()
        {
            clientReceiveThread = new Thread(new ThreadStart(Receive));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }

        private void Receive()
        {
            string msg = "";
            
            while (!msg.Equals(endMessage))
            {
                int length = ClientSocket.Receive(buffer);
                msg = Encoding.UTF8.GetString(buffer, 0, length);

                if (MsgCount == 0)
                {
                    Name = msg;
                    Action(ClientSocket, "New User: " + msg );
                    MsgCount++;
                }

                else { 
                    Action(ClientSocket, Name + ": " + msg);
                    MsgCount++;
                }
            }
            Close();
            }

        public void Send(string message)
        { 
            ClientSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        internal void Close()
        {
            Send(endMessage);
            ClientSocket.Close(1);
            clientReceiveThread.Abort();
        }
    }
}
