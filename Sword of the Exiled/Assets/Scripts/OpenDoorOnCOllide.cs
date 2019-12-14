using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// This class is going to handle a network event and listener.  It is specifically for handling
/// the initial door in the first room.  Would it be easier to just open the door in the first
/// place?  For sure!  But I am doing it this way because I don't have anything using this.
/// </summary>
public class OpenDoorOnCOllide : MonoBehaviourPun
{
    public GameObject targetObject;
    public GameObject[] objectsToEnable;

    //This private constant will be used to check which event is taking place.
    //Events can be values between 1 and 199 for Photon according to the documentation.
    private const byte OPEN_FOURTH_ROOM_DOOR = 1;

    /// <summary>
    /// Add a listener when enabled.
    /// </summary>
    private void OnEnable()
    {
        //Debug.Log("Adding listener for door opening.");
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_OpenFourthRoomDoor;
    }

    /// <summary>
    /// This will remove the listener when disabled.
    /// </summary>
    private void OnDisable()
    {
        RemoveEvent_OpenFourthRoomDoorListener();
    }

    /// <summary>
    /// This will remove the listener.
    /// </summary>
    private void RemoveEvent_OpenFourthRoomDoorListener()
    {
        //Debug.Log("Removing listener for door opening.");
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_OpenFourthRoomDoor;
    }

    /// <summary>
    /// This is the actual event handler.  It will actually accept the event.
    /// Now, this uses the ExitGames.Client.Photon class.  I don't really know
    /// what that is doing or why though.  I just use it as part of what they were
    /// doing. on their YouTube videos.
    /// </summary>
    /// <param name="obj"></param>
    private void NetworkingClient_OpenFourthRoomDoor(EventData obj)
    {
        //Debug.Log("NetworkingClient_OpenFourthRoomDoor just heard an event.  Listening for " + OPEN_FOURTH_ROOM_DOOR + "; heard " + obj.Code);
        //First, check for our event.
        if (obj.Code == OPEN_FOURTH_ROOM_DOOR)
        {
            //Debug.Log("NetworkingClient_OpenFourthRoomDoor is responding to the event.");
            //This is it.  We'll go ahead and open the door.
            Animator anim = targetObject.GetComponent<Animator>();
            anim.SetBool("isOpen", true);
            foreach (GameObject en in objectsToEnable)
            {
                en.SetActive(true);
            }

            //Now that we have opened the door, remove the listener so this doesn't happen again.
            RemoveEvent_OpenFourthRoomDoorListener();
        }
    }

    /// <summary>
    /// This is where we hit the trigger.  We should probably do something like
    /// make sure the player is entering the trigger.
    /// Weird note: this only works in actual multiplayer.  If only one person is playing
    /// it doesn't work for some reason. Not sure why that is.
    /// I did a bit of explaining here for the RaiseEvent.  See https://doc.photonengine.com/en-us/pun/v2/gameplay/rpcsandraiseevent
    /// for more information.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("The open door trigger has been triggered!");

        //Check to see if we collided with a player.
        if (other.tag == "Player")
        {
            //Debug.Log("Player entered the trigger zone!");
            //For the sake of learing some stuff and making sure we do this correctly elsewhere,
            //we will build all the different parts of the event, then actually raise the event.
            //It's really not necessary for most of what we do in this game, but it's really good
            //to know all this.

            //First, get the event code.  We will set it to our constant.
            byte eventCode = OPEN_FOURTH_ROOM_DOOR;

            //Next, we will build any data that needs to be passed into our event.
            //Since we don't need to send anything like vectors, ints, a list of object ids,
            //or any other serializable data, we'll just send a null.
            object[] content = null;

            //Here, we'll set the RaiseEventOptions.  I don't know what all options are available here,
            //but we specifically want all people in our room to get this so we will set the reciever group
            //to all ensuring that even us as the player causing this get the event.
            //After a bit more reading, the RaiseEventOptions are:
            //Receiver Groups
            //Interest Groups
            //Target Actors
            //Caching Options - This one is interesting and can affect the order in which players see things.
            RaiseEventOptions raiseOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

            //Lastly, we'll set the SendOptions.  This only has reliability and encryption.  We don't need encryption,
            //but it's pretty important that this is sent.
            SendOptions sendOptions = new SendOptions { Reliability = true, Encrypt = false };

            //Now, we're actually raising the event using the parameters we just set.
            PhotonNetwork.RaiseEvent(eventCode, content, raiseOptions, sendOptions);
        }
        else
        {
            //Debug.Log("A non player entered the trigger zone.");
        }
    }
}
