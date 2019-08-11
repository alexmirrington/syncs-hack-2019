using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour {

    private const int MAX_CONNECTION = 10;
    private const int PORT = 26000;

    private int connectionId;
    private int hostId;

    private int reliableChannelId;
    private int unreliableChannelId;

    void Start() {
        Initialise();
    }

    void FixedUpdate() {
        ListenOnce();
    }

    void Initialise() {
        NetworkTransport.Init();

        ConnectionConfig config = new ConnectionConfig();
        reliableChannelId = config.AddChannel(QosType.Reliable);
        unreliableChannelId = config.AddChannel(QosType.Unreliable);

        HostTopology topology = new HostTopology(config, MAX_CONNECTION);

        // Listen on port
        hostId = NetworkTransport.AddHost(topology, PORT);

        Debug.Log(string.Format("Now listening on port {0}.", PORT));
    }

    void ListenOnce() {
        int outHostId;
        int outConnectionId;
        int outChannelId;
        byte[] buffer = new byte[1024];
        int dataSize = 1024;
        int receiveSize;
        byte error;
        
        NetworkEventType evnt = NetworkTransport.Receive(out outHostId, out outConnectionId, out outChannelId, buffer, dataSize, out receiveSize, out error);
        switch (evnt) {
            case NetworkEventType.Nothing:
                break;

            case NetworkEventType.ConnectEvent:
                Debug.Log(string.Format("User {0} has connected!", outConnectionId));
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("User {0} has disconnected!", outConnectionId));
                break;

            case NetworkEventType.DataEvent:
                Debug.Log("Data received!");
                break;

            default:
                //Debug.Log("Unexpected network event");
                break;
        }
    }
}
