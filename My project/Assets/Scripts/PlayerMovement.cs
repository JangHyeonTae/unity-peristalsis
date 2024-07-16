using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] Vector3 playerVelocity;
    [SerializeField] float gravityValue = -10f;
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] float jumpHeight = 10f;
    public Vector3 MoveVector { set; get; }

    public VirtualJoystick joystick;
    public CharacterController charcon;
    
    public Transform target;
    public Transform camTrans;

    private bool isGrounded;
    private LayerMask groundMask;
    private Transform transGroundCheckPoint;


    Animator animator;
    private bool hasAnimator;

    //private int _animIDSpeed;
    //private int _animIDJump;

    [SerializeField] float SpeedChageRate = 10f;

    public bool jumpbool;

    void Start()
    {
        hasAnimator = TryGetComponent(out animator);
        charcon = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();    
        transGroundCheckPoint = transform;
        groundMask = (1 << 6 ) | (1 << 7);
        if (hasAnimator)
        {
            animator.applyRootMotion = false;
        }

    }

   //private void AssignAnimationIDs()
   //{
   //    _animIDSpeed = Animator.StringToHash("Speed");
   //    _animIDJump = Animator.StringToHash("Jump");
   //}

    void Update()
    {

        MovePad();

        GroundedCheck();

        //if (isGrounded && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //
        //}
        //else
        //{
        //    playerVelocity.y = gravityValue * Time.deltaTime;
        //}

        Vector3 movement = MoveVector * moveSpeed * Time.deltaTime;

        charcon.Move(playerVelocity * Time.deltaTime + movement);

        
    }

    void GroundedCheck()
    {
        isGrounded = Physics.Raycast(transGroundCheckPoint.position, Vector3.down, groundDistance, groundMask);
        
    }

    public void MovePad()
    {
        Vector3 dir = Vector3.zero;

        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();


        Vector3 dirMove = transform.forward * dir.z + transform.right * dir.x;
        
        MoveVector = dirMove;
        float animatorSpeed = MoveVector.magnitude;

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + dir.x, transform.rotation.eulerAngles.z);
        }


        if (hasAnimator)
        {
            //animator.SetFloat(_animIDSpeed, animatorSpeed);
            animator.SetFloat("Speed", animatorSpeed);
        }
    }
    public void Jump()
    {

        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f* gravityValue);
       

        
        //if (isGrounded)
        //{
        //    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        //}   

        if (hasAnimator)
        {
            animator.SetTrigger("Jump");
        }

    }

}
