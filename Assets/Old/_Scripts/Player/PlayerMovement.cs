using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float RunSpeed ;
    [SerializeField] private float GravityStrength ;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float MoveSmoothTime;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;

    private Vector3 CurrentMoveVelocity;
    private Vector3 MoveDampVeclocity;
    private Vector3 CurrentForceVelocity;
    
    private Vector3 velocity;
    private bool isGrounded;
    private bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    //Component
    private CharacterController Controller;

    private void Awake()
    {
        Controller = transform.parent.GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckOnGround();
        Moving();
        Jumping();
    }

    private void CheckOnGround()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
    
    private void Moving()
    {
        Vector3 PlayerInput = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        };

        if (PlayerInput.magnitude > 1f)
        {
            PlayerInput.Normalize();
        }

        Vector3 MoveVector = transform.parent.TransformDirection(PlayerInput);
        float CurrentSpeed = Input.GetKey(KeyCode.CapsLock) ? WalkSpeed : RunSpeed;

        CurrentMoveVelocity = Vector3.SmoothDamp(
            CurrentMoveVelocity,
            MoveVector * CurrentSpeed,
            ref MoveDampVeclocity,
            MoveSmoothTime
            );

        Controller.Move(CurrentMoveVelocity * Time.deltaTime);
    }

    private void Jumping()
    {
        if (isGrounded )
        {
            if(Input.GetKey(KeyCode.Space) || Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                //Going up
                CurrentForceVelocity.y = JumpHeight;
            }
        }
        else
        {
            //Falling down
            CurrentForceVelocity.y -= GravityStrength * 2f * Time.deltaTime;
        }
        
        //Exectuting the jump
        Controller.Move(CurrentForceVelocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        lastPosition = transform.position;
    }
}
