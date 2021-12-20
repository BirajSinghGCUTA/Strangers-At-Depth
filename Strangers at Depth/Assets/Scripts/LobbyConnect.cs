using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Photon.Pun;
using TMPro;

public class LobbyConnect : MonoBehaviourPunCallbacks
{
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    public static string playerName;
    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickConnect()
    {
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/username").GetValueAsync().ContinueWith(task =>
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
                 playerName = snapshot.Value.ToString();
                PhotonNetwork.NickName = playerName;
                //buttonText.text = "Connecting...";
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.ConnectUsingSettings();
            }
            else
            {
                Debug.Log("Not Working");
            }
        });
        
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("LobbyScreen");
    }
}
