using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;


public class PlayerMovement : MonoBehaviour
{
    public float gravityModifier, jumpPower;
    [SerializeField] float moveSpeed = 1.0f;
    //[SerializeField] Vector3 playerVelocity;
    //[SerializeField] float gravityValue = -10f;
    //[SerializeField] float groundDistance = 0.2f;
    //[SerializeField] float jumpHeight = 10f;
    public Vector3 MoveVector { get; set; }
    public Vector3 dirMove;

    public VirtualJoystick joystick;
    public CharacterController charcon;
    
    public Transform target;
    public Transform camTrans;

   //private bool Grounded;
    public LayerMask groundMask;
    //private Transform transGroundCheckPoint;
    public Transform groundCheckPoint; 


    Animator animator;
    float animatorSpeed = 0f;
    private bool hasAnimator;

    private bool canJump;


    void Start()
    {
        hasAnimator = TryGetComponent(out animator);
        charcon = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();    
        //transGroundCheckPoint = transform;
        //groundMask = (1 << 6 ) | (1 << 7);


        if(hasAnimator)
        {
            animator.applyRootMotion = false;
        }

    }


    void Update()
    {
        MovePad();
        
        //GroundedCheck();

        //Vector3 movement = MoveVector * moveSpeed *Time.deltaTime;

        //playerVelocity.y = gravityValue * Time.deltaTime;

        //charcon.Move(playerVelocity * Time.deltaTime + movement);
        
    }

    //void GroundedCheck()
    //{
    //    Grounded = Physics.Raycast(transGroundCheckPoint.position, Vector3.down, groundDistance, groundMask);
    //    if (Grounded && playerVelocity.y < 0)
    //    {
    //        playerVelocity.y = 0f;
    //
    //    }
    //}

    public void MovePad()
    {
        float ystore = MoveVector.y;

        Vector3 dir = Vector3.zero;

        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();


        dirMove = transform.forward * dir.z + transform.right * dir.x;
        dirMove.Normalize();

        dirMove.y += Physics.gravity.y * gravityModifier * Time.deltaTime;
        dirMove.y = ystore;

        if(charcon.isGrounded)
        {
            dirMove.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        Jump(dirMove.y);

        MoveVector = dirMove;
        float animatorSpeed = MoveVector.x;

        
        Vector3 movement = MoveVector * moveSpeed;
        charcon.Move(movement * Time.deltaTime);

        if(dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + dir.x, transform.rotation.eulerAngles.z);
        }
        
        if(hasAnimator)
        {
            animator.SetFloat("Speed", animatorSpeed);
            //animator.SetFloat("MotionSpeed", 1f);
        }
    }
    public void Jump(float jumpForce)
    {
        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, groundMask).Length > 0;
        if (canJump)
        {
            dirMove.y = Mathf.Lerp(0, jumpForce, Time.deltaTime);
            Debug.Log("jump");
        }  


       //if (hasAnimator)
       //{
       //    animator.SetTrigger("Jump");
       //}

    }
}
