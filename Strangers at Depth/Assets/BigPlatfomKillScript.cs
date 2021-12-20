using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BigPlatfomKillScript : MonoBehaviour
{
    //List<GameObject> players = new List<GameObject>();
    public Camera mainCamera;

    public float currentMiddle ;


    public float staticMaxY;
    public bool isTriggerd = false;

    void FixedUpdate()
    {
     /*   if (PhotonNetwork.CurrentRoom.PlayerCount != players.Length)
        {
            //players = GameObject.FindGameObjectsWithTag("Player");

        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(currentMiddle > (staticMaxY + 10))
        {
            collision.transform.root.gameObject.GetComponent<ScoreManager>().Die();
            isTriggerd = true;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        currentMiddle = 0;
        staticMaxY = 0.875532f;
    }

    // Update is called once per frame
    void Update()
    {
        currentMiddle = mainCamera.transform.position.y;
    }
}
