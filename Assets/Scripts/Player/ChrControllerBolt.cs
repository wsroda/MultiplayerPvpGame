using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class ChrControllerBolt : Bolt.EntityBehaviour<IPvpPlayerState>
{
    public Animator anim;

    public float speed = 1.0f ;
    public float runSpeed = 5.0f ;
    public float walkSpeed = 1.0f ;
    
    private float _verticalInput;
    private float _horizontalInput;
    private bool _shiftKey;

    private MoveState moveState;

    private Vector3 localPosition;


    private void Start()
    {
        AttachCamera();
    }
    public override void Attached()
    {        
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

    void PollKeys()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _shiftKey = Input.GetKey(KeyCode.LeftShift);
    }

    public override void SimulateController()
    {
        PollKeys();

        IPvpPlayerCommandInput input = PvpPlayerCommand.Create();

        input.Vertical = _verticalInput;
        input.Horizontal = _horizontalInput;
        input.Shift = _shiftKey;
        input.MousePosition = GetMousePosOnScene();

        entity.QueueInput(input);
        
    }

    public override void ExecuteCommand(Command command, bool resetState)
    {
        PvpPlayerCommand cmd = (PvpPlayerCommand)command;

        if (resetState)
        {
            localPosition = cmd.Result.Position;
        }        
        else
        {
            Vector3 translatation = new Vector3(cmd.Input.Horizontal, 0f, cmd.Input.Vertical);
            translatation.Normalize();
            

            if (cmd.Input.Shift)
            {
                speed = runSpeed;
            }
            else
            {
                speed = walkSpeed;
            }
            translatation *= BoltNetwork.FrameDeltaTime * speed;
            float magnitude = translatation.magnitude;
            Vector3 lookPoint = new Vector3 (cmd.Input.MousePosition.x, transform.position.y, cmd.Input.MousePosition.z);
            transform.LookAt(lookPoint);
            transform.position += translatation;

            if (magnitude > 0f)
            {
                moveState = cmd.Input.Shift ? moveState = MoveState.RUN : moveState = MoveState.WALK;
            }
            else
            {
                moveState = MoveState.IDLE;
            }

            localPosition = transform.position;
            cmd.Result.Position = localPosition;
        }
    }

    void AnimatePlayer(PlayerCommand cmd)
    {

    }

    private void Update()
    {
        switch (moveState)
        {
            case MoveState.IDLE:
                state.Animator.SetBool("Idle", true);
                state.Animator.SetBool("Run", false);
                state.Animator.SetBool("Walk", false);
                break;

            case MoveState.WALK:
                state.Animator.SetBool("Walk", true);
                state.Animator.SetBool("Run", false);
                state.Animator.SetBool("Idle", false);
                break;
            
            case MoveState.RUN:
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
