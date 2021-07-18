using System;
using client_connect;

namespace Client_connect
{
    class Program
    {
        static void Main(string[] args)
        {
            client_class p0 = new client_class();
            String get_msg;
            int stop_flag = 0;

            if (p0.client_set("Your IP (Ipv4)", 3000) == 1)
            {
                get_msg = p0.client_get();

                while (true)
                {
                    stop_flag = p0.client_send(Console.ReadLine());

                    // to break this while
                    if(stop_flag == 0) {
                        Console.WriteLine("Client Disconnect !!!");
                        break;
                    }

                }
            }

        }
    }
}
