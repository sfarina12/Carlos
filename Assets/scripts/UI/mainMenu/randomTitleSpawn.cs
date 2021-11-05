using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomTitleSpawn : MonoBehaviour
{
    public Transform title;
    public Vector3 limitUp;
    public Vector3 limitDown;

    void Start()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(limitDown.x, limitUp.x),
            Random.Range(limitDown.y, limitUp.y),
            Random.Range(limitDown.z, limitUp.z));
        Quaternion randomRotation = Quaternion.Euler(new Vector3(
            -90,
            Random.Range(-10,10),
            Random.Range(-10,10)));

        title.position = randomPosition;
        title.rotation = randomRotation;
    }

}
