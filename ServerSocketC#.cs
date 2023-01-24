using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections;
namespace estrutura
{
    public class server
    {
        public static string data = null;
        public static string email;
        public static string senha;
        public static void Main()
        {

            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ip = host.AddressList[0];
            // Create a new IPEndPoint object to listen on the specified port
            IPEndPoint localEndPoint = new IPEndPoint(ip, 3306);
           
            try
            {
                Socket listerner = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listerner.Bind(localEndPoint);
                listerner.Listen(10);
                Console.WriteLine("waiting connections...");

                Socket handler = listerner.Accept();

                
                byte[] bytes = null;
                while (true)
                {
                    bytes = new byte[1024];
                    int byteRec = handler.Receive(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, byteRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        Console.WriteLine("Texto do cliente" + " " + data);
                        myBank();
                    }
                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public static void myBank()
        {
            string[] nomes = new string[1000];
            nomes = data.Split(",");
            email = nomes[0];
            senha = nomes[1];
            string adress = "Server=127.0.0.1;User ID=root; port= 3308; Password=falloutguns123;Database=vandaData";
            string query = "INSERT INTO DADOS(email, senha) values ('" + email + "', '" + senha + "')";
            MySqlConnection conect;
            conect = new MySqlConnection(adress);
            MySqlCommand comando;
            comando = new MySqlCommand(query, conect);
            conect.Open();
            comando.ExecuteNonQuery();
            conect.Close();
            Console.WriteLine("Dados enviados com sucesso!");

        }

        public static void myServer()
        {
            IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
            IPAddress ip = host.AddressList[0];
            // Create a new IPEndPoint object to listen on the specified port
            IPEndPoint localEndPoint = new IPEndPoint(ip, 11200);
            Socket listerner = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listerner.Bind(localEndPoint);
                listerner.Listen(10);
                Console.WriteLine("waiting connections...");

                Socket handler = listerner.Accept();

                string data = null;
                byte[] bytes = null;
                while (true)
                {
                    bytes = new byte[1024];
                    int byteRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, byteRec);
                    if(data.IndexOf("<EOF>") > -1)
                    {
                        break;

                    }
                    Console.WriteLine("Texto do cliente" + data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }

}