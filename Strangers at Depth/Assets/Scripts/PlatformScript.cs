using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlatformScript : MonoBehaviour
{
    //public Transform platform;
    private Collider2D platformCollider;
    public List<GameObject> players = new List<GameObject>();
 
    //public Collider2D playerFeet;


    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<BoxCollider2D>();

        
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
        //playerFeet = GameObject.Find("Player").GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
    }
    // Update is called once per frame
    void Update()
    {
       foreach (GameObject player in players)
        {
            if (player.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                platformCollider.enabled = true;
            }
            else
            {
                platformCollider.enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.collider.tag == "Player" && player.velocity.y <= 0)
        {
            platformCollider.enabled = true;
            //Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), platformCollider);
        }*/
    }
}
