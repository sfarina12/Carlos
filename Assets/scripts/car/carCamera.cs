using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carCamera : MonoBehaviour
{
    public Transform car;
    Transform worldRotation;
    public float smooth;
    public float rotationSmooth;

    private void Start()
    {
        worldRotation = GameObject.Find("Terrain").transform;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,car.position,smooth);
        //transform.rotation = Quaternion.Slerp(transform.rotation, car.rotation, rotationSmooth);
        //transform.rotation = Quaternion.Euler(new Vector3(0,transform.rotation.eulerAngles.y,0));

        transform.rotation = Quaternion.Slerp(transform.rotation, worldRotation.rotation, rotationSmooth);
        //transform.rotation = worldRotation.rotation;
    }
}
