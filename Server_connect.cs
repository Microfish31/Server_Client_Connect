using System;
using Server_connect;

namespace Server_connect
{
    class Program
    {
        static void Main(string[] args)
        {
            server_class mainserver = new server_class();
            String get_msg;
            

            if (mainserver.server_set("Your IP (Ipv4)", 3000) == 1)
            {
                //獲取客戶端的socket，用來與客戶端通訊 新建立連接的新 Socket。
                mainserver.client = mainserver.server.Accept();
                
                //set client buffer max receive
                mainserver.client.ReceiveBufferSize = 65536;

                // set timeout 10 sec
                // mainserver.client.ReceiveTimeout = 10000;

                mainserver.server_send("你好，我是伺服端！");

                while (true)
                {
                    get_msg = mainserver.server_get();

                    if (get_msg != "xxxxx") {
                        mainserver.server_work_analysis(get_msg);
                    } else {
                        break;
                    }

                }
            }
        }
    }
}
