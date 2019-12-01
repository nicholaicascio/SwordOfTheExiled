using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Reciever : MonoBehaviourPunCallbacks
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
    public bool MultipleDoors = false;
    public bool ClosesDoor = false;
    bool doorIsClosed = false;

    //This is the recieved color.  It will be set when the setColor function is called.  This is what will be checked against the set value for myColor.
    private string recievedColor;

    public GameObject[] targetObjects;
    public GameObject[] guardTrapperAndDoor;

    // Update is called once per frame
    void Update()
    {
        //if (myColor == null)
        //{
        //    //So, this is going to toggle the door for all players involved.
        //    base.photonView.RPC("RPC_DoorToggle", RpcTarget.All, RecieverId);
        //}
        ////if(RecieverId == 101)
        ////{
        ////    Debug.Log("myColor for Reciever101 is: " + myColor + ";  MulitpleDoors for Reciever101 is: " + MultipleDoors);
        ////}
        //if (myColor == recievedColor && !MultipleDoors)
        //{
        //    //So, this is going to just open doors I think.
        //    base.photonView.RPC("RPC_DoorOpen", RpcTarget.All, RecieverId);
        //}
    }

    /// <summary>
    /// This is a networked function that will run through the doors and toggle them.
    /// </summary>
    /// <param name="Id">Int - Number of the reciever that should be paying attention to this.</param>
    [PunRPC]
    public void RPC_DoorToggle(int Id)
    {
        if (_RecieverId == Id)
        {
            Debug.Log("RPC_DoorToggle function run.");
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
    /// So, this also opens and closes doors.  However, it 
    /// </summary>
    /// <param name="Id">Int - Number of the reciever that should be paying attention to this.</param>
    [PunRPC]
    public void RPC_DoorOpen(int Id)
    {
        if (_RecieverId == Id)
        {
            Debug.Log("RPC_DoorOpen function run.");
            //First, we'll check to see if a second reciever is required.  If so, we need to set that this has been
            //opened so that when that is checked everything will be ready to go.
            if (secondRecieverRequired)
            {
                secondRecieverActivated = true;
            }
            boolCheck();
            foreach (GameObject targetObject in targetObjects)
            {
                if (targetObject.gameObject.name == "BigDoorController" && !secondRecieverRequired)
                {
                    //Debug.Log("open big door");
                    Animator anim = targetObject.GetComponent<Animator>();
                    anim.SetBool("isOpen", true);
                }
                else if (targetObject.gameObject.name == "Reciever")
                {
                    Reciever rec = targetObject.gameObject.GetComponent<Reciever>();
                    rec.secondRecieverActivated = true;
                }
                else if (targetObject.gameObject.name == "BigDoorController" && secondRecieverRequired && secondRecieverActivated)
                {
                    Animator anim = targetObject.GetComponent<Animator>();
                    anim.SetBool("isOpen", true);
                }
            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ActivateTwo()
    {
        if (_myColor == recievedColor)
        {
            base.photonView.RPC("RPC_DoorToggle", RpcTarget.All, _RecieverId);
        }
    }

    /// <summary>
    /// At this point in the game, there is only one door that closes.  It is in the fifth room.  So this
    /// really shouldn't be done like this, but whatever.  I really just need this to work.
    /// </summary>
    /// <param name="Id">Int - Number of the reciever that should be paying attention to this.</param>
    [PunRPC]
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
                base.photonView.RPC("RPC_CloseDoor", RpcTarget.All, _RecieverId);
            }
            //Here, we are going to check for multiple doors.  Super gross.
            else if (MultipleDoors)
            {
                ActivateTwo();
            }
            else if(_myColor == recievedColor)
            {
                base.photonView.RPC("RPC_DoorOpen", RpcTarget.All, _RecieverId);
            }
            //Debug.Log("setColor");
        }
    }

    public void boolCheck()
    {
        Debug.Log("For Reciever_" + _RecieverId + "secondRecieverActivated is: " + secondRecieverActivated + " and secondRecieverRequired is: " + secondRecieverRequired);
    }

}
