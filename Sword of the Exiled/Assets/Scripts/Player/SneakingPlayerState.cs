using UnityEngine;

public class SneakingPlayerState : PlayerState
{
    private PlayerStateController.PlayerStateType pst = PlayerStateController.PlayerStateType.sneaking;

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="playerStateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    public SneakingPlayerState(PlayerStateController playerStateController) : base(playerStateController) { }

    /// <summary>
    /// What to do when entering the normal player state.
    /// </summary>
    public override void OnStateEnter()
    {
        //Set animation stuff.
        animator.SetBool("sprinting", false);
        animator.SetBool("sneaking", true);
        animator.SetBool("idle", false);
        animator.SetBool("walking", false);

        //Set player move speed.
        playerStateController.moveSpeed = playerStateController.sneakingSpeed;

        //Debug.Log("Player state is sneaking");
        pst = PlayerStateController.PlayerStateType.sneaking;
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
        //Easy check first.  See if the player stopped moving.
        if (playerStateController.movement == Vector3.zero)
        {
            //So, if we are sneaking, and we stop moving, we know we need to go
            //to the sneaky wait state.  It's not possible to need to go to the
            //regular wait state, so don't even bother.
            playerStateController.SetPlayerState(new SneakyWaitPlayerState(playerStateController));
        }
        else
        {
            //We are still moving, but we'll need to see if the sprint button has been toggled off
            //or if we've been sent into sneak mode.
            //First, we'll check sprint mode.  We do this because going into sprint mode turns the sneak
            //off, so we don't want to check sneak first because we won't know if we're sprinting or just walking.
            if (playerStateController.isSprinting)
            {
                //Go into sprint mode.
                playerStateController.SetPlayerState(new SprintingPlayerState(playerStateController));
            }
            //Now, we'll check to see if we have stopped sneaking.  If so, go into normal player mode.
            else if (!playerStateController.isSneaking)
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
