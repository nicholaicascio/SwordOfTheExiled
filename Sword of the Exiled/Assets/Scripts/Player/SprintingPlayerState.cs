using UnityEngine;

public class SprintingPlayerState : PlayerState
{
    private PlayerStateController.PlayerStateType pst = PlayerStateController.PlayerStateType.sprinting;

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="playerStateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    public SprintingPlayerState(PlayerStateController playerStateController) : base(playerStateController) { }

    /// <summary>
    /// What to do when entering the normal player state.
    /// </summary>
    public override void OnStateEnter()
    {
        //Set animation stuff.
        animator.SetBool("sprinting", true);
        animator.SetBool("sneaking", false);
        animator.SetBool("idle", false);
        animator.SetBool("walking", false);

        //Set player move speed.
        playerStateController.moveSpeed = playerStateController.sprintingSpeed;

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
    }

    /// <summary>
    /// See if a transition needs to be made into another player state.
    /// </summary>
    public override void CheckTransitions()
    {
        //Easy check first.  See if the player stopped moving.
        if (playerStateController.movement == Vector3.zero)
        {
            //So, if we are sprinting, and we stop moving, we know we need to go
            //to the regular wait state.  It's not possible to need to go to the
            //sneaky wait state, so don't even bother.
            playerStateController.SetPlayerState(new WaitPlayerState(playerStateController));
        }
        else { 
            //We are still moving, but we'll need to see if the sprint button has been toggled off
            //or if we've been sent into sneak mode.
            //First, we'll check sneak mode.  We do this because going into sneak mode turns the sprint
            //off, so we don't want to check sprint first because we won't know if we're sneaking or just walking.
            if (playerStateController.isSneaking)
            {
                //Go into sneak mode.
                playerStateController.SetPlayerState(new SneakingPlayerState(playerStateController));
            }
            //Now, we'll check to see if we have stopped sprinting.  If so, go into normal player mode.
            else if (!playerStateController.isSprinting)
            {
                playerStateController.SetPlayerState(new NormalPlayerState(playerStateController));
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
