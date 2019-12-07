using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PullUpMenu : MonoBehaviour
{
    public GameObject quitCanv;
    public bool esc;
    // Start is called before the first frame update
    void Start()
    {
        //quitCanv = GameObject.FindGameObjectWithTag("QuitGameCanvas");
        quitCanv = GameObject.Find("QuitGameCanvas");
        quitCanv.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        esc = Input.GetButton("Menu");
        if (esc)
        {
            quitCanv.gameObject.SetActive(true);
        }
    }
}
