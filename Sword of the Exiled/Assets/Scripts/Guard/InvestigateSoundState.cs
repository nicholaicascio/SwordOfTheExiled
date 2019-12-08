using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateSoundState : State
{
    Vector3 destination;

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="stateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    public InvestigateSoundState(StateController stateController) : base(stateController) { }

    /// <summary>
    /// See if a transition needs to be made into another state.
    /// </summary>
    public override void CheckTransitions()
    {
        //Check if player is seen.  If so, give chase!
        if (stateController.gs.playerInSight)
        {
            //Debug.Log("Player detected!");
            stateController.SetState(new ChaseState(stateController));
        }
        //Check to see if destination is reached.  If so, enter the wait state.
        else if (stateController.ai.DestinationReached())
        {
            stateController.SetState(new WaitState(stateController));
        }
    }

    /// <summary>
    /// What to do in the investigate sound state.
    /// </summary>
    public override void Act()
    {
        //First, check to see what if we have a target, or if the destination has been reached.
        if (destination == null)
        {
            //Debug.Log("Current destination is: " + destination.ToString());
            //Get the next nav point, and set it as the ai target.
            destination = stateController.gs.personalLastSighting;

            //Debug.Log("New Destiation is: " + destination.ToString());
            stateController.ai.agent.SetDestination(destination);
        }

        //Now, we'll check to see if the player can still be heard.  Since there is no visual confirmation, the guard will still slowly investigate the sound that's probably nothing.
        if (stateController.gs.playerIsHeard)
        {
            destination = stateController.gs.personalLastSighting;
            stateController.ai.agent.SetDestination(destination);
        }
    }

    /// <summary>
    /// What to do when entering the investigate sound state.
    /// </summary>
    public override void OnStateEnter()
    {
        //Set the animation.
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdol", false);

        //So, there are all these problems with the ai.setTarget, so we're going to kill that right now.
        stateController.ai.SetTarget(null);

        //First, set the next nav point.
        destination = stateController.gs.personalLastSighting;
        stateController.ai.agent.SetDestination(destination);

        //Now, change the color to cyan.
        stateController.ChangeColor(Color.cyan);

        //Last, set speed of agent.
        stateController.ai.TargetSpeed = 0.5f;
    }
}
