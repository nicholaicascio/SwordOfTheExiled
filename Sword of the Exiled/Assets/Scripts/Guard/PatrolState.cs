using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    Vector3 destination;

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="stateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    public PatrolState(StateController stateController) : base(stateController) { }

    /// <summary>
    /// See if a transition needs to be made into another state.
    /// </summary>
    public override void CheckTransitions()
    {
        //Check if player is in sight.  If so, change the target.
        if (stateController.gs.playerInSight)
        {
            //Debug.Log("Player detected!");
            stateController.SetState(new ChaseState(stateController));
        }
        //Now, check to see if the player can be heard.
        else if (stateController.gs.playerIsHeard)
        {
            stateController.SetState(new InvestigateSoundState(stateController));
        }
        //Check to see if destination is reached.  If so, enter the wait state.
        else if (stateController.ai.DestinationReached())
        {
            stateController.SetState(new WaitState(stateController));
        }
    }

    /// <summary>
    /// What to do in the patrol state.
    /// </summary>
    public override void Act()
    {
        //First, check to see what if we have a target, or if the destination has been reached.
        if(destination == null)
        {
            //Debug.Log("Current destination is: " + destination.ToString());
            //Get the next nav point, and set it as the ai target.
            destination = stateController.GetNextNavPoint();

            //Debug.Log("New Destiation is: " + destination.ToString());
            stateController.ai.agent.SetDestination(destination);
        }
    }

    /// <summary>
    /// What to do when entering the patrol state.
    /// </summary>
    public override void OnStateEnter()
    {
        //So, there are all these problems with the ai.setTarget, so we're going to kill that right now.
        stateController.ai.SetTarget(null);

        //First, set the next nav point.
        destination = stateController.GetNextNavPoint();
        stateController.ai.agent.SetDestination(destination);

        //Now, change the color to blue.
        stateController.ChangeColor(Color.blue);

        //Last, set speed of agent.
        stateController.ai.TargetSpeed = 0.5f;
    }
}
