using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    public float moveSpeed = 3f;
    public float sprintSpeed = 8f;
    public float sneakSpeed = 2.5f;
    public Rigidbody rb;
    Vector3 movement;

    public bool sprinting;
    public bool sneaking;
    
    // Start is called before the first frame update
    void Start()
    {
        //Check to see if the player is mine.  If not, find the camera and deactivate it.
        if (!base.photonView.IsMine)
        {
            Camera cam = GetComponentInChildren<Camera>();
            cam.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (base.photonView.IsMine)
        {
            Debug.Log("Player seen!");
            //Input
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");
            sprinting = Input.GetButton("Sprint");

            if (sneaking == false && Input.GetButtonDown("Sneak") == true)
            {
                //Debug.Log("Sneaking");
                sneaking = true;
            }
            else if (sneaking == true && Input.GetButtonDown("Sneak") == true)
            {
                //Debug.Log("Not Sneaking");
                sneaking = false;
            }
            else if (sneaking == true && Input.GetButtonDown("Sprint") == true)
            {
                //Debug.Log("Not Sneaking");
                sneaking = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //Movement
        if (sprinting == true)
        {
            //Debug.Log("Spring speed.");
            moveSpeed = sprintSpeed;
        }
        else if (sneaking == true)
        {
            //Debug.Log("Sneak speed");
            moveSpeed = sneakSpeed;
        }
        else
        {
            //Debug.Log("Walk speed?");
            moveSpeed = 5f;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
