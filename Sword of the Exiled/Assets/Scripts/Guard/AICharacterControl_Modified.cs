using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl_Modified : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        public float TargetSpeed;                                   // I added a target speed because I don't know when the agent is set, but it gets set after I try to set the speed in the states.


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            //Debug.Log("Now the agent exists!");
            
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
                agent.speed = TargetSpeed;
            }

            if (!DestinationReached())
                character.Move(agent.desiredVelocity, false, false);
            else
                character.Move(Vector3.zero, false, false);
        }

        /// <summary>
        /// Determine if AI has reached the destination or not.  This uses the stopping distance set in the Nav Mesh Agent for the game object.
        /// </summary>
        /// <returns>True if destination has been reached.  False if the destination has not been reached.</returns>
        public bool DestinationReached()
        {
            //Is a path in the process of being computed but not yet ready?
            if (!agent.pathPending)
            {
                //Path computing is done.  We had to check because otherwise this would return true more than once.  Great times.
                //Debug.Log("Distance remaining: " + agent.remainingDistance);
                return agent.remainingDistance < agent.stoppingDistance;
            }

            return false;
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
