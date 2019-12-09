using UnityEngine;

public class NormalPlayerState : PlayerState
{
    private PlayerStateController.PlayerStateType pst = PlayerStateController.PlayerStateType.normal;

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="playerStateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
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

        //Set player move speed.
        playerStateController.moveSpeed = playerStateController.walkingSpeed;

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
    }

    /// <summary>
    /// See if a transition needs to be made into another player state.
    /// </summary>
    public override void CheckTransitions()
    {
        //Easy check first.  See if the player stopped moving.
        if (playerStateController.movement == Vector3.zero)
        {
            //We're clearly not sneaking as we are in the normal state, so just do the normal wait state.
            playerStateController.SetPlayerState(new WaitPlayerState(playerStateController));
        }
        else
        {
            //It doesn't particularly matter which order we check for these in.  So let's check for sprint first.
            if (playerStateController.isSprinting)
            {
                //Go into sprint mode.
                playerStateController.SetPlayerState(new SprintingPlayerState(playerStateController));
            }
            //Now, we'll check to see if we are sneaking.
            else if (playerStateController.isSneaking)
            {
                playerStateController.SetPlayerState(new SneakingPlayerState(playerStateController));
            }
        }
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
