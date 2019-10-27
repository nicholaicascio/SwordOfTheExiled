using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapperChild : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Guard")
        {
            GuardTrapper dad = gameObject.GetComponentInParent<GuardTrapper>();
            dad.setZone(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
