using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class schooseCar : MonoBehaviour
{
    public Camera camera;
    public float maxDistance;
    public switchMPController mpController;
    [Space]
    public GameObject text;

    RaycastHit hit;

    private void Update()
    {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance))
        {
            if (hit.transform.tag.Equals("car"))
            {
                mpController.desiredCar = hit.transform.gameObject;
                text.active = true; 
            }
        }
        else
            text.active = false;
    }
}
