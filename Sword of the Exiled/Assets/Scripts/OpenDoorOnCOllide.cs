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
    private const byte OPEN_FOURTH_ROOM_DOOR = 0;

    // Start is called before the first frame update
    void Awake()
    {

    }

    /// <summary>
    /// Add a listener when enabled.
    /// </summary>
    private void OnEnable()
    {
        Debug.Log("Adding listener for door opening.");
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
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
        Debug.Log("Removing listener for door opening.");
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    /// <summary>
    /// This is the actual event handler.  It will actually accept the event.
    /// Now, this uses the ExitGames.Client.Photon class.  I don't really know
    /// what that is doing or why though.  I just use it as part of what they were
    /// doing. on their YouTube videos.
    /// </summary>
    /// <param name="obj"></param>
    private void NetworkingClient_EventReceived(EventData obj)
    {
        //First, check for our event.
        if (obj.Code == OPEN_FOURTH_ROOM_DOOR)
        {
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
    /// TODO: Make sure player is entering trigger.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Now, we're sending an event.
        PhotonNetwork.RaiseEvent(OPEN_FOURTH_ROOM_DOOR, null, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }
}
