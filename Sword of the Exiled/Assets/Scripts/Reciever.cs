//using Photon.Pun;
//using Photon.Realtime;
using UnityEngine;

/// <summary>
/// This was using network stuff, but now it's not.  I set it up so that the
/// recievers can be individual to a person playing, but the moving prisms are
/// what you would actually have networked.  This also drastically reduces the
/// amount of network calls being made.  Yay!
/// </summary>
//public class Reciever : MonoBehaviourPunCallbacks
public class Reciever : MonoBehaviour
{
    //So, this reciever id is how we are going to separate all the recievers from each other.  Each room has a number, and there are recievers in each room.  So the first reciever in rooom
    //1 should be Reciever_101.  The 10th reciever in room 8 should be Reciever_810.  This should be that number for tracking purposes.  
    [SerializeField]
    private int _RecieverId = 0;

    //This is the color that this reciever should be looking for.
    [SerializeField]
    private string _myColor;
    //This will hold the last color that was passed into the setColor function.  It's just used to make sure we only check data when
    //things change, not every possible updated moment.
    private string _previousColor;

    public bool secondRecieverActivated = false;
    public bool secondRecieverRequired = false;
    public bool isActivated = false;
    public bool MultipleDoors = false;
    public bool ClosesDoor = false;
    bool doorIsClosed = false;

    //This is the recieved color.  It will be set when the setColor function is called.  This is what will be checked against the set value for myColor.
    private string recievedColor;

    public GameObject[] targetObjects;
    public GameObject[] guardTrapperAndDoor;

    /// <summary>
    /// This is a networked function that will run through the doors and toggle them.
    /// This used to be a network RPC.  But I don't need to do that anymore because
    /// I made the moving prisms proper networked items.
    /// </summary>
    /// <param name="Id">Int - Number of the reciever that should be paying attention to this.</param>
    //[PunRPC]
    public void RPC_DoorToggle(int Id)
    {
        if (_RecieverId == Id)
        {
            //Debug.Log("RPC_DoorToggle function run.");
            foreach (GameObject door in targetObjects)
            {
                Animator anim = door.GetComponent<Animator>();
                //anim.SetBool("isOpen", !anim.GetBool("isOpen"));
                if (anim.GetBool("isOpen") == true)
                {
                    anim.SetBool("isOpen", false);
                    //Debug.Log("close the open door");
                }
                else
                {
                    anim.SetBool("isOpen", true);
                    //Debug.Log("Open the closed door");
                }
            }
        }
    }

    /// <summary>
    /// So, this also opens and closes doors.
    /// As I've mentioned in a couple other things, this used to be RPC based, but
    /// now it's not because the doors open as a result of the properly created 
    /// multiplayer prisms.
    /// </summary>
    /// <param name="Id">Int - Number of the reciever that should be paying attention to this.</param>
    //[PunRPC]
    public void RPC_DoorOpen(int Id)
    {
        if (_RecieverId == Id)
        {
        //    Debug.Log("RPC_DoorOpen function run.");

        //    Debug.Log("Checking second required: " + secondRecieverRequired);

            //Check to see if this has a second reciever required.
            if (secondRecieverRequired)
            {
                //First, we'll look to see if there are any reciever objects.  If so, we want to check
                //to see if they have been properly activated.
                foreach (GameObject targetObject in targetObjects)
                {
                    //We'll check to see if this object is a receiver.
                    if(targetObject.tag == "Reciever")
                    {
                        //This is a reciever.  So, get it.  
                        Reciever rec = targetObject.gameObject.GetComponent<Reciever>();

                        //Set the secodn reciever activated to whatever the reciever isActivated is.
                        secondRecieverActivated = rec.isActivated;
                    }
                }
            }

            //Next, we will loop through doors and see if we can open them.  Yay!
            foreach (GameObject targetObject in targetObjects)
            {
                //First, well check for doors in which no second reciever is needed.
                if (targetObject.gameObject.name == "BigDoorController" && !secondRecieverRequired)
                {
                    //Open the door.
                    //Debug.Log("open big door");
                    Animator anim = targetObject.GetComponent<Animator>();
                    anim.SetBool("isOpen", true);
                }
                //Next, we'll check to see that the second reciever is required AND active!
                else if (targetObject.gameObject.name == "BigDoorController" && secondRecieverRequired && secondRecieverActivated)
                {
                    Animator anim = targetObject.GetComponent<Animator>();
                    anim.SetBool("isOpen", true);
                }
            }
        }
    }

