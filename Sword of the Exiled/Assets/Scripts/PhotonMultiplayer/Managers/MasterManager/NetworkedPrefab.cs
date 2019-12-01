using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// With Photon, you can't instantiate a prefab the way you would normally in Unity passing in a game object reference.
/// You have to instantiate using the game object name and the path in the resources folder.  Clearly this is bad
/// because it can cause issues with typos, and everytime you move something, it has to be changed everywhere.
/// This will take care of that for us.  It will automatically build the paths for us, and we will just be able
/// to instantiate normally just passing in the prefab and it will pull the stored paths.
/// </summary>
[System.Serializable]
public class NetworkedPrefab
{
    public GameObject Prefab;
    public string Path;

    public NetworkedPrefab(GameObject obj, string path)
    {
        Prefab = obj;
        Path = ReturnPrefabPathModified(path);
        //When this is called in MasterManager, it doesn't give us the path we need.  we get the first path below.  But we wan
        //Assets/Resources/File.prefab
        //Resources/File
    }

    /// <summary>
    /// When this is called in MasterManager, it doesn't give us the path we need.  This will give us just what we actually need.
    /// </summary>
    /// <param name="path">i.e. Assets/Resources/File.prefab</param>
    /// <returns>i.e. Resources/File.  Upon error, it returns an empty string.</returns>
    private string ReturnPrefabPathModified(string path)
    {
        //First, get the lenth of the extension (.prefab)
        int extensionLength = System.IO.Path.GetExtension(path).Length;

        //We will now find the part of the path we want to start from since we don't want the "Resources" part of the path.
        string pathSearch = "resources";

        //Get the start of our resources deal.
        int startIndex = path.ToLower().IndexOf(pathSearch);

        //We are adding 1 because it will have a slash that we want to ignore.  But depending on the platform, it could be either forward or backslash.
        //We don't care, just ignore it.
        int additionalLength = pathSearch.Length + 1;

        //Shouldn't happen, but if we didn't find the path, return an empty string.  Deal with that later.
        if (startIndex == -1)
        {
            //Return an empty string as we have reached an error.
            return string.Empty;
        }
        else
        {
            //Return the part of the string between the parts we don't want, from "Resources" to extension. 
            return path.Substring(startIndex + additionalLength, path.Length - (additionalLength + startIndex + extensionLength));
        }
    }
}
