using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableScript : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 start;
    private Vector3 end;
    public bool dragEnable;

    public GameObject networkEventManager;
    private NoteEventManager noteEventManager;

    void Awake() {
        noteEventManager = networkEventManager.GetComponent<NoteEventManager>();
    }

    void OnMouseDown()
    {
        dragEnable = true;
        screenPoint = Camera.main.WorldToScreenPoint(transform.parent.parent.position);
        offset = transform.parent.parent.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        start = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.parent.position.z);
    }

    void OnMouseUp()
    {
       end = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.parent.position.z);
       Snappable parent = GetComponentInParent<Snappable>();
       parent.DoSnap();
       // Update position after snap
       noteEventManager.TriggerNotePosition(end-start);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.parent.parent.position = curPosition;
    }


}
