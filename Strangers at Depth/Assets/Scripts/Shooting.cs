using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviourPunCallbacks, IPunObservable
{
    public Transform firePoint;
    public GameObject projectile;
    public float bulletSpeed;
    public LayerMask whatHit;
    Vector2 mousePosition;
    Vector2 firePointPos;
    RaycastHit2D hit;
    private PhotonView PV;
    //[SerializeField] private HealthControler _healthController;
    public Vector2 startPos;
    public SpriteRenderer sr;
    public float fireRate = 1f;
    public float lastShot = 7f;
    
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        //_healthController = GetComponent<HealthControler>();
    }
    void Update()
    {
        #region notUsed
        /*if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetButtonDown("Fire1"))
            {

                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 dir = touchPos - (new Vector2(transform.position.x, transform.position.y));
                dir.Normalize();
                Shoot();
                *//*GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;*//*
            }
        }*/
        #endregion

        if (PV.IsMine)
        {
           
            if (Input.GetButtonDown("Fire1")&& Time.time > lastShot && PauseMenu.GameisPaused == false)
            {
                //PV.RPC("RPC_Shoot", RpcTarget.All);
                lastShot = Time.time + fireRate;
                RPC_Shoot();
                   
            }
        }
       
    }

    #region notUsed
    /*void ProcessInputs()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
           *//* if (touch.phase)
            {
                if (!IsFiring)
                {
                    IsFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (IsFiring)
                {
                    IsFiring = false;
                }
            }*//*
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    //message = "Begun ";
                    if (!IsFiring)
                    {
                        IsFiring = true;
                        RPC_Shoot();
                    }
                    break;

                case TouchPhase.Ended:
                    if (IsFiring)
                    {
                        IsFiring = false;
                    }
                    break;
            }
        }
    }*/
    #endregion

    public void RPC_Shoot()
    {
        mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
       /* mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y);
        mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(startPos).x, Camera.main.ScreenToWorldPoint(startPos).y);*/
        firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        //hit = Physics2D.Raycast(firePointPos, startPos - firePointPos, 100/*, whatHit//);
        hit = Physics2D.Raycast(firePointPos, mousePosition - firePointPos, 100/*, whatHit*/);
        //Debug.DrawLine(firePointPos, (mousePosition- firePointPos) *100, Color.cyan);
        if (hit.collider != null)
        {
            //Debug.DrawLine(firePointPos, hit.point, Color.red);
            GameObject bullet = PhotonNetwork.Instantiate(projectile.name, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().Owner = PhotonNetwork.LocalPlayer;
        
            //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());


            if (sr.flipX)
            {
                bullet.GetComponent<PhotonView>().RPC("changeDirection", RpcTarget.AllBuffered);
            }


            //StartCoroutine(MyCoroutine(bullet));
            /*if (hit.collider.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<HealthControler>().playerHealth = _healthController.playerHealth - 1;
                //_healthController.UpdateHealth();
                PV.RPC("UpdateHealth", RpcTarget.AllBuffered);
            }*/
        }
    }

    IEnumerator MyCoroutine(GameObject bullet)
    {
        bullet.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(0.3f);

        bullet.GetComponent<Collider2D>().enabled = true;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
