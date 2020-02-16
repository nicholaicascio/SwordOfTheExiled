using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveRoomMenu : MonoBehaviour
{
    //A link to our cavas thing that we're basically considering a mini manager.
    private RoomsCanvases _roomsCanvases;
    //public Canvas CreateRoomCanvas;


    [SerializeField]
    [Tooltip("This is the text box that users will enter their user name in.")]
    private Text _userName;


    /// <summary>
    /// This should be called on initialization.  It's expecting to get the mini manager.
    /// </summary>
    /// <param name="canvases"></param>
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    /// <summary>
    /// This will happen when we leave a room.  Should be called on a button click.  Basically, leave immediately on purpose.  Don't wait for a timeout.
    /// </summary>
    public void OnClick_LeaveRoom()
    {
        //Fist, save whatever username has been entered.
        MasterManager.GameSettings.NickName = _userName.text;

        //We'll leave without the player timeout thing to wait.  We're leaving on purpose.
        PhotonNetwork.LeaveRoom(true);

        //Hide the current room canvas so that we are basically back on the create or join room canvas.
        _roomsCanvases.CurrentRoomCanvas.Hide();
        _roomsCanvases.CreateOrJoinRoomCanvas.gameObject.SetActive(true);

        //CreateRoomCanvas.gameObject.SetActive(false);
    }
}
