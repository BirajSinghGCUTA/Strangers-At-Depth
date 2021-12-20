using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerOrenemy : MonoBehaviour
{
    // Start is called before the first frame update
    PhotonView photonView;
   
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            gameObject.tag = "Enemy";//**NOTE: this may need to be more specific. If you have NPC's they too would have a view that may not be this clients. Which would not always make them an enemy.
        }
        else
        {
            gameObject.tag = "Player";//will only set the tag for the client running the code.
        }
        
    }
}
