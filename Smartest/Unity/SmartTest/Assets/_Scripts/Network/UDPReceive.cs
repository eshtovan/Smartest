using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Battlehub.Dispatcher;
using Random = UnityEngine.Random;


public class UDPReceive : MonoBehaviour
{

   
    Thread receiveThread;
    public GameObject Try;
    private IPEndPoint anyIP;
    UdpClient client;



  
    public int port; 
    public int RecivePort;
    
    public string lastReceivedUDPPacket = "";
    public string allReceivedUDPPackets = ""; // clean up this from time to time!



    public void Start()
    {
        Time.timeScale = 0;
        init();
    }

    // OnGUI
    void OnGUI()
    {
        //Rect rectObj = new Rect(40, 10, 200, 400);
        //GUIStyle style = new GUIStyle();
        //style.alignment = TextAnchor.UpperLeft;
        //GUI.Box(rectObj, "# UDPReceive\n127.0.0.1 " + port + " #\n"
        //            + "shell> nc -u 127.0.0.1 : " + port + " \n"
        //            + "\nLast Packet: \n" + lastReceivedUDPPacket
        //            + "\n\nAll Messages: \n" + allReceivedUDPPackets
        //        , style);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Time.timeScale = 1;
        }
    }
    // init
    private void init()
    {
        // Endpunkt definieren, von dem die Nachrichten gesendet werden.
        print("UDPSend.init()");

        // define port


        // status
        print("Sending to 192.168.1.53 : " + port);
        print("Test-Sending to this Port: nc -u 192.168.1.53  " + port + "");


        client = new UdpClient(port);
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    // receive thread
    private void ReceiveData()
    {
        
        while (true)
        {
            try
            {

                anyIP = new IPEndPoint(IPAddress.Any, port);

                byte[] data = client.Receive(ref anyIP);

                string text = Encoding.UTF8.GetString(data);

             
                lastReceivedUDPPacket = text;

                allReceivedUDPPackets = allReceivedUDPPackets + " " + text;
                Dispatcher.Current.BeginInvoke(() =>
                {
                    if (text == "StartGame")
                    {
                        StartGame();
                    }
                
                    else
                    {
                        Invoke("DuplicatBox", 0.1f);
                    }
             
                });

            }
            catch (Exception err)
            {
                Debug.LogError(err.ToString());
            }
        }

    }

    // getLatestUDPPacket
    // cleans up the rest
    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets = "";
        return lastReceivedUDPPacket;
    }

    public void OnApplicationQuit()
    {
        if (client != null)
        {
            client.Close();
            client.Dispose();
        }


    }
    int i = 0;


    public void StartGame()
    {
        Time.timeScale = 1;
        sendString("Game Started");
    }
    public void DuplicatBox()
    {
        GameObject s = Instantiate(Try);
        s.transform.position=new Vector3(Random.Range(0,-250),-11, Random.Range(0, -250));
        Debug.Log("Messege Recived and " + s.name + "was Born in Position" + s.transform.position);

        sendString("Messege Recived and " + s.name + "was Born in Position" + s.transform.position);
        // Den message zum Remote-Client senden.

    }

    public void sendString(string message)
    {
        try
        {
            anyIP = new IPEndPoint(IPAddress.Loopback, RecivePort);



            // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
            byte[] data = Encoding.UTF8.GetBytes(message);

            Debug.Log("Sending "+ message);
            // Den message zum Remote-Client senden.
            client.Send(data, data.Length, anyIP);
            //}
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }


    // endless test
    private void sendEndless(string testStr)
    {
        do
        {
            sendString(testStr);


        }
        while (true);

    }
}