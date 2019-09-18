using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public Image stateDisplay;
    public Sprite walkStateSprite;
    public Sprite sneakStateSprite;
    public Sprite sprintStateSprite;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement mov = player.GetComponent<PlayerMovement>();
        if (mov.sneaking)
        {
            stateDisplay.sprite = sneakStateSprite;
        }
        else if (mov.sprinting)
        {
            stateDisplay.sprite = sprintStateSprite;
        }
        else
        {
            stateDisplay.sprite = walkStateSprite;
        }
    }
}
