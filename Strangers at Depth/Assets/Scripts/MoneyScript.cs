using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoneyScript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update


    public int value = 1;
    //private KrakensHeld krakensHeld;
    void Start()
    {
        //krakensHeld = FindObjectOfType<KrakensHeld>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
            //if (photonView.IsMine)
            {
            //krakensHeld.AddKraken(value);

            //gameObject.SetActive(false);
            //this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
            /*if (photonView.IsMine)
            {
                ScoreManager.instance.ChangeScore(value);
            }*/
            //ScoreManager.instance.ChangeScore(value);
        }

    }

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }*/
    [PunRPC]

    public void destroy()
    {
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
}
