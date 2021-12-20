using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
public class ScoreManager : MonoBehaviourPunCallbacks, IPunObservable
{

    //public static ScoreManager instance;
    public int value = 1;
    public TextMeshProUGUI text;
    public TextMeshProUGUI healthtext;
    int score;

    private Rigidbody2D myRigidBody;

    public float sec = 5f;

    public Joystick joystick;

    private float curHealth = 200;
    private float minHealth = 0;
    private float maxHealth = 200;
    PlayerMovement playermovement;
    public  int currentPlayersIN = 0;
    public int startingPlayers = 0;
    public int position = 0;
    private bool isWinner = false;
    GameObject coin;
    public GameObject endUI;
    //public float damageTaken = 20;
    // Start is called before the first frame update

    void Start()
    {
        joystick = GetComponent<PlayerMovement>().joystick;
        playermovement = gameObject.GetComponent<PlayerMovement>();
        startingPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        currentPlayersIN = startingPlayers;
        Debug.Log(startingPlayers.ToString());
        Debug.Log(isWinner.ToString());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            //if (photonView.IsMine)
            //{
                coin = collision.gameObject;
                Destroy(coin);
                ChangeScore(value);

            //}

        }
        StartCoroutine(MyCoroutine(collision));
    }

    private bool condition(Collider2D collision)
    {
        Player owner = collision.gameObject.GetComponent<Bullet>().Owner;
        return (owner != PhotonNetwork.LocalPlayer);
    }

    IEnumerator MyCoroutine(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (condition(collision))
            {
                gameObject.GetComponent<Animator>().Play("PlayerHurt");
                if (photonView.IsMine)
                {
                    photonView.RPC("Damage", RpcTarget.AllBuffered);
                  
                    playermovement.disconnectflag = true;
                    playermovement.DisconnectController();

                    yield return new WaitForSeconds(sec);    //Wait one frame

                    
                    playermovement.disconnectflag = false;
                    playermovement.ConnectController();
                }
            }
            else
            {
                if(gameObject.tag == "Player")
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),collision);
                }
                if(gameObject.tag == "Enemy")
                    gameObject.GetComponent<Animator>().Play("PlayerHurt");
            }
        }


    }



    [PunRPC]
    void Damage()
    {
        curHealth -= 20;
        /*if (curHealth == minHealth)
        {
            Die();
            Debug.Log("placed " + position.ToString() + '/' + startingPlayers.ToString());
            Debug.Log(isWinner.ToString());
            //FindObjectOfType<PauseMenu>().EndGame();
            EndGame.info = "placed " + position.ToString() + '/' + startingPlayers.ToString();
            Debug.Log("Damage Pinged");
            FindObjectOfType<EndGame>().EndGamePanel();

        }*/
    }
    void Update(){

        if(joystick == null)
        {
            joystick = GetComponent<PlayerMovement>().joystick;
        }
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth < minHealth)
        {
            curHealth = minHealth;
        }
        if (curHealth == minHealth)
        {
            Die();

        }
        /*if (currentPlayersIN == 1 && isalive)
        {
            
            isWinner = true;
            Debug.Log("is winner pinged" + isWinner.ToString());
            position = currentPlayersIN;
        }
        else
        {
            Debug.Log("is winner pinged" + isWinner.ToString());
            isWinner = false;
        }

        if (isWinner)
        {
            EndGame.info = "WINNER!";
            FindObjectOfType<EndGame>().EndGamePanel();
        }*/
        
    }

    public void Die()
    {
        /*if(GetComponent<PhotonView>().InstantiationId == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }*/
        
        position = currentPlayersIN;
        currentPlayersIN -= 1;
        photonView.RPC("RPC_UpdatePlayerCount", RpcTarget.AllBuffered, currentPlayersIN);
        this.gameObject.SetActive(false);
        //EndGame.info = "YOU PLACED " + position.ToString() + '/' + startingPlayers.ToString();
        //PauseMenu.EndGame();
        //endUI.SetActive(true);
        //FindObjectOfType<EndGame>().EndGamePanel();
        Debug.Log("Die Pinged");
        //PhotonNetwork.LeaveRoom();
        EndGame.info = "YOU PLACED  " + position.ToString() + '/' + startingPlayers.ToString();
        Debug.Log("Winner Pinged");
        FindObjectOfType<EndGame>().EndGamePanel();
    }

    [PunRPC]

    public void RPC_UpdatePlayerCount(int playerCountSyncInt)

    {
        currentPlayersIN = playerCountSyncInt;
        //playerCountText.text = playerCount.ToString();
    }

    void FixedUpdate()
    {
        healthtext.text = curHealth.ToString();
    }
    public void ChangeScore(int coinValue)
    {
       
            score += coinValue;
            text.text = score.ToString();
        
           
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(score);
            stream.SendNext(text.text);
            stream.SendNext(curHealth);
            
        }
        else
        {
            // Network player, receive data
            score = (int)stream.ReceiveNext();
            text.text = (string)stream.ReceiveNext();
            curHealth = (float)stream.ReceiveNext();
			
        }
    }

   
}
