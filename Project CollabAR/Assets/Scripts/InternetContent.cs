using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetContent : MonoBehaviour
{
    public string url;
    public GameObject inputField;
    public GameObject text;
    public GameObject internetButton;

    public void loadURL(){
      Application.OpenURL(url);
    }

    public void showURLButton(){
      url = inputField.GetComponent<InputField>().text;
      inputField.SetActive(false);
      text.SetActive(false);
      internetButton.SetActive(true);
      internetButton.transform.GetChild(0).GetComponent<Text>().text = url;
      if(!url.Contains("https://") && !url.Contains("http://")){
        url = "http://" + url;
      }
    }
}
