using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject component;

    void OnMouseDown()
    {
        GameObject created = Instantiate(component, transform.parent.parent.GetChild(0));
        Destroy(transform.parent.parent.GetChild(0).GetChild(2).gameObject); //delete plus
        Destroy(transform.parent.gameObject); //delete menu

        if (this.gameObject.name == "Text") {
            // Send message to update other clients
            NoteEventManager noteEventManager = transform.parent.parent.GetChild(0).GetChild(3).gameObject.GetComponent<NoteEventManager>();
            noteEventManager.TriggerTextMount();
        }
    }
}
