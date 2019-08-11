using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text IP_ADDR;

    public void startGame(){
      SceneManager.LoadScene("AR Scene");
    }

    public void startNetworkedGame(){
      Debug.Log(IP_ADDR.text);
      PlayerPrefs.SetString("IP_ADDR", IP_ADDR.text);
      Debug.Log(string.Format("Menu IP: {0}", PlayerPrefs.GetString("IP_ADDR")));
      SceneManager.LoadScene("ARNetworking");
    }

}
