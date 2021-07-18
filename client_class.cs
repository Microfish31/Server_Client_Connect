using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client_connect
{
    public class client_class
    {
        //建立客戶端
        public Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public static int get_data_size = 1024;
        public static int send_data_size = 1024;

        //用來接伺服器端的資料 type  = byte
        public byte[] get_byte_data = new byte[get_data_size];

        //用來送伺服器端的資料 type  = byte
        public byte[] send_byte_data = new byte[send_data_size];

        public int get_byte_length;

        // msg type = string
        public string get_message;
        public string send_message;

        public int client_set(String ip_address, int port)
        {
            // Input Data
            // Client ID        type int  <try>
            // "192.168.1.107"  type String
            // 3000             type int

            get_byte_length = 0;

            //建立IP地址（IP地址和埠號是伺服器端的，用來和伺服器進行通訊）
            IPAddress ip = IPAddress.Parse(ip_address);
            EndPoint ep = new IPEndPoint(ip, port);

            // set max buffer
            client.ReceiveBufferSize = 65536;

            // set timeout 10 sec
            //client.ReceiveTimeout = 10000;

            try {
                //連線伺服器
                client.Connect(ep);
            
                 //return ID.ToString() + " Client Set Ok !!\n";
                Console.WriteLine("Client Set Ok !!");
                return 1;
            }catch {
                //return ID.ToString() + " Client Set wrong !!\n";
                Console.WriteLine("Client Set wrong !!");
                return 0;
            }
        }

        public String client_get()   // add try catch
        {
            get_byte_length = client.Receive(get_byte_data);

            get_message = Encoding.UTF8.GetString(get_byte_data, 0, get_byte_length);

            Console.WriteLine(get_message);

            return get_message;
        }

        public int client_send(String send_msg)  // add try catch
        {
            //傳送資訊 
            send_message = send_msg;
            send_byte_data = Encoding.UTF8.GetBytes(send_message);
            client.Send(send_byte_data);
            Console.WriteLine("已傳送");

            // 告知伺服端停止
            if(send_message == "xxxxx") {
               client.Close(); 
               Console.WriteLine("客戶端停止連線");
               return 0;
            }

            client_get();

            return 1;
        }
    }
}
