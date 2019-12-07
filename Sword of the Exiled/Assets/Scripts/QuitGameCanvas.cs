using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameCanvas : MonoBehaviour
{
    public void YesPressed()
    {
        Application.Quit();
    }

    public void NoPressed()
    {
        this.gameObject.SetActive(false);
    }
}
