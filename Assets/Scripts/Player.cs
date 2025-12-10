using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;
    private Vector3 movementDirection;
    private float verticalVelocity;
    [SerializeField] private FloatingJoystick joystick;
    private CharacterController cc;
    private Animator animator;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        MoveWithJoystick();
        MoveWithInput();
        AnimateMovement(cc.velocity.magnitude);
    }


    private void MoveWithJoystick()
    {
        Vector3 horizontal = Vector3.zero;

        if (joystick.Direction.magnitude >= 0.2f)
        {
            horizontal = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
            horizontal.Normalize();
        }
        
        ApplyGravity();

        Vector3 finalMove = horizontal * moveSpeed + Vector3.up * verticalVelocity;

        cc.Move(finalMove * Time.deltaTime);

        if (joystick.input.magnitude >= 0.2f)
        {
            Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            direction.Normalize();

            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        if (!cc.isGrounded)
            verticalVelocity -= 9.81f * Time.deltaTime;        
        else
            verticalVelocity = -0.5f;
    }

    private void MoveWithInput()
    {
        Vector3 horizontal = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        horizontal.Normalize();
        ApplyGravity();

        Vector3 finalMove = horizontal * moveSpeed + Vector3.up * verticalVelocity;
        cc.Move(finalMove * Time.deltaTime);

        Quaternion rotation = Quaternion.LookRotation(horizontal);

        if (horizontal.magnitude > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        
    }

    private void AnimateMovement(float movement)
    {
        //animator.SetFloat("xVelocity", x);
        //animator.SetFloat("zVelocity", z);
        animator.SetFloat("Movement", movement);
    }




}
