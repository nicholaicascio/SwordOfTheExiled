using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSight : MonoBehaviour
{
    public float fieldOfViewAngle = 110.0f;     //How wide the guard can see.
    public bool playerInSight;                  //True when the enemy can see the player, false otherwise.
    public bool playerIsHeard;                  //True when the guard can hear the enemy.  Otherwise false.
    public Vector3 personalLastSighting;        //Last place the player was seen by this guard.  If there is an alarm or something, we can also set this so that all guards know where the player is.  Not really used in this script much.
    public Transform personalLastSightingTransform;   //Last transform of the player.  Needed this for some other stuff I was doing.  I use transforms in my code.  The tutorial used Vector3.

    private UnityEngine.AI.NavMeshAgent agent;      //The navmesh agent required for the path finding, and determining how far away the player is.  Specifically used for hearing.
    private SphereCollider sightCone;               //We'll use this for our detection cone.  Trust me, a given angle and distance from the center of a sphere creates a cone.
    private Animator anim;                          //Apparently the animator controller in this tutorial has a boolean to determine if the player has been seen.  Good then.
    private LastPlayerSighting lastPlayerSighting;  //This is the global last sighting of the player in this tutorial.  Might be cool to have.  
    private GameObject player;                      //The player object.
    private PlayerStateController playerStateController;              //This is how we will get the state of the player in our game.  
    private PlayerHealth playerHealth;              //Probably won't have this.
    private HashIDs hash;                           //Probably won't have this.  Used for comparing controller states.  So the concept might be good as it stores them.  TODO:  Figure out if this is useful.
    private Vector3 previousSighting;               //Stores the previous frames stored value.  This way we will always know the last place the player was seen.
    private Transform previousSightingTransform;    //Stores the previous frames stored value.  This way we will always know the last place the player was seen.

    private void Awake()
    {
        //Start off by setting a bunch of defaults and variables.
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        sightCone = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStateController = player.GetComponent<PlayerStateController>();
        playerHealth = player.GetComponent<PlayerHealth>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();


        //Here, we'll make sure that the last place the player was sighted is the same as the current place the player exists so that we don't chase them.
        //TODO:  Might change this to work differently in our game.
        personalLastSighting = lastPlayerSighting.resetPosition;
        previousSighting = lastPlayerSighting.resetPosition;

    }


    private void Update()
    {
        //Check to see if the global sighting of the player has changed.  If so, update the sighting of the player.  I imagine this will be to start chasing them when they are seen or alarms are set off?
        if(lastPlayerSighting.position != previousSighting)
        {
            personalLastSighting = lastPlayerSighting.position;
            personalLastSightingTransform = lastPlayerSighting.transform;
        }

        //Set the previous sighting to the last place the player was seen.  
        previousSighting = lastPlayerSighting.position;
        previousSightingTransform = lastPlayerSighting.transform;
    }

    /// <summary>
    /// This is going to check for anything being in our field of view.  But three conditions must be true:
    /// 1.  The player must be within the trigger zone.
    /// 2.  The player must be in front of the guard for the field of view.
    /// 3.  Nothing is blocking the guard view of the player.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        //Condition 1:
        //Check to see if the object in our sphere is the player.
        if (other.gameObject == player)
        {
            //Debug.Log("Player entered the danger zone");
            //Default player in sight to false just in case the other conditions are not met.
            playerInSight = false;
            playerIsHeard = false;

            //Now, get the directon of the player from the guard.
            Vector3 direction = other.transform.position - transform.position;

            //Next, get the angle of the player from the guard's forward facing position.  We wouldn't want to be able to see the player from behind us.
            float angle = Vector3.Angle(direction, transform.forward);

            //Condition 2:
            //Check to see if the angle is within our field of view.  Half of it of course because the field of view is a total, but we need to say that the
            //field of view goes both left and right so really we only want half.
            if (angle < fieldOfViewAngle * 0.5f)
            {
                //Debug.Log("Player within viewing angle.");
                //Check to see if there is something between guard and the player to make sure the player can actually be seen, and isn't just behind a wall.
                RaycastHit hit;

                //According to this tutorial, the place the ray cast starts from is the enemy feet.  They don't want to cast against the floor, so they move it up.  I'm
                //not sure if we have that issue.  Also, their unit is 2M tall, and I guess transform.up moves it up 1 meter.  We may need to change this.
                //TODO:  Make sure we transform this correctly so that we have a good idea of what is being ray cast against.
                //if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sightCone.radius))
                if (Physics.Raycast(transform.position, direction.normalized, out hit, sightCone.radius))
                {
                    //Debug.Log("Looking out " + sightCone.radius + " units for player.");
                    //Debug.Log("PLayer is " + Vector3.Distance(player.transform.position, transform.position) + " units away.");
                    //Debug.DrawRay(transform.position + transform.up, direction.normalized, Color.red);
                    //Make sure that we actually hit the player.
                    if (hit.collider.gameObject == player)
                    {
                        //We saw the player, so alert everyone that the player has been seen!
                        //TODO:  Determine if we really want to do this or not.  Maybe we don't.  
                        playerInSight = true;
                        lastPlayerSighting.position = player.transform.position;
                        Debug.Log("Player hit by raycast.  We see them.  Not a wall or something.");
                    }
                }
            }
            //Debug.Log("Check to see if player can be heard.");
            Debug.Log("Current player state is: " + playerStateController.getCurrentPlayerState());
            Debug.Log("Current distance to player is: " + CalculatePathLength(player.transform.position));
            //Debug.Log("Check remaining distance: " + agent.remainingDistance);
            //Now, deal with the player being heard, rather than seen.  I guess players can run and shout.  Check the states.
            //So here's what we'll do.  If the player is running, they can be heard from as far away as their sight cone allows.
            if (playerStateController.getCurrentPlayerState() == PlayerStateController.PlayerStateType.sprinting)
            {
                ////Debug.Log("Current chase radius for sprinting player is: " + sightCone.radius.ToString());
                //When the guard hears the player, it may not be in a straight line.  The player may be around the corner, or in a different room.
                //We'll do this by calculating routes.
                if (CalculatePathLength(player.transform.position) <= sightCone.radius)
                {
                    personalLastSighting = player.transform.position;
                    personalLastSightingTransform = player.transform;
                    Debug.Log("Player heard in sprinting state.");
                    playerIsHeard = true;
                }
            }
            //If the player is in the normal state, they can be heard from half the distance of the sight cone.
            else if (playerStateController.getCurrentPlayerState() == PlayerStateController.PlayerStateType.normal)
            {
                //Debug.Log("Current chase radius for normal player is: " + sightCone.radius * 0.5f);
                //When the guard hears the player, it may not be in a straight line.  The player may be around the corner, or in a different room.
                //We'll do this by calculating routes.
                if (CalculatePathLength(player.transform.position) <= sightCone.radius * 0.5f)
                {
                    personalLastSighting = player.transform.position;
                    personalLastSightingTransform = player.transform;
                    Debug.Log("Player heard in normal state");
                    playerIsHeard = true;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
        }
    }

    /// <summary>
    /// This is goint to calcualte the path distance between the guard and an object.
    /// </summary>
    /// <param name="targetPosition">Vector3 of the object you want to check the path distance to.</param>
    /// <returns></returns>
    float CalculatePathLength(Vector3 targetPosition)
    {
        //Create a new nav mesh path.
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();

        //Make sure the nav is there, and if so get the path from the agent.
        if (agent.enabled)
        {
            //What this does is take a target, and find the waypoints or "corners" between the object and the target.
            //It then stores this in an array of corners.
            agent.CalculatePath(targetPosition, path);
        }

        //The path only contains the waypoints or "corners" of the path.  We need to add the current position as well as the player position
        //which are the endpoints of this path.  That way we can get the actual distance between the two.
        //First, create an array of Vector 3 with two more points than the path.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        //Now, set the first point to the guard position, and the last point to the target position.
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        //This for loop will set all the inbetween values of the way points.  So the first value will be the guard position, the second
        //value will be the first waypoint or "corner", the third value will be the second waypoint or "corner", etc.  And the last value
        //will of course be the target as we set earlier.
        for(int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        //Create a float to track the length.
        float pathLength = 0f;

        //Now, add all the values of the lengths between the waypoints.  Easy peasy.
        for(int i = 0; i < allWayPoints.Length-1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }        //Now, add all the values of the lengths between the waypoints.  Easy peasy.
        //for (int i = 0; i < allWayPoints.Length; i++)
        //{
        //    Debug.Log("Waypoint #" + i + " is: " + allWayPoints[i]);
        //}

        //Debug.Log("SO, there are " + allWayPoints.Length + " points we are calculating from " + allWayPoints[0] + " to " + allWayPoints[allWayPoints.Length - 1]);
        

        //Send the total distance we calculated back.
        return pathLength;
    }


}
