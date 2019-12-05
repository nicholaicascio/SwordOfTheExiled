using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateOrJoinRoomCanvas : MonoBehaviourPunCallbacks
{

    //Make sub menus aware of the rooms canvases
    [SerializeField]
    private CreateRoomMenu _createRoomMenu;

    //Status textbox.
    [SerializeField]
    private Text _statusText;

    //This will track if we are connected to the server or not.
    private bool connected = false;

    //Make sub menus aware of the room listings menu I guess.  This is the scroll window that shows the rooms.
    [SerializeField]
    private RoomListingsMenu _roomListingsMenu;

    //This is the main canvas that will act as a semi manager.
    private RoomsCanvases _roomsCanvases;

    /// <summary>
    /// This is the initializer.  To be called on the first initialization.
    /// </summary>
    /// <param name="canvases"></param>
    public void FirstInitialize (RoomsCanvases canvases)
    {
        //Debug.Log("FirstInitialize for the CreateOrJoinRoomCanvas");
        _roomsCanvases = canvases;
        _createRoomMenu.FirstInitialize(canvases);
        _roomListingsMenu.FirstInitialize(canvases);
    }

    private void Start()
    {
        _statusText.text = "Status: Connecting to server.";
    }

    public override void OnConnectedToMaster()
    {
        _statusText.text = "Status: Connected to server.";
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        _statusText.text = "Status: Disconnected from server for reason: " + cause.ToString();
        //DisconnectCause.ClientTimeout
    }
}
