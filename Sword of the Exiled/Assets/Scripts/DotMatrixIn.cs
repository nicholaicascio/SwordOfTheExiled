using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotMatrixIn : MonoBehaviour
{
    public Image[] dots;
    public float FadeinTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        dots = gameObject.GetComponentsInChildren<Image>();
        foreach (Image dot in dots)
        {
            dot.gameObject.SetActive(false);
        }
        foreach (Image dot in dots)
        {
            StartCoroutine("dotIn", dot);
        }

    }

    IEnumerator dotIn(Image dot)
    {
        float num = Random.Range(0.5f, FadeinTime);

        yield return new WaitForSeconds(num);
        dot.gameObject.SetActive(true);
    }

    IEnumerator dotOut(Image dot)
    {
        float num = Random.Range(0.5f, FadeinTime);

        yield return new WaitForSeconds(num);
        dot.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
