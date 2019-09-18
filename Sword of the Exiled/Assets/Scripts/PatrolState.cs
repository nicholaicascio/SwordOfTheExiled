using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    Transform destination;

    public PatrolState(StateController stateController) : base(stateController) { }

    /// <summary>
    /// 
    /// </summary>
    public override void CheckTransitions()
    {
        //Check if player is in range.  If so, change the target.
        if (stateController.CheckIfInRange("Player"))
        {
            //Debug.Log("Player detected!");
            stateController.SetState(new ChaseState(stateController));
        }
    }

    /// <summary>
    /// What to do in the patrol state.
    /// </summary>
    public override void Act()
    {
        //First, check to see what if we have a target, or if the destination has been reached.
        if(destination == null || stateController.ai.DestinationReached())
        {
            //Get the next nav point, and set it as the ai target.
            destination = stateController.GetNextNavPoint();
            stateController.ai.SetTarget(destination);
        }
    }

    /// <summary>
    /// Set the color to blue when we enter the state.
    /// </summary>
    public override void OnStateEnter()
    {
        //First, set the next nav point.
        destination = stateController.GetNextNavPoint();
        stateController.ai.SetTarget(destination);

        //Now, change the color to blue.
        stateController.ChangeColor(Color.blue);

        //Last, set speed of agent.
        stateController.ai.TargetSpeed = 0.5f;
    }
}
