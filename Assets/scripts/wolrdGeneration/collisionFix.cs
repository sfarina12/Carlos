using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionFix : MonoBehaviour
{
    public bool debug=false;
    private void Awake()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, (transform.GetComponent<MeshFilter>().mesh.bounds.extents*170), Quaternion.identity, LayerMask.GetMask("Default"));

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (debug) Debug.Log(hitColliders[i].transform.name);

            if (hitColliders[i].transform.tag.Equals("car"))
            {
                if (debug) Debug.Log(transform.name+" DELETED");
                Destroy(hitColliders[i].gameObject);
            }
        }
        
        transform.GetComponent<MeshCollider>().convex = true;
        Rigidbody r = transform.gameObject.AddComponent<Rigidbody>();
        r.mass = 1800;
        r.drag = 1.5f;
        r.angularDrag = 1.5f;
    }

    
}
