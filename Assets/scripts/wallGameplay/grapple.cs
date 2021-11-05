using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grapple : MonoBehaviour
{
    [Header("Settings")]
    public float maxDistance = 100f;
    public float grappleSpeed = 25;
    public float grappleYforce = 10f;
    public float minDistanceDetach = 5f;
    public float speedReduction = 5f;
    [Space,Header("Game Objects & Conponents")]
    public GameObject grapplePoint;
    [Space]
    public Camera camera;
    public LayerMask mask;
    [Space,Header("UI")]
    public Animator anim;
    public Slider timeToGrapple;

    LineRenderer lr;
    CharacterController chaController;
    SC_FPSController plaController;
    wallJump WallJump;
    float initialTimeToGrapple;
    float initialYforce;
    bool hooked = false;
    RaycastHit hit;
    float initialGravity;
    float initialGrappleSpeed;
    Vector3 grappleDir;
    float a = 0;

    private void Start()
    {
        chaController = transform.GetComponent<CharacterController>();
        plaController = transform.GetComponent<SC_FPSController>();
        WallJump = transform.GetComponent<wallJump>();
        lr = transform.GetComponent<LineRenderer>();

        initialGravity = plaController.gravity;
        initialYforce = grappleYforce;
        initialGrappleSpeed = grappleSpeed;
        initialTimeToGrapple = timeToGrapple.value;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (timeToGrapple.value == initialTimeToGrapple))
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance, mask))
            {
                WallJump.canMove = false;
                grapplePoint.transform.position = hit.point;
                plaController.gravity = 0;
        
                grappleYforce = initialYforce;
                grappleSpeed = initialGrappleSpeed;
        
                lr.positionCount = 2;
                timeToGrapple.value = 0;
        
                hooked = true;
            }
        if (Input.GetMouseButtonUp(0))
        {
            detach();
        }
        
        if (timeToGrapple.value < initialTimeToGrapple)
            timeToGrapple.value += Time.deltaTime;
        
        if (hooked)
        {
            grappleDir = (grapplePoint.transform.position - transform.position).normalized;
        
            grappleDir = new Vector3(grappleDir.x, grappleDir.y + grappleYforce, grappleDir.z);
        
            if (grappleYforce > 0)
                grappleYforce -= Time.deltaTime;
            else
                grappleYforce = 0;
        
            grappleSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hit.point), 5f, 10f);
        
            chaController.Move(grappleDir * grappleSpeed * 2f * Time.deltaTime);
        
            a = grappleSpeed;
            timeToGrapple.value = 0;
        
            if (Vector3.Distance(transform.position, hit.point) < minDistanceDetach)
                detach();
        }
        else if (a > 0)
        {
            chaController.Move(transform.TransformDirection(Vector3.forward) * a * 2f * Time.deltaTime);
            a -= Time.deltaTime * 10f;
        }
        else
            a = 0f;
        
        
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, maxDistance, mask))
            anim.Play("nearToGrapple");
        else anim.Play("farToGrapple");
        
    }

    void detach()
    {
        hooked = false;
        plaController.gravity = initialGravity;
        WallJump.canMove = true;
    }

    private void LateUpdate()
    {
        if (hooked)
        {
            drawBall();
            drawRope();
        }
        else
            lr.positionCount = 0;
    }
    void drawBall()
    {
        grapplePoint.transform.position = hit.point;
    }
    void drawRope()
    { 
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, grapplePoint.transform.position);
    }
}

