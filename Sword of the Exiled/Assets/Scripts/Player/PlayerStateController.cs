using Photon.Pun;
using UnityEngine;

/// <summary>
/// This will not only control the player states, but the player controls as well.  I'm doing this because the states are
/// controlling the animation, and it's easier to track everything all together.
/// </summary>
public class PlayerStateController : MonoBehaviourPun
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

    //This will hold the player character's rigid body.  We'll just do this in the inspector.  Saves me a line of code later.
    [SerializeField]
    public Rigidbody rb;

    //I'm cheesing it.  The states will need the animator, so I'm just going to allow them to pull it from the state controller.  It just
    //makes it easier to access.
    [SerializeField]
    public Animator animator;

    //Create public bools to help track the states we are in.  The bools will be tracked here and the states will have access to them.
    public bool isSneaking;
    public bool isSprinting;

    //This variable will be used to determine how the player should move.  It tracks direction is all.  This will be accessed by the states.
    public Vector3 movement;

    //This variable is the movement speed.  It will be tracked by the states just like the movement.  However, the states will actually update this value.
    public float moveSpeed;

    //I'm creating some values that we can modify in the inspector just so that we can maninpulate the speeds better.
    [SerializeField]
    public float walkingSpeed = 5.0f;
    [SerializeField]
    public float sneakingSpeed = 2.5f;
    [SerializeField]
    public float sprintingSpeed = 8.0f;

    //Variables
    public PlayerState currentPlayerState;

    // Start is called before the first frame update
    void Start()
    {
        //Check to see if the player is mine.  If not, find the camera and deactivate it.  Used for multiplayer.
        //if (!base.photonView.IsMine)
        //{
        //    Camera cam = GetComponentInChildren<Camera>();
        //    cam.enabled = false;
        //}

        //Set defaults for motion tracking variables.
        isSneaking = false;
        isSprinting = false;

        //Set the initial state
        SetPlayerState(new WaitPlayerState(this));

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //First, get player input for motion.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        //Next, check to see if either the sprint or sneak buttons were hit.
        //We are only going to allow one at a time, so we can check for them both
        //in one if else.  Basically though, they will be toggle switches.  Not
        //hold downs anymore.
        if (Input.GetButtonDown("Sneak") == true)
        {
            Debug.Log("Player toggled sneaking");
            //The player hit the sneaking button.  So set sneaking to the opposite of whatever it is.
            isSneaking = !isSneaking;

            //Set is sprinting to false no matter what.
            isSprinting = false;
        }
        else if (Input.GetButtonDown("Sprint") == true)
        {
            Debug.Log("Player toggled sprinting");

            //Player hit the sprint button.  Toggle it.
            isSprinting = !isSprinting;

            //Set sneaking to false, no matter what.
            isSneaking = false;
        }

        //Now, have the states do their work.
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

    /// <summary>
    /// This is the fixed update which will actually track movement. The values this uses for determining
    /// movement will be set by the states themselves as they all have access to this class.
    /// </summary>
    private void FixedUpdate()
    {
        ////Move the rigid body.
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //Next, we'll check for rotation stuff.
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        }
        //transform.Translate(movement * moveSpeed * Time.fixedDeltaTime, Space.World);

        //Vector3 movement2 = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);


        //transform.Translate(movement2 * moveSpeed * Time.fixedDeltaTime, Space.World);
    }
}