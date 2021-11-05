using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayController : MonoBehaviour
{
    [System.Serializable]
    public class rays
    {
        public Transform position;
        public bool x;
        public bool y;
        public bool z;
        [Range(-1,1)]
        public int multiplier;
    }

    public List<rays> rayList;
    public float rayDistance;
    public terrainPaint paint;

    private void FixedUpdate()
    {
        foreach (rays ray in rayList)
        {
            Vector3 posImpact = new Vector3(
                !ray.x ? ray.position.position.x : (ray.position.position.x - rayDistance* ray.multiplier),
                !ray.y ? ray.position.position.y : (ray.position.position.y - rayDistance* ray.multiplier),
                !ray.z ? ray.position.position.z : (ray.position.position.z - rayDistance* ray.multiplier));

            Debug.DrawLine(ray.position.position, posImpact, Color.red);
            RaycastHit hit;
            if (Physics.Linecast(ray.position.position, posImpact, out hit))
            {
                if (hit.transform.tag.Equals("terreno")) ;
                    Debug.Log(Int32.Parse(hit.point.x.ToString("F0"))+" "+ Int32.Parse(hit.point.z.ToString("F0")));
            }
        }
    }
}
