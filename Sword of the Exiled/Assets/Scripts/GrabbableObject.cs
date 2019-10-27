using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public Transform player;
    //public Transform playerCam;
    public float throwForce = 100;
    bool hasPlayer = false;
    bool beingCarried = false;
    public AudioClip[] soundToPlay;
    //private AudioSource audioSource;
    public int dmg;
    private bool touched = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    void Update()
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

    void RandomAudio()
    {

    }
}
