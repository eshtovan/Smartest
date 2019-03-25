using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Smartest.Infrastructure.Interfaces;

namespace Smartest.Models
{
    public class UnityCommunicationModel : IUnityCommunication
    {
        private static int localPort;
        Thread receiveThread;
        private string IP;  // define in init
        public int port;  // define in init
        public int RecivePort;  // define in init
        IPEndPoint remoteEndPoint;
        UdpClient Reciver;
        UdpClient Sender;

        delegate void StringArgReturningVoidDelegate(string text);

        public UnityCommunicationModel()
        {
            init();
          //  Reciveinit();
        }

        public void init()
        {

            Console.WriteLine("UDPSend.init()");
            IP = "127.0.0.1";
            port = 9000;
           
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
            Sender = new UdpClient();
           // Reciveinit();

            // status
            Console.WriteLine("Sending to " + IP + " : " + port);
            Console.WriteLine("Testing: nc -lu " + IP + " : " + port);

        }

        public void Reciveinit()
        {
            RecivePort = 9001;

            //// define port
            //port = 9003;

            //// status
            //Console.WriteLine("Sending to 127.0.0.1 : " + port);
            //Console.WriteLine("Test-Sending to this Port: nc -u 127.0.0.1  " + port + "");
            Reciver = new UdpClient(RecivePort);
            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();

        }


        private void ReceiveData()
        {
            while (true)
            {

                try
                {

                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = Reciver.Receive(ref anyIP);
                    string text = Encoding.UTF8.GetString(data);

                    Console.WriteLine("got : " + text);
                   // SetText(text);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.ToString());
                }
            }
        }


        public void SendCommand(string msg)
        {
            try
            {
                //if (message != "")
                //{

                // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
                byte[] data = Encoding.UTF8.GetBytes(msg);

                if (Sender == null)
                {
                    Sender = new UdpClient();
                }
                // Den message zum Remote-Client senden.
                Sender.Send(data, data.Length, remoteEndPoint);
                //}
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }
    }
}
