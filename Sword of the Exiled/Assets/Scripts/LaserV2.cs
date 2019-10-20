using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserV2 : MonoBehaviour
{
    public GameObject hitted;
    public string color;
    public Prism prism;
    public Reciever reciever;
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = this.GetComponent<LineRenderer>();
        //daddy = GetComponentInParent<Prism>();
    }

    private void OnDisable()
    {
        if(prism != null)
        {
            prism.setColor(null);
            prism = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, hit.point);
            if (hit.collider.gameObject != hitted && prism != null)
            {
                //Debug.Log("hitted");
                prism.setColor(null);
                prism = null;
                hitted = hit.collider.gameObject;
            }
            if (hit.collider.tag == "Prism")
            {

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
                    else if (prismColor == "green")
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
            if (hit.collider.tag != "Prism" && prism != null)
            {
                prism.setColor(null);
                prism = null;
            }
            if (hit.collider.tag == "Reciever")
            {
                reciever = hit.collider.gameObject.GetComponent<Reciever>();
                reciever.setColor(this.color);
                //Debug.Log("reciever hit");
            }
            if (hit.collider.tag != "Reciever" && reciever != null)
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