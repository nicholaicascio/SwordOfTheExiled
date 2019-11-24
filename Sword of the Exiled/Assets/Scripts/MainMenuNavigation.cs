using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    private Animator CamAnim;
    public Animator CanvAnim;
    //private Animation[] clips;
    public Canvas AreYouSure;
    public GameObject LobbyCam;
    
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
    public void pressLobby()
    {
        Camera thiscam = this.gameObject.GetComponent<Camera>();
        thiscam.enabled = false;
        LobbyCam.gameObject.SetActive(true);
    }

    public void DollyEnded()
    {
        //Debug.Log("dollycomplete");
        CanvAnim.SetBool("dollyComplete", true);
    }

    public void ExitClicked()
    {
        AreYouSure.gameObject.SetActive(true);
    }
    
    public void ExitCancelled()
    {
        AreYouSure.gameObject.SetActive(false);
    }

    public void ExitConfirmed()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
