using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Grounded : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D playerVelocity;
    // Start is called before the first frame update
    PhotonView view;
    void Start()
    {
        //Player = gameObject.transform.parent.gameObject;
        view = Player.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (view.IsMine)
        {
            if (collision.collider.tag == "Ground" && playerVelocity.velocity.y <= 0)
            {
                Player.GetComponent<PlayerMovement>().isGrounded = true;
            }
        }
    }


    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<PlayerMovement>().isGrounded = false;
        }
    }*/
}
