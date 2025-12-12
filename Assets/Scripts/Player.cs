using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;
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
        if(IsMovingWithJoystick())
            MoveWithJoystick();
        else
            MoveWithKeyboard();

        AnimateMovement();
    }


    #region Movement
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

    private void MoveWithKeyboard()
    {
        Vector3 horizontal = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        horizontal.Normalize();
        ApplyGravity();

        Vector3 finalMove = horizontal * moveSpeed + Vector3.up * verticalVelocity;
        cc.Move(finalMove * Time.deltaTime);

        if (horizontal.magnitude != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(horizontal);

            if (horizontal.magnitude > 0)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }

           
    }

    private bool IsMoving()
    {
        return joystick.Direction.magnitude >= 0.2f || 
               Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || 
               Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0;        
    }

    private bool IsMovingWithJoystick()
    {
        return joystick.Direction.magnitude >= 0.2f;
    }

    //private void AnimateMovement() => animator.SetBool("IsMoving", IsMoving());

    private void AnimateMovement()
    {
        float xVelocity = Vector3.Dot(cc.velocity.normalized, transform.right);
        float zVelocity = Vector3.Dot(cc.velocity.normalized, transform.forward);

        animator.SetFloat("xVelocity", xVelocity, 0.1f, Time.deltaTime);
        animator.SetFloat("zVelocity", zVelocity, 0.1f, Time.deltaTime);
    }



    #endregion

}
