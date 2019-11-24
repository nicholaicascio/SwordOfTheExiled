using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Pulled from First Gear Games on YouTube.
/// </summary>
public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _text;     //Text field that we show data in.

    public Player Player { get; private set; }
    public bool Ready = false;

    /// <summary>
    /// Set the player info for the current player
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        SetPlayerText();
    }

    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(target, changedProps);

        //Target should never be null, but make sure the target is the player.
        if(target != null && target == Player)
        {
            //Check to see if the random number key changed.
            if (changedProps.ContainsKey("RandomNumber"))
            {
                //Now, change the player text.
                SetPlayerText();
            }
        }
    }

    private void SetPlayerText()
    {
        int result = -1;

        //Check to see if a random number has been created.
        if (Player.CustomProperties.ContainsKey("RandomNumber"))
        {
            //Get the custom property that was set in RandomCustomPropertyGenerator
            result = (int)Player.CustomProperties["RandomNumber"];
        }

        _text.text = result.ToString() + ", " + Player.NickName;
    }
}
