using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

using UnityEngine;

public class Client : MonoBehaviour
{
    int networkId;

    public void ConnectServer(string message) {
        UdpClient client = new UdpClient();
        try {
            client.Connect("localhost", 3001);
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
}
