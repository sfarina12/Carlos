using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public bool doppioSalto = false;
    public Camera playerCamera;
    public GameObject cameraContainer;
    public float oppositJumpForceReductionSpeed = 6f;
    public PhotonView view;
    public TextMeshPro nickname;
    public switchMPController switchController;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    int numberOfJumps = 1;
    float rotationX = 0;
    float fovDefault = 0;
    float oppositeJumpForce = 1f;
    bool isLeftForce = false;
    bool itWasWallRunning = false;
    bool wasOnWall = false;
    bool doubleJumped = false;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        if (view.IsMine) 
        { 
            playerCamera.enabled = true;
        }
        
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        fovDefault = playerCamera.fieldOfView;
    }

    private void LateUpdate()
    {
        if (view.IsMine)
            if (Input.GetKeyDown(KeyCode.Q)) { switchController.switchController(); }
    }
    void Update()
    {
        if (view.IsMine)
        {
            bool wallRunning = characterController.GetComponent<wallJump>().isWallRunning;
            float speedboostWallRunning = characterController.GetComponent<wallJump>().speedBooster;
            float wSpeed = walkingSpeed;
            float rSpeed = runningSpeed;
            //devi sottrarre il valore precedente e non il valore di defaoult
            //boost speed if wallrunning
            if (itWasWallRunning && !wallRunning)
            {
                rSpeed -= Mathf.Lerp(runningSpeed, speedboostWallRunning, Time.deltaTime);
                wSpeed -= Mathf.Lerp(walkingSpeed, speedboostWallRunning, Time.deltaTime);
                itWasWallRunning = false;
            }

            if (wallRunning)
            {
                rSpeed += Mathf.Lerp(runningSpeed, speedboostWallRunning, Time.deltaTime);
                wSpeed += Mathf.Lerp(walkingSpeed, speedboostWallRunning, Time.deltaTime);
                itWasWallRunning = true;
                wasOnWall = true;
            }

            if(characterController.isGrounded)
                wasOnWall = false;

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);

            //change fov if sprinting
            if (isRunning)
                playerCamera.fieldOfView = fovDefault + 3.0f;
            else
                playerCamera.fieldOfView = fovDefault;

            float curSpeedX = canMove ? (isRunning ? rSpeed : wSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? rSpeed : wSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;

            //oppositeJumpForce jump force on wallrunning
            if (!isLeftForce)
                if ((oppositeJumpForce > curSpeedY) && wasOnWall && (oppositeJumpForce>1f))
                {
                    oppositeJumpForce -= oppositJumpForceReductionSpeed * Time.deltaTime;
                    moveDirection = (forward * curSpeedX) + (right * oppositeJumpForce);
                    
                }
                else
                {
                    moveDirection = (forward * curSpeedX) + (right * curSpeedY);
                    oppositeJumpForce = 1f;
                }
            else
            {
                if ((oppositeJumpForce < curSpeedY) && wasOnWall && (oppositeJumpForce < 1f))
                {
                    moveDirection = (forward * curSpeedX) + (right * oppositeJumpForce);
                    oppositeJumpForce += oppositJumpForceReductionSpeed * Time.deltaTime;
                }
                else
                {
                    if (!doubleJumped)
                    {
                        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
                        oppositeJumpForce = 1f;
                    }
                }
            }

            //cannot double jump on wall, but outside
            //single jump
            if (Input.GetKeyDown("space") && canMove && characterController.isGrounded)
            {
                numberOfJumps = 1;

                if (wallRunning)
                    moveDirection.y = jumpSpeed;
                else
                    moveDirection.y = jumpSpeed;
            }
            else
                moveDirection.y = movementDirectionY;

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
                if (gravity == 0)
                    moveDirection.y = 0;

                if (Input.GetKeyDown("space") && canMove && wallRunning)
                {
                    numberOfJumps = 1;
                    moveDirection.y = jumpSpeed;

                    oppositeJumpForce = curSpeedY * -1;
                    if (isLeftForce)
                    oppositeJumpForce = curSpeedY * -1;

                    if (oppositeJumpForce > 0)
                        isLeftForce = false;
                    else
                        isLeftForce = true;
                }

                if (doppioSalto)
                    if (Input.GetKeyDown("space") && canMove && numberOfJumps > 0 && (!wallRunning))
                    {
                        numberOfJumps--;
                        moveDirection.y = jumpSpeed;
                    }
            }

            //update UI
            this.GetComponent<UIcontroller>().updateSpeedText(characterController.velocity);


            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                cameraContainer.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
        else nickname.text = view.Owner.NickName;

    }
}