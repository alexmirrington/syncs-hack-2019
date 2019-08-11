using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    void OnMouseDown()
    {
        //print("Going home!");
        SceneManager.LoadScene("IntroMenu");
    }
}
