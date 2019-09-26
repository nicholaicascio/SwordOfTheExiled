using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    Transform destination;

    public ChaseState(StateController stateController) : base(stateController) { }

    /// <summary>
    /// Check to see if a transition is necessary.
    /// </summary>
    public override void CheckTransitions()
    {
        //So, we need to transition if we can no longer see the player.  We'll just go to the last place they were seen.
        if (!stateController.gs.playerInSight)
        {
            stateController.SetState(new RunToLastSightingState(stateController));
        }
    }

    /// <summary>
    /// What to do in the patrol state.
    /// </summary>
    public override void Act()
    {
        //Get the next nav point, and set it as the ai target.
        destination = stateController.gs.personalLastSightingTransform;
        stateController.ai.SetTarget(destination);
    }

    /// <summary>
    /// Set the color to red when we enter the state.
    /// </summary>
    public override void OnStateEnter()
    {
        //First, set the color to red.
        stateController.ChangeColor(Color.red);

        //Next, set speed of agent.
        stateController.ai.TargetSpeed = 0.75f;

    }
}
