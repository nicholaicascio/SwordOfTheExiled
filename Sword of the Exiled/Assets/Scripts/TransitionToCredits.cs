using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class TransitionToCredits : MonoBehaviour
{
    private const byte TRANS_TO_CREDITS = 0;

    private void OnEnable()
    {
        Debug.Log("Adding listener for transition to credits");
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        //First, check for our event.
        if (obj.Code == TRANS_TO_CREDITS)
        {
            //This is it.  We'll go ahead and transition to credits
            Debug.Log("collide");
            SceneManager.LoadScene("CreditsScene");

            //Now that we have opened the door, remove the listener so this doesn't happen again.
            RemoveEvent_TransToCredits();
        }
    }

    private void RemoveEvent_TransToCredits()
    {
        Debug.Log("Removing listener for door opening.");
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PhotonNetwork.RaiseEvent(TRANS_TO_CREDITS, null, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }
        //Now, we're sending an event.
        
    }
}
