using UnityEngine;

/// <summary>
/// This is an extension class to destroy children.  Guess it's probably needed since the guy said so.
/// </summary>
public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyImmediately = false)
    {
        //Run through all the children.
        foreach(Transform child in t)
        {
            //We'll be destroying things one way or another.  The guy says to do this,
            //But Unity documentation says you should never destroy something you are iterating over.
            //I mean, we're techinically iterating over t, not the child, so maybe this is alright?
            //We should be defaulting this to false anyway.
            if (destroyImmediately)
            {
                MonoBehaviour.DestroyImmediate(child.gameObject);
            }
            else
            {
                MonoBehaviour.Destroy(child.gameObject);
            }
        }
    }
}