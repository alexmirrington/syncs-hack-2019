using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    void OnMouseDown() {
        //GameObject.FindGameObjectWithTag("Menu").SetActive(true);
        print("testing");
        transform.parent.parent.GetChild(1).gameObject.SetActive(true);

    }
}
