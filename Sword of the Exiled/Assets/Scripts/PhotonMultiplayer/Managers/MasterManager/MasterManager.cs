using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Pulled from First Gear Games on YouTube.
/// </summary>
[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : ScriptableObjectSingleton<MasterManager>
{

    [SerializeField]
    private GameSettings _gameSettings;
    public static GameSettings GameSettings { get { return Instance._gameSettings; } }

    //This will be a list of network prefabs because Photon has weird networking instantiation that's rough.  See NetworkedPrefab.cs
    [SerializeField]
    private List<NetworkedPrefab> _networkedPrefabs = new List<NetworkedPrefab>();

    /// <summary>
    /// This will take an object, make sure it's one of our NetworkedPrefab objects, and instantiate it through Photon if it is.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns>The game object instantiated.</returns>
    public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
    {
        foreach (NetworkedPrefab networkedPrefab in Instance._networkedPrefabs)
        {
            //Check for the object to make sure it matches our prefab variable.  If so, instantiate it.
            if (networkedPrefab.Prefab == obj)
            {
                //Check for an empty path.
                if (networkedPrefab.Path != string.Empty)
                {
                    //Instantiate and return the object.
                    GameObject result = PhotonNetwork.Instantiate(networkedPrefab.Path, position, rotation);
                    return result;
                }
                else
                {
                    //There was no path for some reason, so return nothing.
                    Debug.LogError("Path is empty for GameObject: " + networkedPrefab.Prefab);
                    return null;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// This will build our network prefabs list.  We are going to do it before the scene loads.  It will require editor code, so
    /// it should only run in the editor itself.
    /// IMPORTANT
    /// IMPORTANT
    /// This has to be run once in the editor after everything has be built so that this will be built.  Otherwise, it won't build
    /// and won't have all the data it needs.  This includes needing to be done anything something changes or moves.
    /// IMPORTANT
    /// IMPORTANT
    /// TODO:
    /// See if you can get it to do this when it runs instead of on editor.  Just as a thought.  That way you won't have to do this
    /// everytime you make a change.  So, if you build and run before running in the editor, things won't work as this is.  This works
    /// because classes are serializable.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PopulateNetworkPrefabs()
    {
        //Make sure this only runs in the editor.  Otherwise, exit.
#if UNITY_EDITOR

        //Clear out the prefab list to start.
        Instance._networkedPrefabs.Clear();

        //Get a list of all resources from the entire resources folder.
        GameObject[] results = Resources.LoadAll<GameObject>("");

        //Loop through the list.
        for (int i = 0; i < results.Length; i++)
        {
            //Check to see if this is a Photon network object.
            if (results[i].GetComponent<PhotonView>() != null)
            {
                //This is a photon network object, so get the path and add it to the list.
                string path = AssetDatabase.GetAssetPath(results[i]);
                Instance._networkedPrefabs.Add(new NetworkedPrefab(results[i], path));
            }
        }

        //Really, this is for testing purposes.
        //for (int i = 0; i < Instance._networkedPrefabs.Count; i++)
        //{
        //    Debug.Log("Newtork Prefab " + Instance._networkedPrefabs[i].Prefab.name + ": " + Instance._networkedPrefabs[i].Path);
        //}
#endif
    }

    private void Update()
    {
        Debug.Log("This is the update from the master manager.");

        if (Input.GetKeyDown("Menu") || Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Menu button pressed.");
        }
    }
}
