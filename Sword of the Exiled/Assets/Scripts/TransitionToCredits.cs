using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>
/// So, with this I am thinking that there should be a large class just for listening.
/// Probably not, but finding network listeners may be a pain in the future.
/// More to that point though, it seems like in Photon, network events all get listened
/// to by the same "PhotonNetwork.NetworkingClient.EventReceived" code.  Because of this,
/// you ened to have different constants (possibly enums?), that you send out.  Otherwise, the
/// same event could be triggered even if that wasn't your intent.  So you need to have a uniquely
/// named listener, and a unique "EventData.Code" as well.  Seems odd to need two different
/// things for different events.  Maybe it's becasue one listener can handle multiple events?
/// I suppose that would make sense.  Even if that were the case, there would need to be
/// a place to check all the EventData.Code values, right?  I mean, when you use
/// "PhotonNetwork.RaiseEvent" to raise an event, you don't say which listener(s) should
/// hear it.  You just tell it what value it should pass along.  So odd.  But the case of
/// one listener listening for two values makes sense I suppose.
/// </summary>
public class TransitionToCredits : MonoBehaviourPun
{
    //This private constant will be used to check which event is taking place.
    //Events can be values between 1 and 199 for Photon according to the documentation.
    private const byte TRANS_TO_CREDITS = 2;

    /// <summary>
    /// This runs when the object is enabled.  It will add a network event listener.
    /// </summary>
    private void OnEnable()
    {
        //Debug.Log("Adding listener for transition to credits");
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_CreditsEvent;
    }

    /// <summary>
    /// This is the actual event listener.  When network events take place, this will run.
    /// </summary>
    /// <param name="obj"></param>
    private void NetworkingClient_CreditsEvent(EventData obj)
    {
        //Debug.Log("NetworkingClient_CreditsEvent just heard an event.  Listening for " + TRANS_TO_CREDITS + "; heard " + obj.Code);
        //First, check for our event.
        if (obj.Code == TRANS_TO_CREDITS)
        {
            //Debug.Log("NetworkingClient_CreditsEvent is responding to the event.");
            //This is it.  We'll go ahead and transition to credits
            //Debug.Log("collide");
            SceneManager.LoadScene("CreditsScene");

            //Now that we have opened the door, remove the listener so this doesn't happen again.
            RemoveEvent_TransToCredits();
        }
    }

    /// <summary>
    /// Remove the listener.  I don't know how important this actually is, but it seems to be
    /// common practice to remove listeners once they have been used and no longer have a use.
    /// </summary>
    private void RemoveEvent_TransToCredits()
    {
        //Debug.Log("Removing listener for door opening.");
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_CreditsEvent;
    }

    /// <summary>
    /// This is our trigger.  When the player enters our collider, we will raise the event
    /// that our previously defined listeners are listenting for.  Yay!
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Check to make sure we are colliding with the player.
        if (other.gameObject.tag == "Player")
        {
            byte eventCode = TRANS_TO_CREDITS;

            //Next, we will build any data that needs to be passed into our event.
            object[] content = null;

            //Here, we'll set the RaiseEventOptions.
            RaiseEventOptions raiseOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

            //Lastly, we'll set the SendOptions.  This only has reliability and encryption.  We don't need encryption,
            //but it's pretty important that this is sent.
            SendOptions sendOptions = new SendOptions { Reliability = true, Encrypt = false };

            //Now, we're sending an event.
            PhotonNetwork.RaiseEvent(eventCode, content, raiseOptions, sendOptions);
        }
        
    }
}
