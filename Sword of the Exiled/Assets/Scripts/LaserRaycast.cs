using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRaycast : MonoBehaviour
{
    public string color;
    public Prism prism;
    public Reciever reciever;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Prism")
            {
                //cube = hit.collider.gameObject;
                
                prism = hit.collider.gameObject.GetComponent<Prism>();
                string prismColor;
                prismColor = prism.getColor();

                if (this.color == "blue")
                {
                    if (prismColor == "red")
                    {
                        prism.setColor("magenta");
                    }
                    else if (prismColor == "blue")
                    {
                        prism.setColor("blue");
                    }
                    else if(prismColor == "green")
                    {
                        prism.setColor("cyan");
                    }
                    else if (prismColor == "magenta" || prismColor == "cyan" || prismColor == "yellow")
                    {

                    }
                    else
                    {
                        prism.setColor(this.color);
                    }
                }
                if (this.color == "red")
                {
                    if (prismColor == "red")
                    {
                        prism.setColor("red");
                    }
                    else if (prismColor == "blue")
                    {
                        prism.setColor("magenta");
                    }
                    else if (prismColor == "green")
                    {
                        prism.setColor("yellow");
                    }
                    else if (prismColor == "magenta" || prismColor == "cyan" || prismColor == "yellow")
                    {

                    }
                    else
                    {
                        prism.setColor(this.color);
                    }
                }
                if (this.color == "green")
                {
                    if (prismColor == "red")
                    {
                        prism.setColor("yellow");
                    }
                    else if (prismColor == "blue")
                    {
                        prism.setColor("cyan");
                    }
                    else if (prismColor == "green")
                    {
                        prism.setColor("green");
                    }
                    else if (prismColor == "magenta" || prismColor == "cyan" || prismColor == "yellow")
                    {

                    }
                    else
                    {
                        prism.setColor(this.color);
                    }
                }

                //prism.setColor(this.color);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                //Debug.Log(hit.collider.gameObject.name);
            }
            else if (prism != null)
            {
                prism.setColor(null);
                prism = null;
            }
            else if (hit.collider.tag == "Reciever")
            {
                
                reciever = hit.collider.gameObject.GetComponent<Reciever>();
                reciever.setColor(this.color);
                //Debug.Log("reciever hit");
            }
            else if (reciever != null)
            {
                reciever.setColor(null);
                reciever = null;
                Debug.Log("reciever null");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
            }
        }


    }
}
