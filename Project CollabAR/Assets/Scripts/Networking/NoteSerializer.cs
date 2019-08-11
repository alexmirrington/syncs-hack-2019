using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

using UnityEngine;

[RequireComponent(typeof(NoteEventManager))]
public class NoteSerializer: MonoBehaviour{

    // private NoteEventManager eventManager;

    // void Awake() {
    //     eventManager = gameObject.GetComponent<NoteEventManager>();

    // }

    // void OnEnable() {
    //     eventManager.NoteEdited += SendNoteData;
    //     eventManager.NoteCreated += SendNoteData;
    // }

    // void OnDisable() {
    //     eventManager.NoteEdited -= SendNoteData;
    //     eventManager.NoteCreated -= SendNoteData;
    // }

    /* 
    void Update() {
      if(Input.GetKeyDown("up")) {
        transform.position += Vector3.up * Time.deltaTime;
        Debug.Log("up");
      }
      if(Input.GetKeyDown("down")) {
        transform.position += Vector3.down * Time.deltaTime;
        Debug.Log("down");
      }
      if(Input.GetKeyDown("left")) {
        transform.position += Vector3.left * Time.deltaTime;
        Debug.Log("left");
      }
      if(Input.GetKeyDown("right")) {
        transform.position += Vector3.right * Time.deltaTime;
        Debug.Log("right");
      }

      if(Input.GetKeyDown("space")) {
        byte[] bytes = XmlSerializeToByte(new NoteData(-1, transform.position.x, transform.position.y, transform.position.z, "Test"));
        Debug.Log(bytes);
        var noteData = XmlDeserializeFromBytes<NoteData>(bytes);
        Debug.Log(noteData.x);
      }
    }
    */

    



}

