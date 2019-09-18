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
        //Check to see if the player is out of range.
        if (!stateController.CheckIfInRange("Player"))
        {
            //Go back to patrolling.
            stateController.SetState(new PatrolState(stateController));
        }
    }

    /// <summary>
    /// What to do in the patrol state.
    /// </summary>
    public override void Act()
    {
        if(stateController.targetToChase != null)
        {
            destination = stateController.targetToChase.transform;
            stateController.ai.SetTarget(destination);
        }
    }

    /// <summary>
    /// Set the color to red when we enter the state.
    /// </summary>
    public override void OnStateEnter()
    {
        //First, set the color to red.
        stateController.ChangeColor(Color.red);

        //Next, set speed of agent.
        stateController.ai.TargetSpeed = 0.85f;

    }
}
