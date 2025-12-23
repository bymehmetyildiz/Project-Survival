using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public StateMachine stateMachine { get; private set; }

    //States
    public IdleState idleState { get; private set; }
    public MoveState moveState { get; private set; }
    public WorkState workState { get; private set; }
    public PickUpState pickUpState { get; private set; }
    public PutDownState putDownState { get; private set; }
    public CarryIdleState carryIdleState { get; private set; }
    public CarryWalkState carryWalkState { get; private set; }

    [Header("Component")]
    public CharacterController cc;
    public Animator animator;

    [Header("Movement")]
    public float turnSpeed;
    public float moveSpeed;
    public float verticalVelocity;
    [SerializeField] private FloatingJoystick joystick;

    [Header("Recources Actions")]
    [SerializeField] private float detectRadius;    
    public int carryCapacity = 1;
    public bool isBusyCarrying;

    [Header("Tools")]
    public GameObject woodAxe;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        stateMachine = new StateMachine();

        //States
        idleState = new IdleState(stateMachine, "Idle", cc, this);
        moveState = new MoveState(stateMachine, "Move", cc, this);
        workState = new WorkState(stateMachine, "Work", cc, this);
    }

    void Start()
    {
        stateMachine.Initialize(idleState);

        woodAxe.SetActive(false);
    }

    
    void Update()
    {
        stateMachine.currentState.Update();
    }

    #region Movement

    public void ApplyGravity()
    {
        if (!cc.isGrounded)
            verticalVelocity -= 9.81f * Time.deltaTime;
        else
            verticalVelocity = -0.5f;

        cc.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    public Vector3 Movement()
    {
        Vector3 horizontal = Vector3.zero;

        if (joystick.Direction.magnitude >= 0.2f)
        {
            horizontal = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
            horizontal.Normalize();
        }
        else if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
        {
            horizontal = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            horizontal.Normalize();
        }
        ApplyGravity();

        Vector3 finalMove = horizontal * moveSpeed + Vector3.up * verticalVelocity;

        if (horizontal.magnitude != 0 || joystick.input.magnitude >= 0.2f)
        {
            Quaternion rotation = Quaternion.LookRotation(horizontal);

            if (horizontal.magnitude > 0)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }

        return finalMove;
    }


    public bool IsMoving()
    {
        return joystick.Direction.magnitude >= 0.2f || 
               Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || 
               Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0;        
    }
    #endregion

    //Nearest Resource Detection
    public Resource NearestRecource()
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

    public bool CanInteractWitResource()
    {
        if (NearestRecource() != null 
            && !IsMoving())
            return true;

        return false;
    }

    /*
    //Nearest Collectible Resource Detection
    public Collectible NearestCollectibleResource()
    {
        Collectible nearestCollectible = null;
        float minDist = Mathf.Infinity;
        Collider[] hits = Physics.OverlapSphere(transform.position, detectRadius);
        foreach (Collider hit in hits)
        {
            Collectible col = hit.GetComponent<Collectible>();
            if (col != null)
            {
                float dist = Vector3.Distance(transform.position, col.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestCollectible = col;
                }
            }
        }
        return nearestCollectible;
    }

    public bool CanCollectResource()
    {
        if (NearestCollectibleResource() != null
            && !IsMoving()
            && !isBusyCarrying)
            return true;
        return false;
    }
    */

    //Animation Events
    public void InteractNearestResource()
    {
        if(NearestRecource() != null)
        {
            NearestRecource().Interact(this);
            workState.UnTriggerAnimation();
        }
    }

    public void TriggerCalled() => workState.TriggerAnimation();

}
