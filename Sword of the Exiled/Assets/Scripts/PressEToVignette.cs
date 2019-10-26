using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEToVignette : MonoBehaviour
{
    public Canvas tooltip;
    bool playerIsInside = false;
    bool playerIsLooking = false;
    public Camera lookAtThis;
    public Camera MainCamera;
    Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        MainCamera = other.gameObject.GetComponentInChildren<Camera>();
        rig = other.gameObject.GetComponent<Rigidbody>();
        tooltip.gameObject.SetActive(true);
        playerIsInside = true;
    }
    private void OnTriggerExit(Collider other)
    {
        MainCamera = null;
        tooltip.gameObject.SetActive(false);
        playerIsInside = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!playerIsLooking && playerIsInside && Input.GetKeyDown(KeyCode.E))
        {
            MainCamera.gameObject.SetActive(false);
            lookAtThis.gameObject.SetActive(true);
            playerIsLooking = true;
            rig.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            
        }
        else if (playerIsLooking && Input.GetKeyDown(KeyCode.E))
        {
            lookAtThis.gameObject.SetActive(false);
            MainCamera.gameObject.SetActive(true);
            playerIsLooking = false;
            rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
