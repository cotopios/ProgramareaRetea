using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class ClientSocket
    {
        private readonly Socket _clientSocket;
        //private readonly IPEndPoint _remoteEndPoint;
        public ClientSocket()
        {
            //var ipAddress = IPAddress.Parse(remoteIP);
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //_remoteEndPoint = new IPEndPoint(ipAddress, remotePort);
        }

        public void Connect(string remoteIP, int remotePort)
        {
            var ipAddress = IPAddress.Parse(remoteIP);
            var remoteEndPoint = new IPEndPoint(ipAddress, remotePort);
            try
            {
                _clientSocket.Connect(remoteEndPoint);
                Console.WriteLine($"Client connected to: {remoteEndPoint}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error connecting: {e.Message}");
            }
        }
        public void SendLoop()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Your message: ");
                    string text = Console.ReadLine() ?? "";

                    byte[] bytesData = Encoding.UTF8.GetBytes(text);

                    _clientSocket.Send(bytesData);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}