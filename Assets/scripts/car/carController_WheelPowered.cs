using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController_WheelPowered : MonoBehaviour
{
    [System.Serializable]
    public class AxilsInfo 
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;

        public Transform visualLeftWheel;
        public Transform visualRightWheel;

        public bool isEngine;
        public bool canSteer;
    }

    public List<AxilsInfo> Axils = new List<AxilsInfo>();
    public float maxEnginepower;
    public float maxSteeringAngle;

    private void FixedUpdate()
    {
        float power = maxEnginepower * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxilsInfo axil in Axils)
        {
            if (axil.canSteer)
            {
                axil.leftWheel.steerAngle = steering;
                axil.rightWheel.steerAngle = steering;
            }
            if (axil.isEngine)
            {
                axil.leftWheel.motorTorque = power;
                axil.rightWheel.motorTorque = power;
            }

            applyVisual(axil.leftWheel,axil.visualLeftWheel);
            applyVisual(axil.rightWheel, axil.visualRightWheel);
        }
    }
    void applyVisual(WheelCollider collider,Transform wheel)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position,out rotation);
        wheel.position = position;
        wheel.rotation = rotation;
    }
}
