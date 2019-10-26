﻿public abstract class PlayerState
{
    protected PlayerStateController playerStateController;
    private PlayerStateController.PlayerStateType pst;

    /// <summary>
    /// Must be overridden.  Code what to look for during transition.
    /// </summary>
    public abstract void CheckTransitions();

    /// <summary>
    /// Must be overridden.  Determine what to do while in this state.
    /// </summary>
    public abstract void Act();

    /// <summary>
    /// Must be overridden.  Determine what must be done when entering the state.
    /// </summary>
    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }

    //Constructor
    public PlayerState(PlayerStateController playerStateController)
    {
        this.playerStateController = playerStateController;
    }

    /// <summary>
    /// Return the player state type.
    /// </summary>
    /// <returns>PlayerStateController.PlayerStateType</returns>
    public abstract PlayerStateController.PlayerStateType GetPlayerStateType();

}
