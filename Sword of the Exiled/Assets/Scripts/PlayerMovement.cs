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
            //Input
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");
            sprinting = Input.GetButton("Sprint");

            if (sneaking == false && Input.GetButtonDown("Sneak") == true)
            {
                sneaking = true;
            }
            else if (sneaking == true && Input.GetButtonDown("Sneak") == true)
            {
                sneaking = false;
            }
            else if (sneaking == true && Input.GetButtonDown("Sprint") == true)
            {
                sneaking = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //Movement
        if (sprinting == true)
        {
            moveSpeed = sprintSpeed;
        }
        else if (sneaking == true)
        {
            moveSpeed = sneakSpeed;
        }
        else
        {
            moveSpeed = 5f;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
