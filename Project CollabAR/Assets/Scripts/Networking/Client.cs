using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour {

    private const int MAX_CONNECTION = 10;
    private const int SERVER_PORT = 26000;
    private string SERVER_IP = "";

    private int connectionId;
    private int hostId;

    private int reliableChannelId;
    private int unreliableChannelId;

    public bool loadIP = true; // Set to True for deployment

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        if (loadIP) {
           SERVER_IP = PlayerPrefs.GetString("IP_ADDR");
        }
        Debug.Log(SERVER_IP);
        Initialise();
    }

    void Initialise() {
        NetworkTransport.Init();

        ConnectionConfig config = new ConnectionConfig();
        reliableChannelId  = config.AddChannel(QosType.Reliable);
        unreliableChannelId = config.AddChannel(QosType.Unreliable);
        HostTopology topology = new HostTopology(config, 10);

        // Add host, no one else should be able to connect.
        hostId = NetworkTransport.AddHost(topology, 0);

        byte error;
        connectionId = NetworkTransport.Connect(hostId, SERVER_IP, SERVER_PORT, 0, out error);
    }

    public void Send(byte[] buf) {
        if (buf.Length > 1024) {
            Debug.Log("Buffer size exceeded"); // LOL
            return;
        }
        Debug.Log(buf.Length);
        byte error;
        NetworkTransport.Send(hostId, connectionId, reliableChannelId, buf, buf.Length, out error);
    }

}
