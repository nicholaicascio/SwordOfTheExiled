using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 3f;
    public float defaultSpeed = 5f;
    public float sprintSpeed = 8f;
    public float sneakSpeed = 2.5f;
    public Rigidbody rb;
    Vector3 movement;

    public bool sprinting;
    public bool sneaking;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        sprinting = Input.GetButton("Sprint");          //So, my question is did we use "Sprint" because people can change which the sprint button is?  I wasn't aware of all the sprint, sneak, etc buttons.

        //So, this is going to check and determine how we should move .
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
            moveSpeed = defaultSpeed;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
