using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ClientSocket client = new ClientSocket();
            client.Connect("192.168.43.16", 5050);
            client.SendLoop();
        }
    }
}