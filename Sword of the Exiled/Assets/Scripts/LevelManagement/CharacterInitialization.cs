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

    //This will be our prefab for the guard.
    [SerializeField]
    private GameObject _guardPrefab;

    //Here, we will have a spawn point for the guard.
    [SerializeField]
    private GameObject _guardSpawn;

    //Guard trapper.  Now, I realize that this should not all be done here.  But we are coming
    //close to the end of our available time, so here is where it goes.
    [SerializeField]
    private GuardTrapper _guardTrapper;

    // Start is called before the first frame update
    void Start()
    {
        //We will set the master player to different places than the joiner.
        if (PhotonNetwork.IsMasterClient)
        {
            MasterClientStart();
        }
        else
        {
            SlaveClientStart();
        }
    }

    /// <summary>
    /// This will handled all the things that the master is going to have to do.
    /// Instantiate self, guards. etc.
    /// </summary>
    private void MasterClientStart()
    {
        //First, instanitate the player.
        MasterManager.NetworkInstantiate(_playerPrefab, _hostSpawn.transform.position, Quaternion.identity);

        //Next, instantiate the guard.  We will immediately give that guard to the guard trapper manager.
        //Now that we have a guard, go ahead and give the guard trapper the guard to manage.
        _guardTrapper.guards[0] = MasterManager.NetworkInstantiate(_guardPrefab, _guardSpawn.transform.position, Quaternion.identity);

    }

    /// <summary>
    /// This is going to handle all the things that the slave is going to have to do.
    /// Instantiate self.  
    /// That's pretty much it.
    /// </summary>
    private void SlaveClientStart()
    {
        MasterManager.NetworkInstantiate(_playerPrefab, _joinSpawn.transform.position, Quaternion.identity);
    }
}
