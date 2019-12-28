using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrControllerBolt : Bolt.EntityBehaviour<IPvpPlayerState>
{
    public Animator anim;

    public float speed = 1.0f ;
    public float rotationSpeed = 75.0f ;
    public float runSpeed= 5.0f ;
    public float walkSpeed= 1.0f ;

    public override void Attached()
    {
        AttachCamera();
        anim = GetComponent<Animator>();
        state.SetTransforms(state.Transform, transform);
        state.SetAnimator(anim);
    }

    private void AttachCamera()
    {
        Camera cam = Camera.main;
        CameraController camController = cam.gameObject.GetComponent<CameraController>();
        camController.AttachCamera(this);
    }

    public override void SimulateOwner()
    {
        Vector3 translatation = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        translatation.Normalize();
        // float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translatation *= BoltNetwork.FrameDeltaTime * speed;
        // rotation *= BoltNetwork.FrameDeltaTime;
        transform.position += translatation;
        // transform.Rotate(0, rotation, 0);
        Vector3 mousePos = GetMousePosOnScene();
        transform.LookAt(new Vector3(mousePos.x, this.transform.position.y, mousePos.z));


        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            if (translatation != Vector3.zero)
            {
                state.MoveState = (int)MoveState.RUN;
                // state.Animator.SetBool("Run", true);
                // state.Animator.SetBool("Walk", false);
                // state.Animator.SetBool("Idle", false);
                speed = runSpeed;
            }
        } 
        else 
        {
            if (translatation != Vector3.zero)
            {
                state.MoveState = (int)MoveState.WALK;
                // state.Animator.SetBool("Walk", true);
                // state.Animator.SetBool("Run", false);
                // state.Animator.SetBool("Idle", false);
                speed = walkSpeed;
            }

            if (translatation == Vector3.zero)
            {
                state.MoveState = (int)MoveState.IDLE;
                // state.Animator.SetBool("Idle", true);
                // state.Animator.SetBool("Run", false);
                // state.Animator.SetBool("Walk", false);
            }
        }
    }

    private void Update()
    {
        switch (state.MoveState)
        {
            case (int)MoveState.IDLE:
                state.Animator.SetBool("Idle", true);
                state.Animator.SetBool("Run", false);
                state.Animator.SetBool("Walk", false);
                break;

            case (int)MoveState.WALK:
                state.Animator.SetBool("Walk", true);
                state.Animator.SetBool("Run", false);
                state.Animator.SetBool("Idle", false);
                break;
            
            case (int)MoveState.RUN:
                state.Animator.SetBool("Run", true);
                state.Animator.SetBool("Walk", false);
                state.Animator.SetBool("Idle", false);
                break;
        }        
    }

    private Vector3 GetMousePosOnScene()
    {
        Plane plane = new Plane(Vector3.up, 0);
        Vector3 point = new Vector3(0, 0, 0);
        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            point = ray.GetPoint(dist);
        }
        return point;
    }

 }

 public enum MoveState
 {
     IDLE = 0,
     WALK = 1,
     RUN = 2,
 }
