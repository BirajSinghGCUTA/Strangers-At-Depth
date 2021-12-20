using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public bool rushing = false;
    public Transform top_left;
    public Transform bottom_right;
    public LayerMask ground_layer;
    private bool isGrounded;
    //float timeLeft = 2f;
    public Joystick joystick;
    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    public GameObject bubbles;
    private bool jumpUp;
    float horizontalMove = 0f;
    public int jumpMultiplier;
    public GameObject firePoint;
    [Range(0, .3f)] [SerializeField] private readonly float m_MovementSmoothing = 0.05f;
    private Vector3 m_Velocity = Vector3.zero;
    private bool isFacingRight = true;
    readonly float horizontalMoveK;
    float verticalMove;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        //facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame 
    void Update()
    {

        myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
        horizontalMove = joystick.Horizontal * moveSpeed;

        verticalMove = joystick.Vertical;
        if (verticalMove > 0.5f)
        {
            jumpUp = true;

        }
    }

 

  

    public void hurt()
    {
        if (!rushing)
        {
            gameObject.GetComponent<Animator>().Play("PlayerHurt");
        }

    }


    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(horizontalMove * 3f, myRigidBody.velocity.y);
        //myRigidBody.velocity = targetVelocity;
        myRigidBody.velocity = Vector3.SmoothDamp(myRigidBody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (horizontalMove > 0 && !isFacingRight)
        {

            Flip();

        }
        else if (horizontalMove < 0 && isFacingRight)
        {

            Flip();

        }
        isGrounded = Physics2D.OverlapArea(top_left.position, bottom_right.position, ground_layer);
        if (jumpUp && isGrounded)
        {
            jumpUp = false;
            myRigidBody.AddForce(Vector2.up * jumpMultiplier, ForceMode2D.Impulse);
            //myRigidBody.AddForce(new Vector2(0f,jumpMultiplier));


        }

    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {


        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        *//*internal new*//*
        public Collider2D collider2d;
        *//*internal new*//*
        public bool controlEnabled = true;
        float verticalMove;
        public Joystick joystick;
        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;


        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            
        
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                verticalMove = joystick.Vertical;
                //move.x = Input.GetAxis("Horizontal");
                move.x = joystick.Horizontal;
                if (jumpState == JumpState.Grounded && *//*Input.GetButtonDown("Jump")*//* verticalMove > 0.5f)
                    jumpState = JumpState.PrepareToJump;
                else if (*//*Input.GetButtonUp("Jump")*//*  verticalMove == 0)
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * 1.5f;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}*/