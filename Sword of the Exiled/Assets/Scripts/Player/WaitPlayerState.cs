using UnityEngine;

/// <summary>
/// This state is used for a player standing still.
/// </summary>
public class WaitPlayerState : PlayerState
{
    private PlayerStateController.PlayerStateType pst = PlayerStateController.PlayerStateType.waiting;

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="playerStateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    public WaitPlayerState(PlayerStateController playerStateController) : base(playerStateController) { }

    /// <summary>
    /// What to do when entering the normal player state.
    /// </summary>
    public override void OnStateEnter()
    {
        //Set animation stuff.
        animator.SetBool("sprinting", false);
        animator.SetBool("sneaking", false);
        animator.SetBool("idle", true);
        animator.SetBool("walking", false);

        //Set player move speed.
        playerStateController.moveSpeed = 0.0f;

        //Debug.Log("Player state is sneaking");
        pst = PlayerStateController.PlayerStateType.waiting;
    }

    public override void OnStateExit()
    {
        //Debug.Log("Leaving the sneaking player state");
    }

    /// <summary>
    /// What to do in the normal player state.
    /// </summary>
    public override void Act()
    {
    }

    /// <summary>
    /// See if a transition needs to be made into another player state.
    /// </summary>
    public override void CheckTransitions()
    {
        //First, check to see if there is any motion.  Clearly, we don't want to be in wait state if the player is moving.
        if (playerStateController.movement != Vector3.zero)
        {
            //Debug.Log("There should be motion for the player.");
            //Check for sprinting first.  
            if (playerStateController.isSprinting)
            {
                //Enter the sprinting player state.
                playerStateController.SetPlayerState(new SprintingPlayerState(playerStateController));
            }
            //Check for sneaking next.
            else if (playerStateController.isSneaking)
            {
                //Enter the player sneaking state.
                playerStateController.SetPlayerState(new SneakingPlayerState(playerStateController));
            }
            //It's just normal movement, so go with normal movement.
            else
            {
                playerStateController.SetPlayerState(new NormalPlayerState(playerStateController));
            }
        }
        //So, technically, there is a sneaky wait state. I don't know that we built out the animation for it, but whatever.  I guess I'll check it here.
        else
        {
            //Check to see if we should be in sneak wait instead of regular stand around wait.
            if (playerStateController.isSneaking)
            {
                //Go to sneaky wait player state.
                playerStateController.SetPlayerState(new SneakyWaitPlayerState(playerStateController));
            }
        }

        //Debug.Log("There is no motion for the player");
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
