using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class playerMovement : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float crouchingSpeed = 5.5f;
    [Space]
    public float gravity = 20.0f;
    [Space]
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    [Space]
    public Camera playerCamera;
    [Space]
    public float oppositJumpForceReductionSpeed = 6f;
    [Space]
    public PhotonView view;
    public TextMeshPro nickname;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    GameObject cameraContainer;
    int numberOfJumps = 1;
    float rotationX = 0;
    float fovDefault = 0;
    float oppositeJumpForce = 0f;
    bool isLeftForce = false;
    bool itWasWallRunning = false;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        cameraContainer = playerCamera.transform.parent.gameObject;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        fovDefault = playerCamera.fieldOfView;

        if (view.IsMine) playerCamera.enabled = true;
    }

    void Update()
    {
        if (view.IsMine)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            bool isCrouch = Input.GetKey(KeyCode.LeftControl);

            float curSpeedX = 0f;
            float curSpeedY = 0f;

            if (canMove)
            {
                if (isRunning && !isCrouch)
                {
                    curSpeedX = runningSpeed * Input.GetAxis("Vertical");
                    curSpeedY = runningSpeed * Input.GetAxis("Horizontal");
                }
                else if (isCrouch)
                {
                    curSpeedX = crouchingSpeed * Input.GetAxis("Vertical");
                    curSpeedY = crouchingSpeed * Input.GetAxis("Horizontal");
                }
                else
                {
                    curSpeedX = walkingSpeed * Input.GetAxis("Vertical");
                    curSpeedY = walkingSpeed * Input.GetAxis("Horizontal");
                }

                float movementDirectionY = moveDirection.y;
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);

                if (Input.GetKeyDown("space") && canMove && characterController.isGrounded)
                    moveDirection.y = jumpSpeed;
                else
                    moveDirection.y = movementDirectionY;

                if (!characterController.isGrounded)
                    moveDirection.y -= gravity * Time.deltaTime;

                characterController.Move(moveDirection * Time.deltaTime);

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
        }
        else nickname.text = view.Owner.NickName;

    }

    /*
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
            }



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
                if (oppositeJumpForce > 0f)
                {
                    moveDirection = (forward * curSpeedX) + (right * oppositeJumpForce);
                    oppositeJumpForce -= oppositJumpForceReductionSpeed * Time.deltaTime;
                }
                else
                {
                    moveDirection = (forward * curSpeedX) + (right * curSpeedY);
                    oppositeJumpForce = 0f;
                }
            else
                if (oppositeJumpForce < 0f)
            {
                moveDirection = (forward * curSpeedX) + (right * oppositeJumpForce);
                oppositeJumpForce += oppositJumpForceReductionSpeed * Time.deltaTime;
            }
            else
            {
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);
                oppositeJumpForce = 0f;
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

                if (Input.GetKeyDown("space") && canMove && wallRunning)
                {
                    numberOfJumps = 1;
                    moveDirection.y = jumpSpeed;

                    oppositeJumpForce = curSpeedY * -1;

                    if (oppositeJumpForce > 0)
                        isLeftForce = false;
                    else
                        isLeftForce = true;
                }

                    if (Input.GetKeyDown("space") && canMove && numberOfJumps > 0 && (!wallRunning))
                    {
                        numberOfJumps--;
                        moveDirection.y = jumpSpeed;
                    }
            }

            //update UI
            this.GetComponent<UIcontroller>().updateSpeedText(characterController.velocity);

    characterController.Move(moveDirection* Time.deltaTime);

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
     */
}
