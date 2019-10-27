using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipShower : MonoBehaviour
{
    public Canvas tooltip;
    public bool stationary = false;
    bool playerIsInside = false;

    void Start()
    {
        if (!playerIsInside)
        {
            //Debug.Log("turn off the tooltip");
            tooltip.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !stationary)
        {
            tooltip.gameObject.SetActive(true);
            playerIsInside = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && !stationary)
        {
            tooltip.gameObject.SetActive(false);
            playerIsInside = false;
        }
        
    }
}
