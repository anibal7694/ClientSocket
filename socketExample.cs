/* This is simple synchronous socket program for the Microsoft HoloLens.
It has no functionality. 
Read the ReadME file to get a hang of what is happening. 
Original code written in Unity C# using VS17.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using System.Threading;
using System.Diagnostics;

#if !UNITY_EDITOR
using System.Threading.Tasks;  
#endif

public class SocketExample
{
#if !UNITY_EDITOR
    private bool _useUWP = true;
    private Windows.Networking.Sockets.StreamSocket socket;
    private Task exchangeTask;
#endif
	//We will be using the Stream class from System.IO to send and receive data
	private Byte[] bytes = new Byte[256];
    private StreamWriter writer;
    private StreamReader reader;
	void Start()
    {
        Connect();
    }
	
	//Connect to the server if the application is a UWP Application
	public void Connect(string host = "127.0.0.1", string port = "5555")
    {
#if !UNITY_EDITOR
        ConnectUWP(host, port);
#endif

    }
#if !UNITY_EDITOR
    private async void ConnectUWP(string host, string port)
    {

        try
        {
            if (exchangeTask != null) StopExchange();

            socket = new Windows.Networking.Sockets.StreamSocket();
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(host);
            await socket.ConnectAsync(serverHost, port);

            Stream streamOut = socket.OutputStream.AsStreamForWrite();
            writer = new StreamWriter(streamOut) { AutoFlush = true };

            Stream streamIn = socket.InputStream.AsStreamForRead();
            reader = new StreamReader(streamIn);

        }
        catch (Exception e)
        {
            e.ToString();
        }

    }
	public void StopExchange()
    {
        writer.Write("!\n");

        //if (exchangeTask != null) {
        //    exchangeTask.Wait();
           
            socket.Dispose();
            writer.Dispose();
            reader.Dispose();

            socket = null;
            exchangeTask = null;
        //}

        writer = null;
        reader = null;
    }
#endif
	void Update()
    {
		writer.Write(Data);
		String receivedData = reader.ReadLine();
	}
	//Use application Quit if your application is quitting for sure. 
	//If the application is just closing using the close gesture in HoloLens, then the application is in Pause state.
	public void OnApplicationPause()
    {
        writer.Write("!\n");
#if !UNITY_EDITOR
        socket.Dispose();
        writer.Dispose();
        reader.Dispose();
        socket = null;
        exchangeTask = null;
        StopExchange();
        writer = null;
        reader = null;
#endif
    }
}