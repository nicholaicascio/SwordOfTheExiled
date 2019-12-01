using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    //This is a special photon hash table.
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    /// <summary>
    /// This will create a custom number randomly and update the text field.
    /// </summary>
    private void SetCustomNumber()
    {
        //Create a random object.
        System.Random rnd = new System.Random();

        //Get a random two digit number.
        int result = rnd.Next(0, 99);

        //Set the value of the text to our random number.
        _text.text = result.ToString();

        //Set the key name in this hash table to "Random Number"  
        _myCustomProperties["RandomNumber"] = result;
        //How to remove keys.
        //_myCustomProperties.Remove("RandomNumber");
        //How to go through everything.
        //_myCustomProperties.Keys;

        //We will set the custom properties of the local player.
        //PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties; //Old way.  Don't do this now.
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
    }

    /// <summary>
    /// This will be called by the button.  Yay!
    /// </summary>
    public void OnClick_Button()
    {
        SetCustomNumber();
    }
}
