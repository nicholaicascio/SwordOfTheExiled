using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Pulled from First Gear Games on YouTube.
/// </summary>
public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Transform _content;         //This will basically just determine where we show stuff.
    [SerializeField]
    private PlayerListing _playerListing;   //This is the prefab that we will be showing our players in the room.  Well, it will be the script in the prefab.
    [SerializeField]
    private Text _readyUpText;              //This is for the ready text.  You know, so we know when players are ready to start the game!

    private List<PlayerListing> _listings = new List<PlayerListing>();  //This will be a list of all the players in the room.

    //A link to our cavas thing that we're basically considering a mini manager.
    private RoomsCanvases _roomsCanvases;

    //This is so we know if the player is ready to start the game or not.
    private bool _ready = false;

    /// <summary>
    /// This is replacing the Awake funciton we had before.
    /// </summary>
    public override void OnEnable()
    {
        //Do whatever the base class does.
        base.OnEnable();

        //Call our self made function that does a thing.
        SetReadyUp(false);

        //Get the current room players to display.
        GetCurrentRoomPlayers();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnDisable()
    {
        base.OnDisable();

        //So, we are going to run through our list and destory everything in it.
        for(int i = 0; i < _listings.Count; i++)
        {
            Destroy(_listings[i].gameObject);
        }

        //I imagine we also want to empty out the list here.
        _listings.Clear();
    }

    //Get refernce to semi manager.  Should be called when first created.
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    /// <summary>
    /// Tell if the player is ready to play the game or not.
    /// </summary>
    /// <param name="state">True if ready to play.  False if not ready to play.</param>
    private void SetReadyUp(bool state)
    {
        _ready = state;
        if (state)
        {
            _readyUpText.text = "R";
        }
        else
        {
            _readyUpText.text = "N";
        }
    }

    /// <summary>
    /// To do when leaving the room. Honestly, not sure if this is being replaced by OnDisable or not.
    /// Update: yes.  He deleted this entirely.  I'm a bit unclear as to why we don't use the destroy children thing we made though.
    /// </summary>
    //public override void OnLeftRoom()
    //{

    //    //Destroy content for play listings when leaving the room.
    //    _content.DestroyChildren();

    //    //I imagine we also want to empty out the list here.
    //    _listings.Clear();
    //}

    /// <summary>
    /// This will get the current players in the room.
    /// </summary>
    private void GetCurrentRoomPlayers()
    {
        //Make sure we're connected to Photon.
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        //Next, make sure we're in a room and that there are players in the room.
        if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
        {
            return;
        }
        //Get a dictionary of players. Good times.
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            //Add player to teh player listing.
            AddPlayerListing(playerInfo.Value);
        }
    }

    /// <summary>
    /// This will add a player to the listing sowe can see them.  Yay!
    /// There were issues with Photon with players being added multiple times.  So we deal with that here.
    /// </summary>
    /// <param name="player"></param>
    private void AddPlayerListing(Player player)
    {
        //First, check to see if the listing exists.
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            //We found the player, so we don't just want to add them again.  Just update the data.
            _listings[index].SetPlayerInfo(player);
        }
        else
        {
            //Create a PlayerListing script which happens to exist in the prefab.  I guess this is a shortcut rather than just saying
            //"grab the script from the prefab after we have instantiated it".
            PlayerListing listing = Instantiate(_playerListing, _content);

            //So, this shouldn't ever happen.  But jsut in case, set some data.
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
            }
        }
    }

    /// <summary>
    /// Called when the master client is switched in the game like when the master client changes, or the master client leaves.  SO, you can actually switch master
    /// say if the master is laggy.
    /// But we're going to leave the room if the master leaves the room.
    /// </summary>
    /// <param name="newMasterClient"></param>
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //click the leave room buttom for the player.
        _roomsCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    /// <summary>
    /// This will run when a player enters the room.  It will add them to the listing so that other in the room can see them.
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //Add new player to the listing.
        AddPlayerListing(newPlayer);
    }

    /// <summary>
    /// This will run when a player leaves the room.  It will remove them from the listing so they are no longer seen.
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    /// <summary>
    /// Start the game.  Should only be used by game master.
    /// </summary>
    public void OnClick_StartGame()
    {
        //Make sure that only the master client can start the game.
        if (PhotonNetwork.IsMasterClient)
        {
            //TODO:
            //Put this back in. For testing, we aren't going to make everyone check in.  But we really would want that though.
            //Loop through all the players to see if they are ready.
            for (int i = 0; i < _listings.Count; i++)
            {
                //Make sure we don't check for the master client.
                if (_listings[i].Player != PhotonNetwork.LocalPlayer)
                {
                    //Check to see if this player is not ready.  If they aren't, we'll exit.
                    //So if any player isn't ready, we can't start yet.
                    if (!_listings[i].Ready)
                    {
                        return;
                    }
                }
            }

            //Stop others from joining the room.
            PhotonNetwork.CurrentRoom.IsOpen = false;

            //Stop room from listing as visible for others.
            PhotonNetwork.CurrentRoom.IsVisible = false;
            
            //Load the scene.
            PhotonNetwork.LoadLevel(1);
        }
    }

    /// <summary>
    /// Notify that you are ready to play.  Should only be used if not the master of the game.
    /// </summary>
    public void OnClick_ReadyUp()
    {
        //So, this should only be used if you are not the master of the game.
        if (!PhotonNetwork.IsMasterClient)
        {
            //Set ready to the opposite of what it is.
            SetReadyUp(!_ready);

            //So, you can use the RpcTarget.Buffered stuff so that late joiners can get the data.
            //Others sends it to everyone but the sender.
            //We re going to send to the master.  And we are going to send our ready state.
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient,PhotonNetwork.LocalPlayer,_ready);
            //This is how you would send a secure version.  Cool!
            //base.photonView.RPCSecure("RPC_ChangeReadyState", RpcTarget.MasterClient,true, PhotonNetwork.LocalPlayer, _ready);

            //This can be done, but apparently it's not popular.
            //PhotonNetwork.RemoveRPCs();
        }
    }

    /// <summary>
    /// This is a network function.
    /// We're going to run through the list of players and set the ready status for the one we got the RPC from.
    /// </summary>
    /// <param name="ready"></param>
    [PunRPC]
    public void RPC_ChangeReadyState(Player player, bool ready)
    {
        //Get the index of the player that was passed in.
        int index = _listings.FindIndex(x => x.Player == player);

        //Check to see if the player was found.
        if (index != -1)
        {
            //Player found, so set the value of Ready for the player to the value that was passed in.
            _listings[index].Ready = ready;
        }
    }
}