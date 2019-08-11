using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButtonScript : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool dragEnable;
    public GameObject networkEventManager;
    private NoteEventManager noteEventManager;

    void Awake() {
      noteEventManager = networkEventManager.GetComponent<NoteEventManager>();
    }

    void OnMouseDown(){
      //Destroy(transform.parent.parent.gameObject);
      noteEventManager.TriggerNoteDelete();
    }

}
