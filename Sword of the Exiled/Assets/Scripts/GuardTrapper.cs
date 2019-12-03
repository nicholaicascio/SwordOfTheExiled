using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is going to handle the guard and set his scripts.
/// </summary>
public class GuardTrapper : MonoBehaviour
{
    public GameObject currentZone;
    public GameObject[] guards, leftNavs, rightNavs, doorOpenNavs;
    // Start is called before the first frame update

    public void updateGuard()
    {
        if(currentZone.name == "Left")
        {
            foreach(GameObject guard in guards)
            {
                StateController con = guard.gameObject.GetComponent<StateController>();
                foreach (GameObject nav in leftNavs)
                {
                    nav.SetActive(true);
                }
                foreach (GameObject nav in rightNavs)
                {
                    nav.SetActive(false);
                }
                con.navPoints = leftNavs;
                
            }
        }
        else if (currentZone.name == "Right")
        {
            foreach (GameObject guard in guards)
            {
                StateController con = guard.gameObject.GetComponent<StateController>();
                foreach (GameObject nav in rightNavs)
                {
                    nav.SetActive(true);
                }
                foreach (GameObject nav in leftNavs)
                {
                    nav.SetActive(false);
                }
                con.navPoints = rightNavs;
                
            }
        }
    }

    public void DoorOpened()
    {
        foreach (GameObject guard in guards)
        {
            StateController con = guard.gameObject.GetComponent<StateController>();
            foreach(GameObject nav in rightNavs)
            {
                nav.SetActive(false);
            }
            foreach (GameObject nav in leftNavs)
            {
                nav.SetActive(false);
            }
            foreach (GameObject nav in doorOpenNavs)
            {
                nav.SetActive(true);
            }
            con.navPoints = doorOpenNavs;

        }
    }

    public void setZone(GameObject obj)
    {
        currentZone = obj;
    }
}
