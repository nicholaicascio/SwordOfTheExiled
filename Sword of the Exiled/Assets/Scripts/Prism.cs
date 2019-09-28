using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prism : MonoBehaviour
{
    public string outColor;
    public GameObject red, blue, green, magenta, cyan, yellow;
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
        if (outColor == "magenta")
        {
            magenta.SetActive(true);
        }
        if (outColor == "cyan")
        {
            cyan.SetActive(true);
        }
        if (outColor == "yellow")
        {
            yellow.SetActive(true);
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
        if (outColor != "magenta")
        {
            magenta.SetActive(false);
        }
        if (outColor != "cyan")
        {
            cyan.SetActive(false);
        }
        if (outColor != "yellow")
        {
            yellow.SetActive(false);
        }
    }
    public string getColor()
    {
        return outColor;
    }

    public void setColor(string col)
    {
        outColor = col;
    }
}
