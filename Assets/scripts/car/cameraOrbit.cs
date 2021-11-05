using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOrbit : MonoBehaviour
{
    [Header("Look settings")]
    public GameObject cameraPivot;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public bool invertY = false;
    [Space]
    public Vector2 cameraDistanceMinMax = new Vector2(05f,5f);
    public float distanceMultiplayer=6f;

    float initialY;
    private float cameraDistance;
    [HideInInspector]
    public float rotationX = 0f;
    [HideInInspector]
    public float rotationY = 0f;
    private Vector3 cameraDirection;
    void Start()
    {
        cameraDirection = transform.localPosition.normalized;
        cameraDistance = cameraDistanceMinMax.y;
        initialY = cameraDistanceMinMax.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        rotationY += (invertY?-1:1)*Input.GetAxis("Mouse X") * lookSpeed;

        cameraPivot.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            float y=cameraDistanceMinMax.y;
            if ((y - 1) > cameraDistanceMinMax.x)
                cameraDistanceMinMax.y -= 1;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            float y = cameraDistanceMinMax.y;
            if ((y + 1) < initialY)
                cameraDistanceMinMax.y += 1;
        }

        cameraCollition();
    }

    public void cameraCollition()
    {
        Vector3 collisionPosition = transform.TransformPoint(cameraDirection * cameraDistanceMinMax.y);

        RaycastHit hit;

        Debug.DrawLine(cameraPivot.transform.position, collisionPosition, Color.red);
        
        if (Physics.Linecast(cameraPivot.transform.position, collisionPosition, out hit))
        {
            cameraDistance = Mathf.Clamp(hit.distance* distanceMultiplayer, cameraDistanceMinMax.x,cameraDistanceMinMax.y);
        }
        else
            cameraDistance = cameraDistanceMinMax.y;

        transform.localPosition = cameraDirection * cameraDistance;
    }
}
