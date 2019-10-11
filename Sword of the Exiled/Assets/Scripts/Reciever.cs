using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    public GameObject targetObject;
    public string recievedColor, myColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myColor == recievedColor)
        {
            if (targetObject.gameObject.name == "BigDoorController")
            {
                Debug.Log("open big door");
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
