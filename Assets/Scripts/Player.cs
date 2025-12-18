using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Component")]
    private CharacterController cc;
    private Animator animator;

    [Header("Movement")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;
    private float verticalVelocity;
    [SerializeField] private FloatingJoystick joystick;

    [Header("Recources Actions")]
    [SerializeField] private float detectRadius;


    

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

        Animation();
        Mining();
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

    private void Animation()
    {
        float xVelocity = Vector3.Dot(new Vector3(cc.velocity.x, 0, cc.velocity.z).normalized, transform.right);
        float zVelocity = Vector3.Dot(new Vector3(cc.velocity.x, 0, cc.velocity.z).normalized, transform.forward);

        animator.SetFloat("xVelocity", xVelocity, 0.1f, Time.deltaTime);
        animator.SetFloat("zVelocity", zVelocity, 0.1f, Time.deltaTime);

        animator.SetBool("DoMining", CanInteractWitResource());

    }



    #endregion

    private void Mining()
    {
        if (NearestRecource() != null && !IsMoving())
        {
            // Rotate smoothly toward the closest resource
            Vector3 direction = (NearestRecource().transform.position - transform.position).normalized;
            direction.y = 0; // prevent tilting up/down

            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }
   

    private Resource NearestRecource()
    {
        Resource nearestRecource = null;
        float minDist = Mathf.Infinity;

        Collider[] hits = Physics.OverlapSphere(transform.position, detectRadius);

        foreach (Collider hit in hits)
        {
            Resource res = hit.GetComponent<Resource>();
            if (res != null)
            {
                float dist = Vector3.Distance(transform.position, res.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    nearestRecource = res;
                }
            }
        }

        return nearestRecource;
    }

    private bool CanInteractWitResource()
    {
        if (NearestRecource() != null && !IsMoving())
            return true;

        return false;

    }
    
    public void CollectNearestResource()
    {
        if(NearestRecource() != null)
        {
            NearestRecource().Interact(this);
        }
    }


}
