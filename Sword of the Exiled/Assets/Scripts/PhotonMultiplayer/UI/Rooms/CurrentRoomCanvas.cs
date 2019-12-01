using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    //Player listings menu.  Basically the scroll area that holds player listings.
    [SerializeField]
    private PlayerListingsMenu _playerListingsMenu;

    [SerializeField]
    private LeaveRoomMenu _leaveRoomMenu;       //Basically, this is a button we're using to leave the room.  It's just a place that holds the code to do that.  Don't know that I would normally do it that way myself, but the guy running the tutorial does.

    //We'll create a variable to check leaving the room to basically kick everyone out when the master leaves.
    public LeaveRoomMenu LeaveRoomMenu { get { return _leaveRoomMenu; } }

    //This is the main canvas that will act as a semi manager.
    private RoomsCanvases _roomsCanvases;

    /// <summary>
    /// This is the initializer.  To be called on the first initialization.  Basically, it will pass the semi manager along.
    /// </summary>
    /// <param name="canvases"></param>
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
        _playerListingsMenu.FirstInitialize(canvases);
        _leaveRoomMenu.FirstInitialize(canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
