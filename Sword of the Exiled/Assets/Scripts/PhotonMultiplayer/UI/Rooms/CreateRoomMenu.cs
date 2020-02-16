using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This will work with the canvas that creates rooms.
/// Pulled from First Gear Games on YouTube.
/// </summary>
public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    [Tooltip("This is the text box that users will enter room names in.")]
    private Text _roomName;
    [SerializeField]
    [Tooltip("This is the text box that users will enter their user name in.")]
    private Text _userName;
    [SerializeField]
    [Tooltip("This is the text box that users will enter their user name in.")]
    private Text _statusText;
    public Canvas CreateRoomCanvas;

    //This is the main canvas that will act as a semi manager.
    private RoomsCanvases _roomsCanvases;

    /// <summary>
    /// This is the initializer.  To be called on the first initialization.
    /// </summary>
    /// <param name="canvases"></param>
    public void FirstInitialize(RoomsCanvases canvases)
    {
        //Debug.Log("FirstInitialize for the CreateRoomMenu");
        _roomsCanvases = canvases;
    }

    public void DisableSelf()
    {
        
    }

    public void onClick_CreateRoom()
    {
        //First, make sure both the username and the room name have been entered.
        if (_roomName.text == "" || _userName.text == "")
        {
            Debug.Log("Room name: " + _roomName.text);
            Debug.Log("User name: " + _userName.text);
            _statusText.text = "Status: ";
            if (_userName.text == "")
            {
                _statusText.text = _statusText.text + " Make sure a username has been entered.";
            }
            if (_roomName.text == "" )
            {
                _statusText.text = _statusText.text + " Make sure a room name has been entered.";
            }
        }
        else
        {
            //Fist, save whatever username has been entered.
            MasterManager.GameSettings.NickName = _userName.text;

            //Make sure we are connected before we try to create a room.
            if (!PhotonNetwork.IsConnected)
            {
                Debug.Log("No connection available.");
                return;
            }

            //Reset the network username.
            PhotonNetwork.NickName = MasterManager.GameSettings.NickName;

            //Create room - If room exists, it will fail.
            RoomOptions options = new RoomOptions();

            //Tell the room to broadcast changes.
            options.BroadcastPropsChangeToAll = true;

            //Set the maximum number of players for the room.
            options.MaxPlayers = 2;

            //Join or create room.
            PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);

            //CreateRoomCanvas.gameObject.SetActive(false);

        }
    }

    /// <summary>
    /// What happens when a room is created.
    /// </summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully.");

        //Now, show the current room canvas.
        _roomsCanvases.CurrentRoomCanvas.Show();
        CreateRoomCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// What happens when a room creation fails.
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message, this);
    }
}
