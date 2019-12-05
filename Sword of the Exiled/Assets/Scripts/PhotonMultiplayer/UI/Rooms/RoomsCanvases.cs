using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is going to act as a semi manager for the canvases.
/// </summary>
public class RoomsCanvases : MonoBehaviour
{
    //Set a field to track the create or join room canvas.
    [SerializeField]
    private CreateOrJoinRoomCanvas _createOrJoinRoomCanvas;

    //This is the canvas of the create or join room canvas where users either create a room or join a room.
    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas
    { get { return _createOrJoinRoomCanvas; } }

    //Set a field to track the current room canvas.
    [SerializeField]
    private CurrentRoomCanvas _currentRoomCanvas;

    //This is the canvas of the room once a user has either created or joined a room.
    public CurrentRoomCanvas CurrentRoomCanvas { get { return _currentRoomCanvas; } }

    /// <summary>
    /// This runs when the script is first built.
    /// </summary>
    private void Awake()
    {
        FirstInitialize();
    }

    /// <summary>
    /// This will intialize our canvases and pass itself as a reference to the sub canvases that are to be controlled.
    /// </summary>
    private void FirstInitialize()
    {
        //Debug.Log("FirstInitialize for the RoomCanvases");
        CreateOrJoinRoomCanvas.FirstInitialize(this);
        CurrentRoomCanvas.FirstInitialize(this);
    }

}
