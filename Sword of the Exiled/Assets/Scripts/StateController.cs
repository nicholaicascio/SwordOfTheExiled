using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateController : MonoBehaviour
{
    //Variables
    public State currentState;
    public GameObject[] navPoints;
    public GameObject targetToChase;
    public int navPointNum;
    public Transform destination;
    //public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl_Modified ai;
    public Renderer[] childrenRend;
    public GameObject[] targets;
    public float detectionRange = 5.0f;

    /// <summary>
    /// Get the transform of the next navpoint to head to.
    /// </summary>
    /// <returns></returns>
    public Transform GetNextNavPoint()
    {
        navPointNum = (navPointNum + 1) % navPoints.Length;
        return navPoints[navPointNum].transform;
    }

    /// <summary>
    /// Quick function that will change the color of everything to a passed in color.
    /// </summary>
    /// <param name="color">Color</param>
    public void ChangeColor(Color color)
    {
        foreach(Renderer r in childrenRend)
        {
            foreach(Material m in r.materials)
            {
                m.color = color;
            }
        }
    }

    //Check to see if anything with a specific tag is within range.
    public bool CheckIfInRange(string tag)
    {
        //Get a list of all objects with the tag.
        targets = GameObject.FindGameObjectsWithTag(tag);

        //See if our list is empty or not.
        if(targets != null)
        {
            //Run through the list of potential targets.
            foreach(GameObject t in targets)
            {
                //Check to see if the potential target is within detection range.
                if(Vector3.Distance(t.transform.position, transform.position) < detectionRange)
                {
                    //Start chasing target I guess?
                    targetToChase = t;
                    return true;
                }
            }
        }

        //No targets within detection range.
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Find Nav Points in the environment.
        navPoints = GameObject.FindGameObjectsWithTag("NavPoint");
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl_Modified>();

        //Get render components for color change.
        childrenRend = GetComponentsInChildren<Renderer>();

        //Set the initial state
        SetState(new PatrolState(this));

    }

    // Update is called once per frame
    void Update()
    {
        currentState.CheckTransitions();
        currentState.Act();
    }

    public void SetState(State state)
    {
        if(currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "AI agent in state " + state.GetType().Name;

        if(currentState != null)
        {
            currentState.OnStateEnter();
        }
    }
}
