using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{
    //A link to our cavas thing that we're basically considering a mini manager.
    private RoomsCanvases _roomsCanvases;

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
        //We'll leave without the player timeout thing to wait.  We're leaving on purpose.
        PhotonNetwork.LeaveRoom(true);

        //Hide the current room canvas so that we are basically back on the create or join room canvas.
        _roomsCanvases.CurrentRoomCanvas.Hide();
    }
}
