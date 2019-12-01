using Photon.Pun;
using UnityEngine;

/// <summary>
/// This script is used to initialize players in the game.
/// </summary>
public class CharacterInitialization : MonoBehaviour
{
    //This is the player prefab.  In the future, we may have different
    //prefabs for different players.
    [SerializeField]
    private GameObject _playerPrefab;

    //This is a two player game, so we need two points to spawn from.
    //First, the host spawn point.
    [SerializeField]
    private GameObject _hostSpawn;

    //Next, is the joiner spawn.
    [SerializeField]
    private GameObject _joinSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //We will set the master player to different places than the joiner.
        if (PhotonNetwork.IsMasterClient)
        {
            MasterManager.NetworkInstantiate(_playerPrefab, _hostSpawn.transform.position, Quaternion.identity);
        }
        else
        {
            MasterManager.NetworkInstantiate(_playerPrefab, _joinSpawn.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
