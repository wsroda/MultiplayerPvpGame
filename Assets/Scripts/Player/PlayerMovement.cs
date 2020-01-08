using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Bolt.EntityBehaviour<IPlayerCubeState>
{



    [SerializeField]
    private float speed = 4f;

    [SerializeField]
    private Animator playerAnimator;

    public float Speed { get => speed; set => speed = value; }

    public Animator PlayerAnimator { get => playerAnimator; set => playerAnimator = value; }

    public override void Attached()
    {
        state.SetTransforms(state.CubeTransform, transform);
        state.SetAnimator(PlayerAnimator);
    }

    public override void SimulateOwner()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        float shoot = Input.GetAxis("Fire1");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + movement * Speed * BoltNetwork.FrameDeltaTime;
            state.IsMoving = true;
        }
        else
        {
            state.IsMoving = false;
        }
    }

    private void Update()
    {
        if (state.IsMoving == true)
        {
            state.Animator.Play("PlayerMoveAnimation");
        }
        else
        {
            state.Animator.Play("PlayerIdleAnimation");
        }
    }



}
