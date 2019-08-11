using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEventManager : MonoBehaviour {

    private NoteManager noteManager;

    void Awake() {
        noteManager = GameObject.Find("NoteManager").GetComponent<NoteManager>();
    }

    public void TriggerTextMount() {
        string id = transform.parent.parent.GetComponent<NoteInfo>().GetId();
        noteManager.MountTextContent(id);
    }

    public void TriggerNotePosition(Vector3 position) {
        string id = transform.parent.parent.GetComponent<NoteInfo>().GetId();
        noteManager.UpdateNotePosition(id, position);
    }

    public void TriggerNoteEdit(string noteText) {
        string id = transform.parent.parent.GetComponent<NoteInfo>().GetId();
        noteManager.EditNote(id, noteText);        
    }

    public void TriggerNoteDelete() {
        string id = transform.parent.parent.GetComponent<NoteInfo>().GetId();
        noteManager.DestroyNote(id);
    }

}
