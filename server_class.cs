using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server_connect
{
    public class server_class
    {
        //建立伺服端
        public Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Socket client;
        public static int get_data_size = 1024;
        public static int send_data_size = 1024;

        //用來接客戶端的資料 type  = byte
        public byte[] get_byte_data = new byte[get_data_size];

        //用來送客戶端的資料 type  = byte
        public byte[] send_byte_data = new byte[send_data_size];

        public int get_byte_length;

        // msg type = string
        public string get_message;
        public string send_message;

        public int server_set(String ip_address, int port)
        {
            // Input Data
            // Client ID        type int  <try>
            // "Your IP (Ipv4)" type String
            // 3000             type int

            get_byte_length = 0;

            // 建立IP地址（IP地址和埠號是伺服器端的，用來和伺服器進行通訊）
            IPAddress ip = IPAddress.Parse(ip_address);
            EndPoint ep = new IPEndPoint(ip, port);

            // listen()只有在bind()通話後才能工作。
            // Your IP (Ipv4):port 繫結EP
            server.Bind(ep);

            // 伺服器開始監聽
            // public void Listen (int32 backlog);
            // 設定最大連線數量為10
            server.Listen(10);

            // set max buffer
            server.ReceiveBufferSize = 65536;

            // set timeout 10 sec
            // server.ReceiveTimeout = 10000;

            try
            {
                //return " Sever Set Ok !!\n";
                Console.WriteLine("Sever Set Ok !!");

                // print start
                Console.WriteLine("伺服端...運行中...");
                return 1;
            }
            catch
            {
                //return " Client Set wrong !!\n";
                Console.WriteLine("Sever Set wrong !!");
                return 0;
            }
        }

        public String server_get()   // add try catch
        {
            // server or client timeout let wrong.
            get_byte_length = client.Receive(get_byte_data);

            //GetString(Byte[], Int32, Int32)  /包含要解碼之位元組序列的位元組陣列。/要解碼的第一個位元組索引。/要解碼的位元組數。/
            get_message = Encoding.UTF8.GetString(get_byte_data, 0, get_byte_length);

            Console.WriteLine("客戶端：" + get_message);

            if (get_message == "xxxxx")
            {
                client.Close();
                Console.WriteLine("客戶端要求停止連線!!");
            }
            else
            {
                server_send("收到");
            }

            return get_message;
        }


        public int server_send(String send_msg)  // add try catch
        {
            //傳送資訊 
            send_message = send_msg;
            send_byte_data = Encoding.UTF8.GetBytes(send_message);
            client.Send(send_byte_data);

            Console.WriteLine("已傳送");

            // 告知客戶端停止
            if (send_message == "xxxxx")
            {
                client.Close();
                server.Close();
                Console.WriteLine("伺服端要求停止連線");
                return 0;
            }

            return 1;
        }

        public void server_work_analysis(String get_msg)  // add try catch
        {
            switch (get_msg) {
                case "請求進入":
                                Console.WriteLine("工作代號 0: 請求進入");
                                break;
                case "請求退出":
                                Console.WriteLine("工作代號 1: 請求退出");
                                break;
            }
        }
    }
}
