using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// Pulled from First Gear Games on YouTube.
/// </summary>
public class TestConnect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Connecting to Photon: ", this);

        //Send rate.  This is used for stuff that changes frequently.  It's defaulted to 20.
        //How many packets you can send per second.
        PhotonNetwork.SendRate = 20;

        //This is also for network stuff that changes frequently.  Defaults to 10.
        //This is the important part, and problematic.  Used for transform position, ration, etc.
        //So, this sends data per second.  That's why this rate should be lower than the send
        //rate.  Otherwise, it will take up all the send rate.  In the editor, the photon transrom
        //view uses this behind the sense.
        //SO, the best way to handle this is to use the PhotonTransformViewClassic, and set the
        //interpolation option to estimated. (at least for slow moving objects).
        PhotonNetwork.SerializationRate = 5;

        //Easily load scenes between players.
        PhotonNetwork.AutomaticallySyncScene = true;
        //Set the nickname.
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;

        //Locks game version for users.  This will make sure that only certain game
        //versions can work together.  It blocks out other versions.
        //PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;

        //Connect using the settings in Photon->PhotonUnityNetworking->Resources->
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// This runs when we have connected to the master.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server.",this);
        Debug.Log("Nickname: " + PhotonNetwork.LocalPlayer.NickName, this);

        //Join the lobby now so we can get room lists, if we're not in a lobby already.
        //Important when moving from room to room.
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    /// <summary>
    /// This runs when the disconnect has happened.  Could be a failure or a disconnect call.
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason: " + cause.ToString(), this);
        //DisconnectCause.ClientTimeout
    }
}
