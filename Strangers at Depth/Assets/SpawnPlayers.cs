using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class SpawnPlayers : MonoBehaviour
{
   public GameObject[] playerPrefabs;

   public GameObject playerPrefab;
   public float minX;
   public float minY;
   public float maxX;
   public float maxY;

   public Camera mainCamera;

   public Joystick joystick;
   public GameObject[] Platforms;
   public backgroundLoop bgloop;

   // Start is called before the first frame update
   void Start()
   {
       Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));


       GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];

       PlayerMovement playermovement = playerToSpawn.GetComponent<PlayerMovement>();
       playermovement.joystick = joystick;

       PhotonNetwork.Instantiate(playerToSpawn.name, randomPosition, Quaternion.identity);

       //GameObject Player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
   }



   // Update is called once per frame
   void Update()
   {

   }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;




public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    public Camera mainCamera;

    public Joystick joystick;
    public GameObject[] Platforms;
    public backgroundLoop bgloop;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        PlayerMovement playermovement = playerPrefab.GetComponent<PlayerMovement>();
        playermovement.joystick = joystick;

        GameObject Player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    { 

    }
}
*/
