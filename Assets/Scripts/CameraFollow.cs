using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = _target.transform.position;
        transform.rotation = _target.transform.rotation;
    }
}
