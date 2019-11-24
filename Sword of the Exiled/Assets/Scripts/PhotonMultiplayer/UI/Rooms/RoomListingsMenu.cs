using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pulled from First Gear Games on YouTube.
/// </summary>
public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;         //This will basically just determine where we show stuff.
    [SerializeField]
    private RoomListing _roomListing;   //This is the prefab that we will be showing our rooms to users in.  Well, it will be the script in the prefab.

    private List<RoomListing> _listings = new List<RoomListing>();  //Listing to hold all the rooms.
    private RoomsCanvases _roomsCanvases;       //We are going to track some stuff for the canvases as we need to be able to go back and forth.

    /// <summary>
    /// This will set our _roomsCanvases.  Should be called when this is created.  
    /// </summary>
    /// <param name="canvases"></param>
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    /// <summary>
    /// When a room is joined, this will take us to that room canvas.
    /// </summary>
    public override void OnJoinedRoom()
    {
        _roomsCanvases.CurrentRoomCanvas.Show();

        //Destroy room listings when you join a room.
        _content.DestroyChildren();

        //We've destroyed stuff, so clear out our listing now.
        _listings.Clear();

    }

    /// <summary>
    /// This will run every time the room list is updated.  We will put the value into our room list and show it to the user.
    /// </summary>
    /// <param name="roomList"></param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //First, run through each room in the roomList.
        foreach (RoomInfo info in roomList)
        {
            //Removed from rooms list.
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            //Added to rooms list.
            else
            {
                //Check to see if the room already exists.
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {

                    //Create a RoomListing script which happens to exist in the prefab.  I guess this is a shortcut rather than just saying
                    //"grab the script from the prefab after we have instantiated it".
                    RoomListing listing = Instantiate(_roomListing, _content);

                    //So, this shouldn't ever happen.  But jsut in case, set some data.
                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);
                        _listings.Add(listing);
                    }
                }
                else
                {
                    //Modify listing here.
                    //_listings[index].dowhatever.
                }
            }
        }
    }
}