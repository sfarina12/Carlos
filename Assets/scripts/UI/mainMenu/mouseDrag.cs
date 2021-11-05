using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseDrag : MonoBehaviour
{
    public float throwSpeedMultiplier = 1;

    Vector3 mOffset;
    float mZCoord;
    Rigidbody rigidBody;
    Vector3 previousPosition;

    private void Start() { rigidBody = transform.GetComponent<Rigidbody>(); }

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag() { transform.position = GetMouseWorldPos() + mOffset; }
    private void FixedUpdate() { previousPosition = transform.position; }

    private void OnMouseUp()
    {
        Vector3 throwVelocity = transform.position - previousPosition;
        float speed = (throwVelocity.magnitude * throwSpeedMultiplier) / Time.deltaTime;

        rigidBody.velocity=speed*throwVelocity.normalized;
    }
}
