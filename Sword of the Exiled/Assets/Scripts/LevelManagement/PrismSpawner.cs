using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will basically handle the prism spawning.
/// </summary>
public class PrismSpawner : MonoBehaviourPun
{
    //We will get our prism prefabs.
    [SerializeField]
    private GameObject _leftPrismPrefab;
    [SerializeField]
    private GameObject _rightPrismPrefab;
    [SerializeField]
    private GameObject _upPrismPrefab;
    [SerializeField]
    private GameObject _downPrismPrefab;

    //This will be our list of left prism spawn points
    [SerializeField]
    private List<GameObject> _leftPrismSpawns;
    //This will be our list of right prism points.
    [SerializeField]
    private List<GameObject> _rightPrismSpawns;
    //This will be our list of up prism spawn points.
    [SerializeField]
    private List<GameObject> _upPrismSpawns;
    //This will be our list of down prism points.
    [SerializeField]
    private List<GameObject> _downPrismSpawns;

    // Start is called before the first frame update
    void Start()
    {
        //We will only create the prisms if we are the master client.
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject prism;
            //So, we are going to run through all our prism spawn point lists,
            //and we will instantiate a prism there.
            //Start with the left prism.
            foreach(GameObject go in _leftPrismSpawns)
            {
                prism = MasterManager.NetworkInstantiate(_leftPrismPrefab, go.transform.position, Quaternion.Euler(0,-90,0));
            }
            //Start with the right prism.
            foreach (GameObject go in _rightPrismSpawns)
            {
                MasterManager.NetworkInstantiate(_rightPrismPrefab, go.transform.position, Quaternion.Euler(0, 90, 0));
            }
            //Start with the up prism.
            foreach (GameObject go in _upPrismSpawns)
            {
                MasterManager.NetworkInstantiate(_upPrismPrefab, go.transform.position, Quaternion.identity);
            }
            //Start with the left prism.
            foreach (GameObject go in _downPrismSpawns)
            {
                MasterManager.NetworkInstantiate(_downPrismPrefab, go.transform.position, Quaternion.Euler(0, 180, 0));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
