using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Server : MonoBehaviour {

    private const int MAX_CONNECTION = 10;
    private const int PORT = 26000;

    private int connectionId;
    private int hostId;

    private int reliableChannelId;
    private int unreliableChannelId;

    private NoteManager noteManager;

    void Start() {
        Initialise();
        noteManager = GameObject.Find("NoteManager")?.GetComponent<NoteManager>();
        if (noteManager == null) {
            Debug.Log("OOF");
        }
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
                Text connectionText = GameObject.FindWithTag("ConnectionText").GetComponent<Text>();
                connectionText.text = string.Format("User {0} connected.", outConnectionId);
                //coroutine = FadeText(text, 3.0f, 1.0f);
                //StartCoroutine(coroutine);
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("User {0} has disconnected!", outConnectionId));
                Text connectionText2 = GameObject.FindWithTag("ConnectionText").GetComponent<Text>();
                connectionText2.text = string.Format("User {0} disconnected.", outConnectionId);
                break;

            case NetworkEventType.DataEvent:
                Debug.Log("Data received!");
                Debug.Log(string.Format("ReceivedSize: {0}", receiveSize));
                ReceivedData(buffer, receiveSize);
                break;

            default:
                //Debug.Log("Unexpected network event");
                break;
        }
    }

    // private IEnumerator FadeText(Text text, float wait, float fadeTime) {
        
    // }

    void ReceivedData(byte[] bytes, int recvLen) {
        // Truncate null bytes
        var newBytes = new byte[recvLen];
        for (int i = 0; i < newBytes.Length; ++i) {
            newBytes[i] = bytes[i];
        }
        var data = NoteManager.XmlDeserializeFromBytes<NoteData>(newBytes);
        noteManager.ResolveNoteUpdate(data);
    }
}
