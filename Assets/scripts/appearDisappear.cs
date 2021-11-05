using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearDisappear : MonoBehaviour
{
    public GameObject obj;
    [HideInInspector]
    public GameObject lastCar;
    public Vector3 spawn;
    public bool spawnAtStartup = true;

    private void Start() { if(spawnAtStartup) PhotonNetwork.Instantiate(obj.name, spawn, Quaternion.Euler(0,0,0)); }

    public void instantiateMultiplayer(GameObject spawnObject) { PhotonNetwork.Instantiate(spawnObject.name, spawn, Quaternion.Euler(0, 0, 0)); }
    public void instantiateMultiplayer(GameObject spawnObject,Vector3 position) { PhotonNetwork.Instantiate(spawnObject.name, position, Quaternion.Euler(0, 0, 0)); }
    public void instantiateMultiplayer(GameObject spawnObject, Vector3 position, Quaternion rotation) { PhotonNetwork.Instantiate(spawnObject.name, position, rotation); }
    public void instantiateMultiplayer(GameObject spawnObject, Vector3 position, Quaternion rotation, GameObject desiredCar) 
    { 
        GameObject car = PhotonNetwork.Instantiate(spawnObject.name, position, rotation);
        car.GetComponent<instantiateCar>().changeCar(desiredCar);
    }

    public void destroyMultiplayer(GameObject destryObject) { PhotonNetwork.Destroy(destryObject); }
}
