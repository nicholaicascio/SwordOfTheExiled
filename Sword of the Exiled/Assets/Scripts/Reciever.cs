using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    public GameObject targetObject;
    public string recievedColor, myColor;
    public bool secondRecieverActivated = false;
    public bool secondRecieverRequired = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myColor == recievedColor)
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

    public void setColor(string col)
    {
        //Debug.Log("setColor");
        recievedColor = col;
    }

}
