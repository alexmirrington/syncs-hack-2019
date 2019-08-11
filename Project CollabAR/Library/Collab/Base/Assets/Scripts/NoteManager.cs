using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour {

    public GameObject canvas;
    public GameObject textNoteHolderPrefab;
    public GameObject noteHolderPrefab;
    public GameObject menuPrefab;
    public GameObject textComponentPrefab;

    private Client client;

    private Dictionary<string, GameObject> noteHolders = new Dictionary<string, GameObject>();

    void Awake() {
        GameObject networkManager = GameObject.Find("NetworkManager");
        client = networkManager.GetComponent<Client>();
    }

    void Start() {
        // Find all noteHolders already present
        // var holders = GameObject.FindGameObjectsWithTag("NoteHolder");
        // foreach (var holder in holders) {
        //     noteHolders.Add(Guid.NewGuid().ToString(), holder);
        // }
    }

    public static byte[] XmlSerializeToByte<T>(T value) where T : class {
      if (value == null)
      {
          throw new ArgumentNullException();
      }

      XmlSerializer serializer = new XmlSerializer(typeof(T));

      using (MemoryStream memoryStream = new MemoryStream())
      {
          using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream))
          {
              serializer.Serialize(xmlWriter, value);

              return memoryStream.ToArray();
          }
      }
    }

    public static T XmlDeserializeFromBytes<T> (byte[] bytes) where T : class {
       if (bytes == null || bytes.Length == 0) {
           throw new InvalidOperationException();
       }

       XmlSerializer serializer = new XmlSerializer(typeof(T));

       using (MemoryStream memoryStream = new MemoryStream(bytes))
       {
           using (XmlReader xmlReader = XmlReader.Create(memoryStream))
           {
               return (T)serializer.Deserialize(xmlReader);
           }
       }
    }

    public void SendNoteData(NoteData data) {
        byte[] bytes = XmlSerializeToByte(data);
        client.Send(bytes);
    }

    // TODO manage ID conflicts
    public void ResolveNoteUpdate(NoteData note) {
        Debug.Log("Resolving note update...");
        Debug.Log(note.content);
        Debug.Log(note.action);

        // GameObject[] holders = GameObject.FindGameObjectsWithTag("NoteHolder");

        // bool exists = false;

        // foreach (var holder in holders) {
        //     string id = holder.GetComponent<NoteInfo>().GetId();
        //     Debug.Log(id);
        //     if (id == note.id) {
        //         exists = true;
        //     }
        // }

        // Loop through existing notes and their IDs
        if (noteHolders.ContainsKey(note.id)) {
            GameObject toUpdate;
            switch(note.action) {
              case "DELETE":
                DeleteNote(note.id);
                break;
              case "UPDATE_TEXT":
                noteHolders.TryGetValue(note.id, out toUpdate);
                toUpdate.GetComponentInChildren<InputField>().text = note.content;
                break;
              case "UPDATE_POS":
                noteHolders.TryGetValue(note.id, out toUpdate);
                toUpdate.transform.position += new Vector3(note.x, note.y, note.z);
                break;
              case "MOUNT_TEXT":
                //Debug.Log("Mount");
                noteHolders.TryGetValue(note.id, out toUpdate);
                GameObject created = Instantiate(textComponentPrefab, toUpdate.transform.GetChild(0)); // Instantiate at note pos
                Destroy(toUpdate.transform.GetChild(0).GetChild(2).gameObject); //delete plus
                Debug.Log("Mount");
                // toUpdate.transform.position = new Vector3(note.x, note.y, note.z);
                break;
            }
        } else {
            // No existing note found, create a new one
            AddNote(new Vector3(note.x, note.y, note.z), note.id);
        }
    }

    public void MountTextContent(string id) {
        NoteData data = new NoteData(id, 0, 0, 0, "", "MOUNT_TEXT");
        SendNoteData(data);
    }

    public void EditNote(string id, string text) {
        NoteData data = new NoteData(id, 0, 0, 0, text, "UPDATE_TEXT");
        SendNoteData(data);
    }

    public void UpdateNotePosition(string id, Vector3 position) {
        NoteData data = new NoteData(id, position.x, position.y, position.z, "", "UPDATE_POS");
        SendNoteData(data);
    }

    public void DestroyNote(string id) {
        DeleteNote(id);
        NoteData data = new NoteData(id, 0, 0, 0, "", "DELETE");
        SendNoteData(data);
    }

    private void DeleteNote(string id) {
        GameObject toRemove;
        noteHolders.TryGetValue(id, out toRemove);
        noteHolders.Remove(id);
        Destroy(toRemove);
    }

    public void CreateNote(Vector3 position) {
        string id = AddNote(position);

        // Send data to clients
        NoteData noteData = new NoteData(id, position.x, position.y, position.z, "", "CREATE");
        SendNoteData(noteData);
    }

    public void CreateNote(Vector3 position, String type) {
        if (type == "choosing-the-type") {
            string id = AddNote(position, type, null);

            // Send data to clients
            NoteData noteData = new NoteData(id, position.x, position.y, position.z, "", "CREATE");
            SendNoteData(noteData);
        }
    }

    private string AddNote(Vector3 position, string id=null) {
        Debug.Log(string.Format("Added note at ({0}, {1}, {2}).", position.x, position.y, position.z));

        // Create new note in front of user
        GameObject note = Instantiate(noteHolderPrefab, position, Quaternion.identity, canvas.transform);
        note.transform.position = canvas.transform.position;
        note.transform.rotation = canvas.transform.rotation;
        note.transform.localScale = new Vector3(1, 1, 1);
        if (id == null) {
            id = Guid.NewGuid().ToString();
        }
        note.GetComponent<NoteInfo>().SetId(id);
        noteHolders.Add(id, note);
        return id;

    }

    private string AddNote(Vector3 position, string type, string id=null) {
        Debug.Log(position);
        Debug.Log("YEET");
        Debug.Log(string.Format("Added note at ({0}, {1}, {2}).", position.x, position.y, position.z));

        // Create new note in front of user
        GameObject note = Instantiate(noteHolderPrefab, position, Quaternion.identity, canvas.transform);
        note.transform.position = canvas.transform.position;
        note.transform.rotation = canvas.transform.rotation;
        note.transform.localScale = new Vector3(1, 1, 1);

        GameObject newMenu = Instantiate(menuPrefab, new Vector3(0, 0, 0), Quaternion.identity, note.transform);
        newMenu.SetActive(false);
        newMenu.transform.localPosition = new Vector3(120, 0, 0);
        newMenu.transform.rotation = note.transform.rotation;
        newMenu.transform.localScale = new Vector3(30, 30, 30);
        //newMenu.transform.position = new Vector3(newMenu.transform.position.x - 2, newMenu.transform.position.y, 10);

        if (id == null) {
            id = Guid.NewGuid().ToString();
        }
        note.GetComponent<NoteInfo>().SetId(id);
        noteHolders.Add(id, note);
        return id;

    }

    // For notes that already exist

    public void CreateNote(GameObject note) {
        string id = AddNote(note);

        // Send data to clients
        NoteData noteData = new NoteData(id, note.transform.position.x, note.transform.position.y, note.transform.position.z, "", "CREATE");
        SendNoteData(noteData);
    }

    private string AddNote(GameObject note) {
        Debug.Log(string.Format("Added note at ({0}, {1}, {2}).", note.transform.position.x, note.transform.position.y, note.transform.position.z));

        string id = Guid.NewGuid().ToString();
        note.GetComponent<NoteInfo>().SetId(id);
        noteHolders.Add(id, note);
        return id;

    }
}
