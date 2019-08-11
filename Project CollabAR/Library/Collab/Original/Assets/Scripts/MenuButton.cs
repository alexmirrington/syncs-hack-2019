using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject component;

    void OnMouseDown()
    {
        Instantiate(component, transform.parent.parent.GetChild(0));
        transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        Destroy(transform.parent.parent.GetChild(0).GetChild(2).gameObject);
        Destroy(transform.parent.gameObject);
    }
}
