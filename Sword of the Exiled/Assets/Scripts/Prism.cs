using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prism : MonoBehaviour
{
    public string outColor;
    public GameObject red, blue, green;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (outColor == "red")
        {
            red.SetActive(true);
        }
        if (outColor == "blue")
        {
            blue.SetActive(true);
        }
        if (outColor == "green")
        {
            green.SetActive(true);
        }
        if (outColor != "red")
        {
            red.SetActive(false);
        }
        if (outColor != "blue")
        {
            blue.SetActive(false);
        }
        if (outColor != "green")
        {
            green.SetActive(false);
        }
    }

    public void setColor(string col)
    {
        outColor = col;
    }
}
