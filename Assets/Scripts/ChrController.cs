﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrController : MonoBehaviour
{
    public Animator anim;

    public float speed = 1.0f ;
    public float rotationSpeed = 75.0f ;
    public float runSpeed= 5.0f ;
    public float walkSpeed= 1.0f ;

    void Start()
    {
        anim = GetComponent<Animator> ();
    }

    void Update()
    {
        float translatation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translatation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translatation);
        transform.Rotate(0, rotation, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (translatation != 0)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    anim.SetBool("Run", false);
                    anim.SetBool("Walk", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("FRWalk", false);
                    anim.SetBool("FRRun", true);
                    anim.SetBool("Dead", false);
                    speed = runSpeed;
                }

                else
                {
                    anim.SetBool("Run", true);
                    anim.SetBool("Walk", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("FRWalk", false);
                    anim.SetBool("FRRun", false);
                    anim.SetBool("Dead", false);
                    speed = runSpeed;
                }
            }



        }
        else
        {

            if (translatation != 0)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("FRWalk", true);
                    anim.SetBool("FRRun", false);
                    anim.SetBool("Dead", false);
                    speed = walkSpeed;
                }
                else
                {
                    anim.SetBool("Walk", true);
                    anim.SetBool("Run", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("FRWalk", false);
                    anim.SetBool("FRRun", false);
                    anim.SetBool("Dead", false);
                    speed = walkSpeed;
                }
            }

            if (translatation == 0)
            {
                anim.SetBool("Idle", true);
                anim.SetBool("Run", false);
                anim.SetBool("Walk", false);
                anim.SetBool("FRWalk", false);
                anim.SetBool("FRRun", false);
                anim.SetBool("Dead", false);
            }
        }
    }
}
