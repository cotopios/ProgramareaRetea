using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class ServerSocket
    {
        List<Socket> clients = new List<Socket>();
        private readonly Socket _serverSocket;
        private readonly IPEndPoint _serverEndPoint;
        public ServerSocket(string ip, int port)
        {
            var ipAdress = IPAddress.Parse(ip);
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //var ipAdress = IPAddress.Parse(ip);
            _serverEndPoint = new IPEndPoint(ipAdress, port);
        }
        public void BindAndListen(int queueLimit)
        {
            try
            {
                _serverSocket.Bind(_serverEndPoint);
                _serverSocket.Listen(queueLimit);
                Console.WriteLine($"Server listening on: {_serverEndPoint}");
            }
            /*            catch ()
                        {

                        }*/
            catch (Exception e)
            {
                Console.WriteLine($"Error binding and listening: {e.Message}");
            }
        }
        public void AcceptAndReceive()
        {
            while (true)
            {
                Socket? client;

                client = acceptClient();

                if (client != null)
                {
                   
                    Console.WriteLine($"{client.RemoteEndPoint} conected");

                    lock (clients)
                    {
                        clients.Add(client);
                    }

                    Thread clientThread = new Thread(() => receiveLoop(client));
                    clientThread.Start();
                }
            }
        }
        private Socket? acceptClient()
        {
            Socket? client = null;

            try
            {
                client = _serverSocket.Accept();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error accepting client: {e.Message}");
                //return;
            }

            return client;

        }
        private void receiveLoop(Socket client)
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesCount = client.Receive(buffer);
                    if (bytesCount == 0)
                    {
                        Console.WriteLine("Disconected");
                        return;
                    }
                    string text = Encoding.UTF8.GetString(buffer, 0, bytesCount);
                    Console.WriteLine($"From {client.RemoteEndPoint} : {text}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error receiving data from : {client.RemoteEndPoint}");
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }
    }
}