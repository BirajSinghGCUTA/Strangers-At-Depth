using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;

public class Bullet : MonoBehaviourPun, IPunObservable
{
    public float speed;
    public float destroyTime = 2f;
    public bool shootLeft = false;
    bool seen = false;
    public static bool shotSelf;
    public Player Owner;
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
    }
    private void Update()
    {
        if (!shootLeft)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (GetComponent<Renderer>().isVisible)
        {
            seen = true;
        }


        if (seen && !GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.tag != "Coin")
        {
            //Destroy(gameObject);
            //destroy();
            this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
        }
        //Destroy(gameObject);
        //this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
        //StartCoroutine(destroyBullet());
        //Destroy(gameObject);
        if(collision.gameObject.tag == "Player")
        {
            shotSelf = true;
            Debug.Log(shotSelf.ToString());
        }
        if(collision.gameObject.tag == "Enemy")
        {
            shotSelf = false;
            this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
            Debug.Log(shotSelf.ToString());
        }
    }


    [PunRPC]
    
    public void destroy()
    {
        Destroy(this.gameObject);
    }

    
    [PunRPC]
    public void changeDirection()
    {
        shootLeft = true;
    }
    /*void Start()
    {
        //rb.velocity = transform.right * speed;

    }

   

    private void Update()
    {

        if (GetComponent<Renderer>().isVisible)
        {
            seen = true;
        }


        if (seen && !GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }

    }*/
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            
            stream.SendNext(Bullet.shotSelf);
        }
        else
        {
            // Network player, receive data
            
            Bullet.shotSelf = (bool)stream.ReceiveNext();
        }
    }

}
