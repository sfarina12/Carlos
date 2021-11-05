using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class race : MonoBehaviour
{

    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="player")
        {
            
        }
    }
    void Update()
    {
        
    }
}
