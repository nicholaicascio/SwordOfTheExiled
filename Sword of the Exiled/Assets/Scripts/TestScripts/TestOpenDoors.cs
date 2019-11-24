using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public class TestOpenDoors : MonoBehaviour
{

}
//public class TestOpenDoors : MonoBehaviourPunCallbacks
//{
//    //public GameObject targetObject;
//    public string recievedColor, myColor;
//    public bool secondRecieverActivated = false;
//    public bool secondRecieverRequired = false;
//    public bool MultipleDoors = false;
//    public bool ClosesDoor = false;
//    bool doorIsClosed = false;
//    //public bool Toggled = false;
//    //public bool Toggleable = false;
//    public GameObject[] targetObjects;
//    public GameObject[] guardTrapperAndDoor;
//    // Start is called before the first frame update
//    void Start()
//    {

//    }
//    // Update is called once per frame
//    void Update()
//    {
//        if (myColor == null)
//        {
//            foreach (GameObject door in targetObjects)
//            {
//                Animator anim = door.GetComponent<Animator>();
//                if (anim.GetBool("isOpen") == true)
//                {
//                    anim.SetBool("isOpen", false);
//                    Debug.Log("close the open door");
//                }
//                else
//                {
//                    anim.SetBool("isOpen", true);
//                    Debug.Log("Open the closed door");
//                }
//            }
//        }

//        if (myColor == recievedColor && !MultipleDoors)
//        {
//            foreach (GameObject targetObject in targetObjects)
//            {
//                if (targetObject.gameObject.name == "BigDoorController" && !secondRecieverRequired)
//                {
//                    //Debug.Log("open big door");
//                    Animator anim = targetObject.GetComponent<Animator>();
//                    anim.SetBool("isOpen", true);
//                }
//                else if (targetObject.gameObject.name == "Reciever")
//                {
//                    Reciever rec = targetObject.gameObject.GetComponent<Reciever>();
//                    rec.secondRecieverActivated = true;
//                }
//                else if (targetObject.gameObject.name == "BigDoorController" && secondRecieverRequired && secondRecieverActivated)
//                {
//                    Animator anim = targetObject.GetComponent<Animator>();
//                    anim.SetBool("isOpen", true);
//                }
//            }

//        }
//    }

//    public void ActivateTwo()
//    {
//        if (myColor == recievedColor)
//        {
//            foreach (GameObject door in targetObjects)
//            {
//                Animator anim = door.GetComponent<Animator>();
//                if (anim.GetBool("isOpen"))
//                {
//                    anim.SetBool("isOpen", false);
//                    Debug.Log("close the open door");
//                }
//                else
//                {
//                    anim.SetBool("isOpen", true);
//                    Debug.Log("Open the closed door");
//                }
//            }
//        }
//    }

//    public void CloseDoor()
//    {

//        if (!doorIsClosed)
//        {
//            doorIsClosed = true;
//            for (int i = 0; i < guardTrapperAndDoor.Length; i++)
//            {
//            }
//public class Reciever : MonoBehaviour
//{
   
