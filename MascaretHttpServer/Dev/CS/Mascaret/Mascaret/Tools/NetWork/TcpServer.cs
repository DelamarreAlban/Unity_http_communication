using System.Collections;


using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Mascaret
{
    public class TcpServer
    {
        private int port = 8080;

        /* WINPHONE */
        private IPAddress ipAd;
        private Socket listenSocket;
        private TcpListener tcp_Listener = null;


        protected TcpConnectionFactory connectionFactory;

        public TcpServer(int port, TcpConnectionFactory connectionFactory)
        {
            this.port = port;
            this.connectionFactory = connectionFactory;
        }

        public void Start()
        {
            tcp_Listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            tcp_Listener.Start();
            MascaretApplication.Instance.VRComponentFactory.Log("Mascaret Http/Tcp server started on port : " + port);

            var serverThread = new Thread(() =>
            {
                while (true)
                {
                    Update();
                    Thread.Sleep(100);
                }
            });
            serverThread.Start();
        }

        public void Update()
        {
            //MascaretApplication.Instance.VRComponentFactory.Log("Mascaret Http/Tcp server update...");
            /* WINPHONE */
            //if (tcp_Listener.Pending())
            //{
            //    MascaretApplication.Instance.VRComponentFactory.Log("listener pending !");
                //System.Console.WriteLine("Nouvelle Connection");
                
            //    Socket ss = tcp_Listener.AcceptSocket();
            //    TcpConnection tcpConnection = createConnection(ss);
            //    handleAccept(tcpConnection);

                //Debug.Log (" Fin Handle Connection ...");
            //}


            MascaretApplication.Instance.VRComponentFactory.Log("listener pending !");
            //System.Console.WriteLine("Nouvelle Connection");

            Socket ss = tcp_Listener.AcceptSocket();
            //TcpConnection tcpConnection = createConnection(ss);
            
            var childSocketThread = new Thread(() =>
            {
                /*
                byte[] data = new byte[100];
                int size = ss.Receive(data);
                Console.WriteLine("Recieved data: ");
                for (int i = 0; i < size; i++)
                    Console.Write(Convert.ToChar(data[i]));

                Console.WriteLine();
                */

                TcpConnection tcpConnection = createConnection(ss);
                handleAccept(tcpConnection);

                ss.Close();
            });
            childSocketThread.Start();

        }

        public void handleAccept(TcpConnection connection)
        {
            connection.start();
        }

        /* WINPHONE */
        public virtual TcpConnection createConnection(Socket ss)
        {
            return connectionFactory.create(ss);
        }


    }
}
