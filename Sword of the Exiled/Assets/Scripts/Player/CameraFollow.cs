using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The purpose of this class is to have the camera follow the player without getting the same rotation which we definitely don't want.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    //Target that the camera will follow.  Should be set in inspector.
    [SerializeField]
    private GameObject _followTarget;

    //Vector3 that has how far away the camera will follow.
    private Vector3 _offset;

    // Use this for initialization.  Doing it this way allows us to set the camera in the inspector and such, and then we can just use what it is.  Yay!
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        _offset = transform.position - _followTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Move the camera to the player position with the offset.
        transform.position = _followTarget.transform.position + _offset;
    }
}
