using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    private Animator CamAnim;
    public Animator CanvAnim;
    private Animation[] clips;
    // Start is called before the first frame update
    void Start()
    {
        CamAnim = this.gameObject.GetComponent<Animator>();
        
    }

    public void pressPlay()
    {
        //Debug.Log("play");
        CamAnim.SetBool("Play", true);
        CanvAnim.SetBool("playPressed", true);
    }

    public void DollyEnded()
    {
        //Debug.Log("dollycomplete");
        CanvAnim.SetBool("dollyComplete", true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
