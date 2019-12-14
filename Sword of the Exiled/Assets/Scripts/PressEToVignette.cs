using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEToVignette : MonoBehaviour
{
    //Inspector variables.
    public Canvas tooltip;          //This is a canvas set in the inspector.  It will basically just tell the player to press "E".
    public Camera lookAtThis;       //This is a camera set up in the inspector.  It will show the user the vignette.

    //Public variables
    public Camera MainCamera;       //This is the player camera.  We will get it when the player enters the trigger zone.
    Rigidbody rig;                  //This is the rigid body of the player.  It is set in the trigger.

    //Private variables
    public bool playerIsInside = false;    //We'll use this to ensure that the player is inside the space where we would show the poster.
    public bool playerIsLooking = false;   //We'll use this to determine if the player is looking at the poster or not.

    //Testing variables
    public GameObject go;

    /// <summary>
    /// This is going to set things up when the player enters the trigger area so that they can view the poster.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //First, make sure the other thing we are hitting is the player.
        if (other.tag == "Player")
        {
            go = other.gameObject;
            //Now, get the camera component of the player.
            MainCamera = other.gameObject.GetComponentInChildren<PlayerStateController>().mainCamera;

            //Get the rigid body of the player.
            rig = other.gameObject.GetComponent<Rigidbody>();

            //Activate the tool tip so that the player knows they can hit the "E" key for something to happen.
            tooltip.gameObject.SetActive(true);

            //Set this bool to true so we know we can show the vignette.
            playerIsInside = true;
        }
    }

    /// <summary>
    /// This will run when the player leaves the trigger area.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        //First, make sure the other thing being hit is the player.
        if (other.tag == "Player")
        {
            //Remove camera reference.
            MainCamera = null;

            //Stop showing tool tip.
            tooltip.gameObject.SetActive(false);

            //Note that no player is inside the area to show the vignette
            playerIsInside = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if the player is trying to see the vignette.
        if (!playerIsLooking && playerIsInside && Input.GetKeyDown(KeyCode.E))
        {
            //First, we will activate our camera.
            lookAtThis.gameObject.SetActive(true);

            //Next, deactivate the player camera.
            MainCamera.gameObject.SetActive(false);

            //Set the bool so that we know the player is looking at the vignette.
            playerIsLooking = true;

            //I guess what this is doing is freezing the player to keep them from moving when they view the vignette.
            rig.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            
        }
        //Check to see if the player is trying to stop looking at the vignette.
        else if (playerIsLooking && Input.GetKeyDown(KeyCode.E))
        {
            //Turn our camera off.
            lookAtThis.gameObject.SetActive(false);

            //Turn the player camera back on.
            MainCamera.gameObject.SetActive(true);

            //Set is player looking to false.
            playerIsLooking = false;

            //Reset the constraints to allow the player to move again.
            rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rig.constraints = 0;
        }
    }
}
