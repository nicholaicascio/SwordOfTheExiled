using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The purpose of this class is to set the username for a text box.
/// </summary>
public class SetUsername : MonoBehaviour
{
    InputField input;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputField>();
        Debug.Log("Start call - Current username to set for screen: " + MasterManager.GameSettings.NickName);
        //Get the username from the MasterManager.GameSettings singleton.
        input.text = MasterManager.GameSettings.NickName;
        Debug.Log("Input field text value: " + input.text);
    }
    void OnEnable()
    {
        input = GetComponent<InputField>();
        Debug.Log("OnEnable call - Current username to set for screen: " + MasterManager.GameSettings.NickName);
        //Get the username from the MasterManager.GameSettings singleton.
        //this.gameObject.GetComponent<Text>().text = MasterManager.GameSettings.NickName;
        input.text = MasterManager.GameSettings.NickName;
        Debug.Log("Input field text value: " + input.text);
    }
}
