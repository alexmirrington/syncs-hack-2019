using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputFieldListener : MonoBehaviour
{

    private UnityAction<string> m_MyFirstAction;

    // Start is called before the first frame update
    void Start()
    {
      var input = gameObject.GetComponent<InputField>();
      m_MyFirstAction += NetworkManager;
      input.onEndEdit.AddListener(m_MyFirstAction);
    }

    void NetworkManager(string input){
      transform.parent.GetChild(2).GetComponent<NoteEventManager>().TriggerNoteEdit(input);
    }
}
