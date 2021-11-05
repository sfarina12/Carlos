using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomCargeneration : MonoBehaviour
{
    public List<GameObject> cars;
    public List<Transform> roads;
    [Range(0,100)]
    public int spawnChancePercentage=50;
    [Space,Header("testing purpose only")]
    public GameObject singleRoad;
    public bool noList = false;

    private void Start()
    {
        if (!noList)
            foreach (Transform obj in roads)
            {
                foreach (Transform child in obj)
                {
                    GameObject grandChild = child.GetChild(0).gameObject;
                    geteratePosition(grandChild);
                }
            }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && noList)
        { geteratePosition(singleRoad); }
    }

    private void geteratePosition(GameObject road)
    {
        if (isCreatable(100))
        {
            Bounds roarBounds = road.GetComponent<MeshFilter>().mesh.bounds;
            GameObject randomDetail = cars[Random.Range(0, cars.Count - 1)];
            randomDetail.transform.localScale = new Vector3(170, 170, 170);

            //--------------------------------------------------------------------------------------

            Bounds detailBounds = randomDetail.GetComponent<MeshFilter>().sharedMesh.bounds;
            float detailY = detailBounds.size.y;
            float boundsY = roarBounds.size.y / 3f;

            float randomPositionFromRoad_X = Random.Range(roarBounds.min.x - (detailBounds.min.x * 170), roarBounds.max.x - (detailBounds.max.x * 170));
            float randomPositionFromRoad_Z = Random.Range(roarBounds.min.z - (detailBounds.min.z * 170), roarBounds.max.z - (detailBounds.max.z * 170));

            //--------------------------------------------------------------------------------------
            float calculatedY = boundsY+ (detailY *170);
            instantiateCar(randomDetail,road,new Vector3(randomPositionFromRoad_X, calculatedY, randomPositionFromRoad_Z), Quaternion.Euler(0,-90+road.transform.parent.rotation.eulerAngles.y,0));
        }
    }
    private bool isCreatable(int chance) { return (Random.Range(0, 100) < chance); }
    private void instantiateCar(GameObject randomDetail,GameObject parent,Vector3 position,Quaternion rotation)
    {
        GameObject newProp = Instantiate(randomDetail, Vector3.zero, rotation);

        newProp.transform.parent = parent.transform;
        newProp.transform.localPosition = position;
        newProp.tag = "car";
        newProp.AddComponent<collisionFix>().debug = noList;
    }
}
