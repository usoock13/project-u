using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class Client : MonoBehaviour
{
    #region UDP Server Connecting
    int networkId;
    public void ConnectServer(string message) {
        UdpClient client = new UdpClient();
        try {
            client.Connect("localhost", 6021);
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            client.Send(bytes, bytes.Length);
            
            IPEndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] receiveByte = client.Receive(ref remoteEndpoint);
            string resMsg = Encoding.UTF8.GetString(receiveByte);

            print("Respone message from server :: " + resMsg);
        } catch (System.Exception e) {
            Debug.LogError("Exception:: " + e.Message);
        }
    }
    #endregion

    #region TCP/IP Socket Connecting
    private TcpClient socketConnection;

    public void ConnectRelay() {
        try {
            socketConnection = new TcpClient("125.190.241.9", 6000);
            //socketConnection = new TcpClient("127.0.0.1", 6001);
        } catch (Exception e) {
            Debug.LogError("Throwed exception on connecting server : " + e);
        }
    }
    public void HostConnecting() {

    }
    public void SendToRelay(string message) {
        byte[] bytes = Encoding.UTF8.GetBytes(message);
        if(socketConnection == null) {
            print("is not any tcp client");
            return;
        }
        try {
            NetworkStream stream = socketConnection.GetStream();
            stream.ReadTimeout = 4000;
            if(stream.CanWrite) {
                Task waitingForPartner = new Task(() => {
                    stream.Write(bytes, 0, bytes.Length);
                    byte[] resBytes = new byte[1024];

                    print("I'll be wating for love, wating for love.");
                    stream.Read(resBytes, 0, resBytes.Length);

                    string jsonStr = Encoding.UTF8.GetString(resBytes);
                    UserInfo partnerInfo = JsonUtility.FromJson<UserInfo>(jsonStr);
                    print(partnerInfo);
                });
                waitingForPartner.Start();
            }
        } catch (SocketException socketException) {
            Debug.LogError("Throwed exception on sending message" + socketException);
        }
    }
    #endregion
}

class UserInfo {
    public string address;     
    public int port;
    public override string ToString() {
        return $"Address : {address}, Port : {port}";
    }
}