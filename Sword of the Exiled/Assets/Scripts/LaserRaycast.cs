using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRaycast : MonoBehaviour
{
    public string color;
    public Prism prism;
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
                prism.setColor(this.color);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                Debug.Log(hit.collider.gameObject.name);
            }
            else if (prism != null)
            {
                prism.setColor(null);
                prism = null;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
            }
        }


    }
}
