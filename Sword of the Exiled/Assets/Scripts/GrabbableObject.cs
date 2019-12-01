using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public bool stationary = false;
    public Transform player;
    bool hasPlayer = false;
    bool beingCarried = false;
    public AudioClip[] soundToPlay;
    //private AudioSource audioSource;
    private bool touched = false;

    private void Start()
    {
        //Assign player to transform of the player.
        setPlayer();
    }

    /// <summary>
    /// Assign the player to the transform of the player.
    /// TODO:
    /// Replace this with the Photon ownership code.
    /// </summary>
    private void setPlayer()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length > 0)
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        }
    }

    void Update()
    {
        //Check to see if the player is set.
        if (player == null)
        {
            setPlayer();
        }
        else
        {
            if (!stationary)
            {
                float dist = Vector3.Distance(gameObject.transform.position, player.position);
                if (dist <= 2.5f)
                {
                    hasPlayer = true;
                }
                else
                {
                    hasPlayer = false;
                }
                if (hasPlayer && Input.GetKeyDown(KeyCode.E))
                {
                    GetComponent<Rigidbody>().isKinematic = true;
                    transform.parent = player;
                    beingCarried = true;
                }
                if (beingCarried)
                {
                    if (touched)
                    {
                        GetComponent<Rigidbody>().isKinematic = false;
                        transform.parent = null;
                        beingCarried = false;
                        touched = false;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<Rigidbody>().isKinematic = false;
                        transform.parent = null;
                        beingCarried = false;
                        //GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                        RandomAudio();
                    }
                }
            }
        }
    }

    void RandomAudio()
    {

    }

    /// <summary>
    /// This is a quick and dirty coroutine to make some stuff wait.  Every now and then, you just need to wait a moment.
    /// </summary>
    /// <param name="x">Int - Number of milliseconds to wait.</param>
    private IEnumerator SimpleWait(float x)
    {
        yield return new WaitForSeconds(x);
    }
}
