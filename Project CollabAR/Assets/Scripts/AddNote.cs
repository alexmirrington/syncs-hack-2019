using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNote : MonoBehaviour
{
    private NoteManager noteManager;
    //public GameObject note;
    //public GameObject canvas;
    //public GameObject menu;

    // Start is called before the first frame update
    void Start() {
      noteManager = GameObject.Find("NoteManager")?.GetComponent<NoteManager>();
      if (noteManager == null) {
        Debug.Log("OOF");
      }
    }

    void OnMouseDown() {
        //print("Test");

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
        //Vector3 offset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));



        // Create new box in front of user
        //GameObject newBox = Instantiate(note, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);
        //GameObject newMenu = Instantiate(menu, new Vector3(0, 0, 0), Quaternion.identity, newBox.transform);
        ////newMenu.SetActive(false);

        //newBox.transform.position = canvas.transform.position;
        //newBox.transform.rotation = canvas.transform.rotation;
        //newBox.transform.localScale = new Vector3(1, 1, 1);

        //newMenu.transform.position = newBox.transform.position;
        //newMenu.transform.rotation = newBox.transform.rotation;
        //newMenu.transform.localScale = new Vector3(30, 30, 30);

        //newMenu.transform.position = new Vector3(newMenu.transform.position.x - 2, newMenu.transform.position.y, 10);

        //noteManager.CreateNote(newBox);
        noteManager.CreateNote(new Vector3(0, 0, 0), "choosing-the-type");
    }

    // // Update is called once per frame
    // void Update()
    // {
    //
    // }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AddNote : MonoBehaviour
//{
//    public GameObject note;
//    public GameObject canvas;
//    public GameObject menu;
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    void OnMouseDown()
//    {
//        Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
//        //Vector3 offset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

//        // Create new box in front of user
//        GameObject newBox = Instantiate(note, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);
//        GameObject newMenu = Instantiate(menu, new Vector3(0, 0, 0), Quaternion.identity, newBox.transform);
//        //newMenu.SetActive(false);

//        newBox.transform.position = canvas.transform.position;
//        newBox.transform.rotation = canvas.transform.rotation;
//        newBox.transform.localScale = new Vector3(1, 1, 1);

//        newMenu.transform.position = newBox.transform.position;
//        newMenu.transform.rotation = newBox.transform.rotation;
//        newMenu.transform.localScale = new Vector3(30, 30, 30);

//        newMenu.transform.position = new Vector3(newMenu.transform.position.x - 2, newMenu.transform.position.y, 10);



//    }


//}
