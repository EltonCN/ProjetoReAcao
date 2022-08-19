using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Net;
using System.Security.Permissions;

//https://gist.github.com/danielbierwirth/0636650b005834204cb19ef5ae6ccedb
public class EmotionReceiver : MonoBehaviour
{
    [SerializeField] EmotionData targetEmotionData;
    [SerializeField] bool startTrackOnConnection = false;

    TcpClient socketConnection;
    Thread clientReceiveThread;
    bool mustStop = false;

    // Use this for initialization 	
    void Start()
    {
        ConnectToTcpServer();
    }


    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }

    private void ListenForData()
    {
        StringBuilder buffer = new StringBuilder();



        while (true)
        {
            try
            {
                socketConnection = new TcpClient();
                socketConnection.Connect("localhost", 65444);
                
                if(startTrackOnConnection)
                {
                    StartTrack();
                }

                Byte[] bytes = new Byte[1024];
                NetworkStream stream = socketConnection.GetStream();

                while (true)
                {

                    byte receivedByte = (byte)stream.ReadByte();
                    char receivedChar = Encoding.UTF8.GetChars(new[] { receivedByte })[0];

                    if (receivedChar < 0)
                    {
                        continue;
                    }
                    else if (receivedChar == '{')
                    {
                        if (buffer.Length == 0)
                        {
                            buffer.Append("{");
                        }
                    }
                    else if (receivedChar == '}')
                    {
                        if (buffer.Length == 0)
                        {
                            continue;
                        }

                        buffer.Append(receivedChar);

                        string jsonString = buffer.ToString();
                        buffer.Clear();
                        JsonUtility.FromJsonOverwrite(jsonString, targetEmotionData);
                    }
                    else
                    {
                        if (buffer.Length == 0)
                        {
                            continue;
                        }

                        buffer.Append(receivedChar);
                    }

                }
            }
            catch (SocketException)// socketException)
            {
                //Debug.Log("Socket exception: " + socketException);
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                Debug.Log("Stopping " + e);
                mustStop = true;
            }


            if (mustStop)
            {
                Debug.Log("Stopping");
                break;
            }
        }
    }


    public void StartTrack()
    {
        SendMessage("true");
    }

    public void EndTrack()
    {
        SendMessage("false");
    }

    void SendMessage(string message)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                byte[] clientMessageAsByteArray = Encoding.UTF8.GetBytes(message);
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    void OnDestroy()
    {
        Debug.Log("Destroy");
        KillTheThread();
    }

    [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
    private void KillTheThread()
    {
        mustStop = true;
        clientReceiveThread.Abort();
    }
}