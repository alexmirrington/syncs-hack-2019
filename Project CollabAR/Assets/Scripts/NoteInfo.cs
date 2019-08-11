using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NoteInfo : MonoBehaviour {

    private string id;

    public void SetId(string id) {
        this.id = id;
    }

    public string GetId() {
        return id;
    }
}
