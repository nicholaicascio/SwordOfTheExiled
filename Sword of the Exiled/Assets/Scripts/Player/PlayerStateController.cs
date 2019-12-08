using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    //Create an enum for the states that we'll use I guess.
    public enum PlayerStateType
    {
        normal = 0,
        sneaking = 1,
        sprinting = 2,
        waiting = 3,
        sneakWaiting = 4
    }


    //I'm cheesing it.  The states will need the animator, so I'm just going to allow them to pull it from the state controller.  It just
    //makes it easier to access.
    [SerializeField]
    public Animator animator;

    //Variables
    public PlayerState currentPlayerState;

    // Start is called before the first frame update
    void Start()
    {

        //Set the initial state
        //SetState(new PatrolState(this));
        SetPlayerState(new WaitPlayerState(this));

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        currentPlayerState.CheckTransitions();
        currentPlayerState.Act();
    }

    /// <summary>
    /// Set the player state.
    /// </summary>
    /// <param name="playerState">PlayerState class.</param>
    public void SetPlayerState(PlayerState playerState)
    {
        if (currentPlayerState != null)
        {
            currentPlayerState.OnStateExit();
        }

        currentPlayerState = playerState;
        gameObject.name = "Player agent in state " + playerState.GetType().Name;

        if (currentPlayerState != null)
        {
            currentPlayerState.OnStateEnter();
        }

        //Debug.Log("Player is now in state: " + currentPlayerState.GetPlayerStateType());
    }

    /// <summary>
    /// Get the player state type.  Easy way to get the player state.
    /// </summary>
    /// <returns></returns>
    public PlayerStateType getCurrentPlayerState()
    {
        return currentPlayerState.GetPlayerStateType();
    }
}