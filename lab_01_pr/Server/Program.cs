using System;
using Server;
using Server;


/*ServerSocket server = new ServerSocket("127.0.0.1", 5050);
server.BindAndListen(15);*/

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ServerSocket server = new ServerSocket("192.168.43.16", 5050);
            server.BindAndListen(15);
            server.AcceptAndReceive();
        }
    }
}