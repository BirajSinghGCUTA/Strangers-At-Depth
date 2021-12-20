using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerUsername : MonoBehaviourPunCallbacks, IPunObservable
{
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    //public GameObject playerName;
    public TextMeshProUGUI textName;
    public string setName;
    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        SetName();
        setName = LobbyConnect.playerName;
    }

    // Update is called once per frame
    /*void Awake()
    {
        SetName();
    }*/
    void Update()
    {
        StartCoroutine(SetName());
        
    }
    IEnumerator SetName()
    {
        yield return new WaitForSeconds(.1f);
        if (photonView.IsMine)
        {
            /*FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/username").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the error...
                    Debug.Log("Failed to load Krakens held");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    //string ss = snapshot.Child("Total Krakens").Value.ToString();
                    //Debug.Log(ss);
                    //krakensHeld.text = snapshot.Value.ToString();
                    //krakensHeld.gameObject.SetActive(true);
                    // Do something with snapshot...

                    setName = snapshot.Value.ToString();

                    //buttonText.text = "Connecting...";
                }
                else
                {
                    Debug.Log("Not Working");
                }
            });
            
            if(string.IsNullOrWhiteSpace(setName))
            {
                setName = "Name";
            }
            textName.text = setName;
            */
            textName.text = setName;
        }


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
           
            stream.SendNext(textName.text);
        }
        else
        {
            // Network player, receive data
          
            textName.text = (string)stream.ReceiveNext();
        }
    }
}
