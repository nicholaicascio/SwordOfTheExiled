using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOnCOllide : MonoBehaviour
{
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Animator anim = targetObject.GetComponent<Animator>();
        anim.SetBool("isOpen", true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
