using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public Rigidbody car;
    public float fowrardAcceleration;
    public float turnAccelleration;
    [Space]
    public float gravity;
    public Transform wheel;

    private void FixedUpdate()
    {
        Move();
        Turn();
        Fall();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W)) car.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * fowrardAcceleration * 10);
        else if (Input.GetKey(KeyCode.S)) car.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * -fowrardAcceleration * 10);

        Vector3 localVelocity = transform.InverseTransformDirection(car.velocity);
        localVelocity.x = 0;
        car.velocity = transform.TransformDirection(localVelocity);
    }
    void Turn()
    {
        if (Input.GetKey(KeyCode.D)) car.AddTorque(Vector3.up * turnAccelleration * 10);
        else if (Input.GetKey(KeyCode.A)) car.AddTorque(-Vector3.up * turnAccelleration * 10);
    }
    void Fall()
    {
        car.AddForce(Vector3.down * gravity * 10);
    }
}
