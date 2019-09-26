using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : State
{
    Transform destination;
    private float timecheck;        //For some reason, Time.time returns the number of seconds since the beginning fo the game.  So we'll track that here.

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="stateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    public SearchState(StateController stateController) : base(stateController) { }

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
        //Next, we'll see if the player can be heard.  If so, we'll update the position we're going to.
        else if (stateController.gs.playerIsHeard)
        {
            stateController.SetState(new RunToLastSightingState(stateController));
        }
        //Check timer so that the guard will stay for some amount of time before going back on patrol.
        else if (Time.time - timecheck > stateController.searchWaitTime)
        {
            //We've waited as long as we need to, so go to the next patrol point.
            stateController.SetState(new PatrolState(stateController));
        }
    }

    /// <summary>
    /// What to do in the search state.
    /// </summary>
    public override void Act()
    {
        //Just searching, so no real need to do anything yet.  Might have active searching and turning at some point.
    }

    /// <summary>
    /// What to do when entering the search state.
    /// </summary>
    public override void OnStateEnter()
    {
        //Now, change the color to magenta.
        stateController.ChangeColor(Color.magenta);

        //Last, set speed of agent.
        stateController.ai.TargetSpeed = 0.75f;

        //Set our timer so that we can see how long we've been waiting.
        timecheck = Time.time;
    }
}
