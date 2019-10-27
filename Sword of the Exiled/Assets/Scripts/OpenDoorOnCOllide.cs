using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOnCOllide : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject[] objectsToEnable;

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Animator anim = targetObject.GetComponent<Animator>();
        anim.SetBool("isOpen", true);
        foreach(GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
