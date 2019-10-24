using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    public GameObject targetObject;
    public string recievedColor, myColor;
    public bool secondRecieverActivated = false;
    public bool secondRecieverRequired = false;
    public bool TwoDoors = false;
    public GameObject[] targetObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(myColor == null)
        {
            foreach (GameObject door in targetObjects)
            {
                Animator anim = door.GetComponent<Animator>();
                if (anim.GetBool("isOpen") == true)
                {
                    anim.SetBool("isOpen", false);
                    Debug.Log("close the open door");
                }
                else
                {
                    anim.SetBool("isOpen", true);
                    Debug.Log("Open the closed door");
                }
            }
        }

        if (myColor == recievedColor && TwoDoors == false)
        {
            if (targetObject.gameObject.name == "BigDoorController" && secondRecieverRequired == false)
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
            else if (targetObject.gameObject.name == "BigDoorController" && secondRecieverRequired == true && secondRecieverActivated == true)
            {
                Animator anim = targetObject.GetComponent<Animator>();
                anim.SetBool("isOpen", true);
            }
        }
    }

    public void ActivateTwo()
    {
        if (myColor == recievedColor)
        {
            foreach (GameObject door in targetObjects)
            {
                Animator anim = door.GetComponent<Animator>();
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

    public void setColor(string col)
    {
        if (recievedColor != col && TwoDoors == true)
        {
            recievedColor = col;
            ActivateTwo();
        }
        else if(recievedColor != col && TwoDoors == false)
        {
            recievedColor = col;
        }
        //Debug.Log("setColor");
    }

}
