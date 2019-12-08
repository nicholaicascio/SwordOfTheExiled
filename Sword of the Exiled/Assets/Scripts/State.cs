using UnityEngine;

public abstract class State
{
    protected StateController stateController;      //state controller so that there is always a reference to it.  This way we will know what is in controll and the controller will be changing states.
    protected Animator animator;                    //Animator of the object.  It will be passed in from the state controller.  We just need to make sure it is set.

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

    /// <summary>
    /// Constructor.  Must pass in the state controller, and the animator.
    /// </summary>
    /// <param name="stateController">State controller in charge of all states.</param>
    /// <param name="anim">Animator of the oject.</param>
    public State(StateController stateController)
    {
        //Debug.Log("Setting the state");
        this.stateController = stateController; //Reference to the stateController.
        this.animator = stateController.animator;                   //Reference to the animator for the object.
        //Debug.Log("Finished setting state.");
    }

}
