using UnityEngine;

public class SprintingPlayerState : PlayerState
{
    private PlayerStateController.PlayerStateType pst = PlayerStateController.PlayerStateType.sprinting;
    private Vector3 movement;

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="stateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    public SprintingPlayerState(PlayerStateController playerStateController) : base(playerStateController) { }

    /// <summary>
    /// What to do when entering the normal player state.
    /// </summary>
    public override void OnStateEnter()
    {
        //Debug.Log("Player state is sprinting");
        pst = PlayerStateController.PlayerStateType.sprinting;
    }

    public override void OnStateExit()
    {
        //Debug.Log("Leaving the sprinting player state");
    }

    /// <summary>
    /// What to do in the normal player state.
    /// </summary>
    public override void Act()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// See if a transition needs to be made into another player state.
    /// </summary>
    public override void CheckTransitions()
    {
        //First, check to see if there is any motion.
        if (movement != Vector3.zero)
        {
            //If ever we stop hitting the run button, go back to normal mode.  
            if (!Input.GetButton("Sprint"))
            {
                playerStateController.SetPlayerState(new NormalPlayerState(playerStateController));
            }
            //If we say to sneak, we need to go to sneak mode.
            else if (Input.GetButtonDown("Sneak") == true)
            {
                playerStateController.SetPlayerState(new SneakingPlayerState(playerStateController));
            }
        }

        //There is no motion.  So, we'll go ahead and set this to waiting.
        playerStateController.SetPlayerState(new WaitPlayerState(playerStateController));
    }

    /// <summary>
    /// Return the player state type.
    /// </summary>
    /// <returns></returns>
    public override PlayerStateController.PlayerStateType GetPlayerStateType()
    {
        return pst;
    }
}
