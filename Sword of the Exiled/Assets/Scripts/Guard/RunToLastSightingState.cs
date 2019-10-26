using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToLastSightingState : State
{
    Vector3 destination;

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="stateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    public RunToLastSightingState(StateController stateController) : base(stateController) { }

    /// <summary>
    /// See if a transition needs to be made into another state.
    /// </summary>
    public override void CheckTransitions()
    {
        //Here, we will check to see if we can see the target.  If so, start chasing.
        if (stateController.gs.playerInSight)
        {
            stateController.SetState(new ChaseState(stateController));
        }
        //Check to see if destination is reached.  If so, enter the search state.
        else if (stateController.ai.DestinationReached())
        {
            stateController.SetState(new SearchState(stateController));
        }
    }

    /// <summary>
    /// What to do in the run to last sighting state.
    /// </summary>
    public override void Act()
    {
        //First, check to see what if we have a target.
        if (destination == null)
        {
            //Debug.Log("Current destination is: " + destination.ToString());
            //Get the next nav point, and set it as the ai target.
            destination = stateController.gs.personalLastSighting;

            //Debug.Log("New Destiation is: " + destination.ToString());
            stateController.ai.agent.SetDestination(destination);
        }

        //Next, we'll see if the player can be heard.  If so, we'll update the position we're going to.
        if (stateController.gs.playerIsHeard)
        {
            Debug.Log("We hear the player.  So go to where we can hear them.");
            //Get the next nav point, and set it as the ai target.
            destination = stateController.gs.personalLastSighting;
            stateController.ai.agent.SetDestination(destination);
        }
    }

    /// <summary>
    /// What to do when entering the run to last sighting state.
    /// </summary>
    public override void OnStateEnter()
    {
        //So, there are all these problems with the ai.setTarget, so we're going to kill that right now.
        stateController.ai.SetTarget(null);

        //First, set the next nav as the last sighting area.
        destination = stateController.gs.personalLastSighting;
        stateController.ai.agent.SetDestination(destination);

        //Now, change the color to yellow.
        stateController.ChangeColor(Color.yellow);

        //Last, set speed of agent.
        stateController.ai.TargetSpeed = 0.75f;
    }
}
