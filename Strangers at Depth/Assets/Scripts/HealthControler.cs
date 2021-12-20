using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControler : MonoBehaviourPunCallbacks
{

    public int playerHealth;
    [SerializeField] private Image[] hearts;
    public Sprite fadedHeart;
    public Sprite heart;
    //private PhotonView PV;
    private void Start()
    {
        //PV = GetComponent<PhotonView>();
        //UpdateHealth();
       // PV.RPC("UpdateHealth", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void UpdateHealth()
    {
        for( int i = 0; i < hearts.Length; i++)
        {
            if(i < playerHealth)
            {
                hearts[i].sprite = heart;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }
    }

    
}

