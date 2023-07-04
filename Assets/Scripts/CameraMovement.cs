using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Vector3 _offset;
    private GameObject _target; 
    
    void Start()
    {
        _target = GameObject.FindWithTag("Player");
        _offset = transform.position - _target.transform.position;
    }
    
    void LateUpdate()
    {
        if(_target != null)
        {
            // Add offset and move camera 
            Vector3 newPosition = _target.transform.position + _offset;
            transform.position = newPosition;
        }
    }
}
