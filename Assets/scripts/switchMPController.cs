using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchMPController : MonoBehaviour
{
    appearDisappear spawnMP;
    public GameObject objectToSpawm;
    [HideInInspector]
    public GameObject desiredCar;
    [Tooltip("chack if is the car or the human to request to change aspect")]
    public bool isHuman = true;

    private void Start() { spawnMP = GameObject.Find("mp").GetComponent<appearDisappear>(); }

    public void switchController()
    {
        if (isHuman)
        {
            Vector3 rot = new Vector3(0, desiredCar.transform.rotation.eulerAngles.y + 90, 0);
            spawnMP.lastCar=desiredCar;
            spawnMP.instantiateMultiplayer(objectToSpawm, transform.position, Quaternion.Euler(rot), desiredCar);
            desiredCar.active = false;
        }
        else
        {
            spawnMP.lastCar.active = true;
            spawnMP.lastCar.transform.position = new Vector3(transform.position.x+7, transform.position.y, transform.position.z);
            spawnMP.lastCar.transform.rotation = transform.rotation;
            spawnMP.instantiateMultiplayer(objectToSpawm, transform.position, Quaternion.Euler(0,0,0)); 
        }
        spawnMP.destroyMultiplayer(transform.gameObject);
    }

}
