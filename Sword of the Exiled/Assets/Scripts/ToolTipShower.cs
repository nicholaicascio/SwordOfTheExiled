using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipShower : MonoBehaviour
{
    public Canvas tooltip;
    bool playerIsInside = false;

    void Update()
    {
        if (!playerIsInside)
        {
            tooltip.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        tooltip.gameObject.SetActive(true);
        playerIsInside = true;
    }
    private void OnTriggerExit(Collider other)
    {
        tooltip.gameObject.SetActive(false);
        playerIsInside = false;
    }
}
