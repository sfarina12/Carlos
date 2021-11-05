using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarPhysics : MonoBehaviour
{
    protected Rigidbody Rigidbody;
    public Vector3 CenterOfMass;

    public WheelInfo[] Wheels;

    public float MotorPower = 5000f;
    public float SteerAngle = 35f;

    [Range(0, 1)]
    public float KeepGrip = 1f;
    public float Grip = 5f;
    [Space]
    public PhotonView view;
    public Camera camera;
    public switchMPController switchController;

    // Use this for initialization
    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.centerOfMass = CenterOfMass;
        OnValidate();
    }

    private void Start()
    {
        if (!view.IsMine) { camera.enabled = false; }
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E)) { switchController.switchController(); }

            for (int i = 0; i < Wheels.Length; i++)
            {
                if (Wheels[i].Motor)
                {
                    Wheels[i].WheelCollider.motorTorque = Input.GetAxis("Vertical") * MotorPower * 10;
                }
                if (Wheels[i].Steer)
                    Wheels[i].WheelCollider.steerAngle = Input.GetAxis("Horizontal") * SteerAngle;

                applyVisual(Wheels[i].WheelCollider, Wheels[i].MeshRenderer);
            }

            Rigidbody.AddForceAtPosition(transform.up * Rigidbody.velocity.magnitude * -0.1f * Grip, transform.position + transform.rotation * CenterOfMass);
        }
    }

    void applyVisual(WheelCollider collider, Transform wheel)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        wheel.position = position;
        wheel.rotation = rotation;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + CenterOfMass, .1f);
        Gizmos.DrawWireSphere(transform.position + CenterOfMass, .11f);
    }

    void OnValidate()
    {
        //Debug.Log("Validate");
        for (int i = 0; i < Wheels.Length; i++)
        {
            //settings
            var ffriction = Wheels[i].WheelCollider.forwardFriction;
            var sfriction = Wheels[i].WheelCollider.sidewaysFriction;
            ffriction.asymptoteValue = Wheels[i].WheelCollider.forwardFriction.extremumValue * KeepGrip * 0.998f + 0.002f;
            sfriction.extremumValue = 1f;
            ffriction.extremumSlip = 1f;
            ffriction.asymptoteSlip = 2f;
            ffriction.stiffness = Grip;
            sfriction.extremumValue = 1f;
            sfriction.asymptoteValue = Wheels[i].WheelCollider.sidewaysFriction.extremumValue * KeepGrip * 0.998f + 0.002f;
            sfriction.extremumSlip = 0.5f;
            sfriction.asymptoteSlip = 1f;
            sfriction.stiffness = Grip;
            Wheels[i].WheelCollider.forwardFriction = ffriction;
            Wheels[i].WheelCollider.sidewaysFriction = sfriction;
        }
    }

    [System.Serializable]
    public struct WheelInfo
    {
        public WheelCollider WheelCollider;
        public Transform MeshRenderer;
        public bool Steer;
        public bool Motor;
        [HideInInspector]
        public float Rotation;
    }
}