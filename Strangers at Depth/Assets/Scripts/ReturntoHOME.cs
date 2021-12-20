using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ReturntoHOME : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //not for popup return to hs
    public void OnTerminateClick()
    {
        PhotonNetwork.Disconnect(); 
        SceneManager.LoadScene("HomeScreenScene");
    }

    //PopUpAd
    public int time = 0;
    void FixedUpdate()
    {
        if (!Input.anyKey)
        {
            time = time + 1;
        }
        else
        {
            time = 0;
        }
        //50 frames per second
        if (time == 350)
        {
            Debug.Log("350 frames passed with no input load ad");
            SceneManager.LoadScene("Advertisement", LoadSceneMode.Additive);
            //if (SceneManager.GetActiveScene().name != "Advertisement")
        }

    }
}
