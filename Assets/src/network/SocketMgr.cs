using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// Summary description for Class1
/// </summary>
public class SocketMgr
{
    private static int MAXBUFFER = 1024 * 64;
    private static int HALFMAXBUFFER = MAXBUFFER / 2;
    private Socket socket;
    public bool isconnect = false;

    private int sendlen = 0;
    private byte[] sendbuf = new byte[MAXBUFFER];
    private int recvlen = 0;
    private byte[] recvbuf = new byte[MAXBUFFER];
    public Queue<byte[]> recvlist = new Queue<byte[]>();
    public SocketMgr(){
	}

    public bool blockConnect(string ip, UInt16 port)
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            socket.Connect(ip, port);
        }
        catch (SocketException ex){
            Logger.LogException(ex);
            return false;
        }
        isconnect = true;
        return isconnect;
    }

    public bool connect(string ip, UInt16 port){
        if (ip == "") return false;
        try { 
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Blocking = false;
            //IPAddress ipaddr = IPAddress.Parse(ip);
            //IPEndPoint ep = new IPEndPoint(ipaddr, port); 
            socket.BeginConnect(ip, port, new AsyncCallback(endConnect), null);
            Logger.LogMsg("BeginConnect!");
        }
        catch (SocketException ex){
            Logger.LogException(ex);
            return false;
        }
        return true;
    }

    private void endConnect(IAsyncResult async){
		Logger.LogMsg("EndConnect!");
		try
		{
			socket.EndConnect(async);
            isconnect = true;
		}
		catch (SocketException ex)
		{
			if (!ex.NativeErrorCode.Equals(10035))
			{
				Logger.LogException(ex);
			}
		}
	}

    public void disConnect(){
        socket.Close();
        socket = null;
    }

    public bool send(byte[] buf){
        if (!isconnect) return false;
		ushort blen = (ushort)buf.Length;
        if (blen > HALFMAXBUFFER) {
            return false;
        }
		sendbuf [0] = (byte)(blen >> 8);
		sendbuf [1] = (byte)(blen & 0x00ff);
		sendlen = 2;
        Array.Copy(buf, 0, sendbuf, sendlen, buf.Length);
        sendlen += buf.Length;
        int len = socket.Send(sendbuf, sendlen, SocketFlags.None);
        if (len != sendlen){             
            Array.Copy(sendbuf, sendlen, sendbuf, 0, sendlen - len);
            sendlen -= len;
        }
        return true;
    }
    public void recv(){
        socket.BeginReceive(recvbuf,recvlen, HALFMAXBUFFER, SocketFlags.None, new AsyncCallback(endRecv), null);
    }

    private void endRecv(IAsyncResult async)
    {
        int len = socket.EndReceive(async);
        recvlen += len;
        if (recvlen >= 2) {
			int l = (recvbuf[0] << 4) & (recvbuf[1]);
			if (recvlen >= l + 2){
                byte[] temp = new byte[l];
                Array.Copy(recvbuf, 2, temp, 0, l);
                recvlist.Enqueue(temp);           
                recvlen -= l + 2;
            }
        } 
        recv();
    }
}
