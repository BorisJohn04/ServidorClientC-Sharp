using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
namespace ClienteSocket
{
    class Program
    {
        static void Main()
        {
                //configuração de conexão com o servidor
                IPHostEntry host = Dns.GetHostEntry("localhost");//se conecta ao mesmo endereço do servidor, que é local host
                IPAddress ip = host.AddressList[0];//adiciona o ip em uma adresslist
                IPEndPoint remoteEP = new IPEndPoint(ip, 3306);//cria um novo ponto de acesso com o ip e porta onde o servidor está aberto
               
                try
                {
                    //cria um canal socket para enviar dados
                    Socket sender = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    //conecta o socket ao servidor
                    sender.Connect(remoteEP);
                    Console.WriteLine("Conectado ao servidor");
                    //envio de dados
                    Console.WriteLine("Envie algo ao servidor");
                    while (true)
                    {
                        string texto = Console.ReadLine();
                        //os dados são enviados convertidos em bytes, e no servidor são convertidos em strings
                        byte[] msg = Encoding.ASCII.GetBytes(texto + "," + "<EOF>");
                        //método que envia os dados
                        int byteSent = sender.Send(msg);
                    }

                    //encerra o envio
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            
        }
    }
}