using UnityEngine;

public class SneakyWaitPlayerState : PlayerState
{
    private PlayerStateController.PlayerStateType pst = PlayerStateController.PlayerStateType.sneakWaiting;
    private Vector3 movement;

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="stateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    public SneakyWaitPlayerState(PlayerStateController playerStateController) : base(playerStateController) { }

    /// <summary>
    /// What to do when entering the normal player state.
    /// </summary>
    public override void OnStateEnter()
    {
        //Debug.Log("Player state is sneaking");
        pst = PlayerStateController.PlayerStateType.sneakWaiting;
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
            //If we've hit the sneak button, start sneaking.
            else if (Input.GetButtonDown("Sneak") == true)
            {
                playerStateController.SetPlayerState(new SneakingPlayerState(playerStateController));
            }
            //It's just normal movement, so go with normal movement.
            else
            {
                playerStateController.SetPlayerState(new NormalPlayerState(playerStateController));
            }
        }

        //There may not be movement, but we'll still need to see if we're switching between sneaky waiting and just waiting.
        if (Input.GetButtonDown("Sneak") == true)
        {
            playerStateController.SetPlayerState(new WaitPlayerState(playerStateController));
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
