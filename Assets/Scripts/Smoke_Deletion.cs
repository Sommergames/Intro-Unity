using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke_Deletion : MonoBehaviour
{

    private float _smokeTimer = 1f;
    void Start()
    {
        
    }


    // Destroy Smoke animation after 1 second 
    void Update()
    {
        _smokeTimer -= Time.deltaTime;
        if (_smokeTimer < 0)
        {
            Destroy(this.gameObject);
        }
        
        
    }
}
