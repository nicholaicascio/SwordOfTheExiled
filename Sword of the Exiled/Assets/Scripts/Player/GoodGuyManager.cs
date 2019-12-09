using Photon.Pun;
using UnityEngine;

public class GoodGuyManager : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {

        //Check to see if the player is mine.  If not, find the camera and deactivate it.  Used for multiplayer.
        if (!base.photonView.IsMine)
        {
            Camera cam = GetComponentInChildren<Camera>();
            cam.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
