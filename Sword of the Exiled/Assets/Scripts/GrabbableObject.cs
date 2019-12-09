using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// This class will allow an object to be grabbable over the network.
/// </summary>
public class GrabbableObject : MonoBehaviourPun, IPunOwnershipCallbacks
{
    //Determines if a prism should be moveable or not.  Set to true on in game environmental prisms.
    public bool stationary = false;
    public Transform player;

    //Vector3 that has how far away the prism will follow.  There was an issue with rotations happening,
    //so this is to prevent that issue from happening.
    private Vector3 _offset;

    //So, this has player deal has something to do with moving and being carried.  We're going
    //to set this to work with a trigger.  When this has a player, it can be moved.
    private bool hasPlayer = false;

    //This will track if the prism is being carried.  It can only move while being carried.
    private bool beingCarried = false;
    
    public AudioClip[] soundToPlay;

    /// <summary>
    /// Setup the call back so that this is called properly.  Registers the IPunOwnership callback internally.
    /// If you didn't want to execute when the objec is disabled just like with events, this would be OnEnable, and
    /// OnDisable.
    /// </summary>
    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    /// <summary>
    /// Setup the call back so that this is called properly.  Registers the IPunOwnership callback internally.
    /// If you didn't want to execute when the objec is disabled just like with events, this would be OnEnable, and
    /// OnDisable.
    /// </summary>
    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    /// <summary>
    /// So, I realized that I am constantly setting the isKinematic to true.
    /// I should probably just set that in the prefabs.
    /// TODO: set prefab for prisms to isKinematic = true;
    /// </summary>
    void Update()
    {
        //Make sure we can move this prism.
        if (!stationary && hasPlayer)
        {
            //First, we'll check to see if the "E" key is pressed.
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("E Key pressed.");

                //First, check to see if the object is being carried.
                if (beingCarried)
                {
                    //Debug.Log("Turn off being carried.");

                    //Lastly, turn being carried off.
                    beingCarried = false;
                }
                //Object is not being carried.  So, we will request ownership of it and make it moveable.
                else
                {
                    //Debug.Log("Turn on being carried.");
                    //Get ownership if this isn't already mine.
                    if (!photonView.IsMine)
                    {
                        //Debug.Log("Making this mine.");
                        //This will allow whomever pressed "E" to own this prism.
                        //Quick note, for this to work, the prism has to be set to "Takeover" in the PhotonView for Owner.
                        base.photonView.RequestOwnership();
                    }

                    //Calculate and store the offset value by getting the distance between the player's position and camera's position.
                    _offset = transform.position - player.transform.position;

                    //Lastly, we will show that this is being carried.
                    beingCarried = true;
                }
            }

            //Now that we've done some checks, see if this is being carried.
            if (beingCarried)
            {
                //Move this prism along with the player by the offset.
                transform.position = player.transform.position + _offset;
            }
        }
    }

    /// <summary>
    /// No idea what this is for.  It does nothing and is called when a mouse down
    /// is clicked.  I think it was supposed to be for putting the prism down.  I've
    /// removed that old code though.
    /// </summary>
    void RandomAudio()
    {

    }

    /// <summary>
    /// This is going to check for colisions with players to see if a player can control the object.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        //Since we are only making moves with this grabbable,
        //we need to check if this is stationary before we do anyting.
        if (!stationary)
        {
            //Check for other collision to be the player.
            if (other.tag == "Player")
            {
                //We have hit a player.  Set the player variable to that transform.
                player = other.GetComponent<Transform>();

                //Now, set hasPlayer to true.
                hasPlayer = true;
            }
        }
    }

    /// <summary>
    /// This will run when the player is no longer near the prism.  Because of the floor issues, there is
    /// also a problem with exiting the trigger before we actually want to.  So, we'll only do this
    /// if the player has intentionally set the prism down.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        //Since we are only making moves with this grabbable,
        //we need to check if this is stationary before we do anyting.
        if (!stationary)
        {
            //Make sure the other collider is the player.
            if (other.tag == "Player" && !beingCarried)
            {
                //Set the hasPlayer boolean to false.
                hasPlayer = false;
            }
        }
    }

    /// <summary>
    /// Get ownership of an object that is marked as request in the PhotonView.
    /// This can be called on anything that uses the IPunOwnershipCallbacks
    /// It's important to make sure the target view matches the view on this object before
    /// you do anything with it.
    /// </summary>
    /// <param name="targetView"></param>
    /// <param name="requestingPlayer"></param>
    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        //Make sure the target view is the same.  Honestly, I don't know what that means.  PhotonView is a class that you put on photon networked
        //objects.  Why would you not be looking the correct PhotonView?
        if (targetView != base.photonView)
        {
            return;
        }

        //TODO:
        //Add checks here should you want them.

        //This actually does the ownership transfer.
        base.photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        //Make sure the target view is the same.  Honestly, I don't know what that means.  PhotonView is a class that you put on photon networked
        //objects.  Why would you not be looking the correct PhotonView?
        if (targetView != base.photonView)
        {
            return;
        }
    }
}
