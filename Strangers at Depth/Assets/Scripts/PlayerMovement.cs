using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;
    public bool isGrounded = true;
    private float horizontalMove;
    private float verticalMove;
    public int jumpMultiplier;
    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    private bool isFacingRight = true;
    public GameObject canvasHealth;
    public GameObject canvasCoin;
    public bool disconnectflag = false;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            this.gameObject.tag = "Enemy";
        }
        else
        {
            this.gameObject.tag = "Player";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (!disconnectflag)
            {
                ConnectController();
            }
            Vector2 movement = new Vector2(horizontalMove, 0f) * Time.deltaTime * moveSpeed;
            transform.gameObject.GetComponent<Rigidbody2D>().position += movement;
            myAnim.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (horizontalMove > 0 && !isFacingRight)
            {

                Flip();

            }
            else if (horizontalMove < 0 && isFacingRight)
            {

                Flip();

            }
        }
    }

    void FixedUpdate()
    {
        Jump();
    }

    void Jump()
    {
        if (view.IsMine)
        {
            if (verticalMove > .5f && isGrounded == true)
            {
                isGrounded = false;
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.zero, ForceMode2D.Impulse);
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpMultiplier, ForceMode2D.Impulse);
            }
        }
       
    }

    protected void Flip()
    {
       
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
        canvasHealth.transform.Rotate(0f, 180f, 0f);
        canvasCoin.transform.Rotate(0f, 180f, 0f);

    }
    public void DisconnectController()
    {
        horizontalMove = 0;
        verticalMove = 0;
    }
    public void ConnectController()
    {
        horizontalMove = joystick.Horizontal;
        verticalMove = joystick.Vertical;
    }
}
