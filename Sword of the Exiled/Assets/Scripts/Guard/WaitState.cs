using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{
    private float timecheck;        //For some reason, Time.time returns the number of seconds since the beginning fo the game.  So we'll track that here.

    /// <summary>
    /// This is the constructor.  Just use it.
    /// </summary>
    /// <param name="stateController">State controller script that called this state.  It should pass itself so this has a reference.</param>
    /// <param name="anim">animator of the object this is for.</param>
    public WaitState(StateController stateController) : base(stateController) {}

    /// <summary>
    /// What to do when entering the wait state.
    /// </summary>
    public override void OnStateEnter()
    {
        //Set the animation.
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdol", true);

        //So, there are all these problems with the ai.setTarget, so we're going to kill that right now.
        stateController.ai.SetTarget(null);

        //Now, change the color to green.
        stateController.ChangeColor(Color.green);

        //Last, set speed of agent.
        stateController.ai.TargetSpeed = 0f;

        //Set our timer so that we can see how long we've been waiting.
        timecheck = Time.time;
    }

    /// <summary>
    /// This was just something I did for Dr. Dan for class.  We had to have a state that would create new ai dudes.
    /// </summary>
    //public override void OnStateExit()
    //{
    //    stateController.NewGuard();
    //}

    /// <summary>
    /// What to do in the wait state.
    /// </summary>
    public override void Act()
    {
    }

    /// <summary>
    /// See if a transition needs to be made into another state.
    /// </summary>
    public override void CheckTransitions()
    {
        //Here, we will only check to see if we can see the target.
        if (stateController.gs.playerInSight)
        {
            //Debug.Log("Player seen!");
            stateController.SetState(new ChaseState(stateController));
        }
        //Check timer so that the guard will stay for some amount of time before leaving for the next destination.
        else if (Time.time - timecheck > stateController.patrolWaitTime)
        {
            //Debug.Log("Moving to next patrol point.");
            //We've waited as long as we need to, so go to the next patrol point.
            stateController.SetState(new PatrolState(stateController));
        }

    }
}