    /// <summary>
    /// This code runs when a room has multiple doors.
    /// </summary>
    public void ActivateTwo()
    {
        //Debug.Log("Activate two called.");
        //Debug.Log("_myColor: " + _myColor + "; recievedColor: " + recievedColor);
        //Makre sure the color recieved is the color we are looking for.
        if (_myColor == recievedColor || recievedColor is null)
        {
            //Now, check to see if we can open the door.
            //base.photonView.RPC("RPC_DoorToggle", RpcTarget.All, _RecieverId);
            RPC_DoorToggle(_RecieverId);
        }
    }

    /// <summary>
    /// At this point in the game, there is only one door that closes.  It is in the fifth room.  So this
    /// really shouldn't be done like this, but whatever.  I really just need this to work.
    /// This used to be a pun RPC thing.  But that won't be necessary at this point in time
    /// as I set the moveable objects to network objects.  Yay!
    /// </summary>
    /// <param name="Id">Int - Number of the reciever that should be paying attention to this.</param>
    //[PunRPC]
    public void RPC_CloseDoor(int Id)
    {
        //Debug.Log("Running CloseDoor");
        if (!doorIsClosed)
        {
            doorIsClosed = true;
            for (int i = 0; i < guardTrapperAndDoor.Length; i++)
            {
                if (guardTrapperAndDoor[i].name == "BigDoorController")
                {
                    //Debug.Log("Closed a door");
                    Animator anim = guardTrapperAndDoor[i].GetComponent<Animator>();
                    anim.SetBool("isOpen", false);
                }
                else if (guardTrapperAndDoor[i].tag == "NavPoint")
                {
                    //Debug.Log("Disabled a Nav Point");
                    //guardsAndNavPoints[i].SetActive(false);
                }
                else if (guardTrapperAndDoor[i].name == "GuardTrapper")
                {
                    GuardTrapper trap = guardTrapperAndDoor[i].gameObject.GetComponent<GuardTrapper>();
                    trap.updateGuard();
                    Debug.Log("Updated a Guard's navpoints");
                    //StateController con = guardsAndNavPoints[i].gameObject.GetComponent<StateController>();
                    //con.updateNavs();
                }
            }
        }
        else
        {
            doorIsClosed = false;
            foreach (GameObject obj in guardTrapperAndDoor)
            {
                if (obj.name == "BigDoorController")
                {
                    Animator anim = obj.GetComponent<Animator>();
                    anim.SetBool("isOpen", true);
                }
                else if(obj.name == "GuardTrapper")
                {
                    GuardTrapper gt = obj.GetComponent<GuardTrapper>();
                    gt.DoorOpened();
                }
            }
        }
    }

    /// <summary>
    /// This will take a color and determine what should be done for the reciever.
    /// </summary>
    /// <param name="col">Color being passed into the receiver.</param>
    public void setColor(string col)
    {
        //First, check to see if the color being passed in is the same as the previous color.  If so,
        //there has been no change and no action needs to be taken.
        if (_previousColor != col)
        {
            //Debug.Log("previous color: " + _previousColor + "; new color: " + col);
            //Set the previous color to the color passed in.
            _previousColor = col;
            recievedColor = col;

            //At this point in the game, there is only one door with the "ClosesDoor" flag.  It is in room 5.
            //What we will actually do with that door is close it to trap the guard.  So, we'll check for that.
            //It just so happens that this works because there is only one laser that can hit this reciever.  This
            //code won't work for multiple laser colors because nothing checks for the recieved color to match
            //the myColor.  The CloseDoor stuff is a bit messy, but it works for now.
            if (ClosesDoor)
            {
                //base.photonView.RPC("RPC_CloseDoor", RpcTarget.All, _RecieverId);
                RPC_CloseDoor(_RecieverId);
            }
            //Here, we are going to check for multiple doors.  Super gross.
            else if (MultipleDoors)
            {
                ActivateTwo();
            }
            else if(_myColor == recievedColor)
            {
                //Set that this is activated to true.  Needed for switches requireing more
                //than one switch to be activated.
                isActivated = true;

                //base.photonView.RPC("RPC_DoorOpen", RpcTarget.All, _RecieverId);
                RPC_DoorOpen(_RecieverId);
            }
            //Debug.Log("setColor");
        }
    }
}
