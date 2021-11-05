using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomDetails : MonoBehaviour
{
    public List<GameObject> details;
    public List<Transform> pavements;
    [Range(0, 100)]
    public int spawnChancePercentage = 50;
    [Min(0)]
    public Vector2 spawnRandomScale = new Vector2(1,1);

    private void Start()
    {
        foreach(Transform obj in pavements)
        {
            foreach (Transform child in obj)
            {
                GameObject grandChild=child.GetChild(0).gameObject;
                geteratePosition(grandChild); 
            }
        }
    }

    private void geteratePosition(GameObject pavement)
    {
        if (isCreatable(100))
        {
            Bounds pavementBounds = pavement.GetComponent<MeshFilter>().mesh.bounds;
            GameObject pavementDetail = details[Random.Range(0, details.Count - 1)];
            float randomScale = Random.Range(spawnRandomScale.x, spawnRandomScale.y);
            pavementDetail.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            //--------------------------------------------------------------------------------------

            Bounds detailBounds = pavementDetail.GetComponent<MeshFilter>().sharedMesh.bounds;
            float detailY = detailBounds.size.y;
            float boundsY = pavementBounds.size.y;

            float randomPositionFromRoad_X = Random.Range(pavementBounds.min.x - (detailBounds.min.x * randomScale), pavementBounds.max.x - (detailBounds.max.x * randomScale));
            float randomPositionFromRoad_Z = Random.Range(pavementBounds.min.z - (detailBounds.min.z * randomScale), pavementBounds.max.z - (detailBounds.max.z * randomScale));


            //--------------------------------------------------------------------------------------
            float calculatedY = boundsY;
            instantiateCar(pavementDetail, pavement, new Vector3(randomPositionFromRoad_X, calculatedY, randomPositionFromRoad_Z), Quaternion.Euler(0, Random.Range(-180,180), 0));
        }
    }
    private bool isCreatable(int chance) { return (Random.Range(0, 100) < chance); }
    private void instantiateCar(GameObject randomDetail, GameObject parent, Vector3 position, Quaternion rotation)
    {
        GameObject newProp = Instantiate(randomDetail, Vector3.zero, rotation);
        newProp.transform.parent = parent.transform;
        newProp.transform.localPosition = position;
    }

    /*private void geteratePosition(GameObject pavement)
    {
        if (Random.RandomRange(0, 100) < 50)
        {
            Bounds bounds = pavement.GetComponent<MeshFilter>().mesh.bounds;
            GameObject randomDetail = details[Random.Range(0, details.Count - 1)];

            float detailX = randomDetail.GetComponent<MeshFilter>().sharedMesh.bounds.size.x / 2;
            float detailZ = randomDetail.GetComponent<MeshFilter>().sharedMesh.bounds.size.z / 2;

            float randomX = Random.Range(0, bounds.size.x);
            float randomZ = Random.Range(0, bounds.size.z);
            float boundsY = bounds.size.y;

            if (bounds.size.x > detailX || bounds.size.z > detailZ)
            {
                randomX = randomX < detailX ? detailX : randomX;
                if (randomX != detailX)
                    randomX = randomX > (bounds.size.x - detailX) ? (bounds.size.x - detailX) : randomX;

                randomZ = randomZ < detailZ ? detailZ : randomZ;
                if (randomZ != detailZ)
                    randomZ = randomZ > (bounds.size.z - detailZ) ? (bounds.size.z - detailZ) : randomZ;
            }

            GameObject newProp = Instantiate(randomDetail, Vector3.zero, Quaternion.Euler(0, Random.Range(-180, 180), 0));

            float randomsize = newProp.transform.localScale.y + (Random.Range(-0.8f, 0.3f));

            newProp.transform.localScale = new Vector3(randomsize, randomsize, randomsize);
            newProp.transform.parent = pavement.transform;
            newProp.transform.localPosition = new Vector3(randomX, boundsY, randomZ);
        }*/
}

