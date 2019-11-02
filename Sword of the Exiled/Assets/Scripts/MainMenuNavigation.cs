using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    public void pressPlay()
    {
        anim.SetBool("Play", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
