using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnFloatingPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera mainCamera;
    public GameObject [] platformPrefabCollection;
    GameObject platform;
    public GameObject BigPlatform;

    Queue<GameObject> PlatformListtoDestroy = new Queue<GameObject>();

    private float halfHeight;
    public float halfWidth;

    public float currentMiddle;
    public float CurrentMaxY;
    public float NextMaxY;
    public float PrevMaxY;
    public bool runOnce;

    void Start()
    {
     
        halfHeight = mainCamera.orthographicSize;
        halfWidth = mainCamera.aspect * halfHeight;
        currentMiddle = 0;
        CurrentMaxY = halfHeight;
        NextMaxY = CurrentMaxY + (halfHeight * 2);
        PrevMaxY = CurrentMaxY;
        runOnce = true;

        platform = (GameObject)PhotonNetwork.InstantiateRoomObject(platformPrefabCollection[Random.Range(0, platformPrefabCollection.Length)].name, new Vector2(0, 2.0f), Quaternion.identity);
        PlatformListtoDestroy.Enqueue(platform);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentMiddle = mainCamera.transform.position.y;
        if (mainCamera.transform.position.y + halfHeight >= (PrevMaxY-2) && runOnce)
        {
            runOnce = false;
            for (float i = 0; i <= 3; i= i+1.0f)
            {
                platform = (GameObject)PhotonNetwork.InstantiateRoomObject(platformPrefabCollection[Random.Range(0, platformPrefabCollection.Length)].name, new Vector2(0, ((i * 2.5f) + PrevMaxY)), Quaternion.identity);
                PlatformListtoDestroy.Enqueue(platform);
            }

            PrevMaxY = NextMaxY;
            NextMaxY = NextMaxY + (halfHeight * 2);
            runOnce = true;
        }
        if(PlatformListtoDestroy.Peek() && PlatformListtoDestroy.Peek().transform.position.y <= ((mainCamera.transform.position.y - (halfHeight*2))))
        {
            PhotonNetwork.Destroy(PlatformListtoDestroy.Dequeue());
        }
    }
}
