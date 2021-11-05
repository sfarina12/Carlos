using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallJump : MonoBehaviour
{
    public CharacterController characterController;
    public int isleft=0;// 0 - non  e ne a destra ne sinistra | 1 - sinistra | 2 - destra
    public float MinimumWallRunningHeight = 2;
    public float GravityReductionOnWall = 3f;

    [Min(0)]
    public float speedBooster=0f;
    [Min(1)]
    public float speedBoostVelocity = 1f;

    [HideInInspector]
    public bool isWallRunning = false;
    [HideInInspector]
    public bool canMove = true;

    public Animator cameraAnimator;

    private float gravity;
    
    void Start() { gravity = characterController.GetComponent<SC_FPSController>().gravity; }

    private void OnCollisionStay(Collision collision)
    {
        if (canMove)
        {
            if (collision.gameObject.tag == "wall")
            {
                Vector3 characterForceDirection = characterController.velocity;

                if ((characterForceDirection != Vector3.zero) && (characterController.transform.position.y >= MinimumWallRunningHeight))
                {
                    if (Input.GetAxis("Horizontal") > 0) { isleft = 1; cameraAnimator.SetBool("isWallRunningR", true); cameraAnimator.SetBool("isWallRunningL", false); }
                    if (Input.GetAxis("Horizontal") < 0) { isleft = 2; cameraAnimator.SetBool("isWallRunningL", true); cameraAnimator.SetBool("isWallRunningR", false); }

                    if (Input.GetAxis("Horizontal") != 0)
                        characterController.GetComponent<SC_FPSController>().gravity = gravity / GravityReductionOnWall;
                    else
                        characterController.GetComponent<SC_FPSController>().gravity = gravity;

                    isWallRunning = true;
                }
                else
                {
                    isleft = 0;
                    characterController.GetComponent<SC_FPSController>().gravity = gravity;
                    isWallRunning = false;
                    cameraAnimator.SetBool("isWallRunningR", false);
                    cameraAnimator.SetBool("isWallRunningL", false);
                }
            }
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {

        if (canMove)
        {
            characterController.GetComponent<SC_FPSController>().gravity = 20f;
            isWallRunning = false;
            cameraAnimator.SetBool("isWallRunningR", false);
            cameraAnimator.SetBool("isWallRunningL", false);
        }
        
    }
}
