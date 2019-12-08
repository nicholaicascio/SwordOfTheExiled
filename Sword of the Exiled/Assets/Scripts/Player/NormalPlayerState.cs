using UnityEngine;

public class NormalPlayerState : PlayerState
{
    private PlayerStateController.PlayerStateType pst = PlayerStateController.PlayerStateType.normal;
    private Vector3 movement;

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="stateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    public NormalPlayerState(PlayerStateController playerStateController) : base(playerStateController) { }

    /// <summary>
    /// What to do when entering the normal player state.
    /// </summary>
    public override void OnStateEnter()
    {
        //Set animation stuff.
        animator.SetBool("sprinting", false);
        animator.SetBool("sneaking", false);
        animator.SetBool("idle", false);
        animator.SetBool("walking", true);

        //Debug.Log("Player state is normal");
        pst = PlayerStateController.PlayerStateType.normal;
    }

    public override void OnStateExit()
    {
        //Debug.Log("Leaving the normal player state.");
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
            //If ever we hit the run button, start sprinting.  
            if (Input.GetButton("Sprint"))
            {
                playerStateController.SetPlayerState(new SprintingPlayerState(playerStateController));
            }
            else if (Input.GetButtonDown("Sneak") == true)
            {
                playerStateController.SetPlayerState(new SneakingPlayerState(playerStateController));
            }
        }

        //No motion detected, so we will need to set this back to wait state.
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
