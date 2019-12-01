using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    private void Awake()
    {
        //We'll have a random offset.
        Vector2 offset = Random.insideUnitCircle * 3f;

        //Create a new vector3, and we'll offsett it by the offset.
        Vector3 position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z);

        //Now, make it.
        MasterManager.NetworkInstantiate(_prefab, position, Quaternion.identity);
    }
}
