using UnityEngine;

/// <summary>
/// We are going to create a singleton that can exist in multiple scenes without any issues because it is handled
/// in the inspector.  Cool story.  We get the benefits of referencing the singleton without having to have a direct
/// reference to the object.  Sweet!
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();

                if (results.Length == 0)
                {
                    Debug.LogError("SingletonScriptableObject -> Instance -> results lenght is 0 for type " + typeof(T).ToString() + ".");
                    return null;
                }

                if (results.Length > 1)
                {
                    Debug.LogError("SingletonScriptableObject -> Instance -> results length is greater than 1 for type " + typeof(T).ToString() + ".");
                    return null;
                }

                _instance = results[0];
            }

            return _instance;
        }
    }
}