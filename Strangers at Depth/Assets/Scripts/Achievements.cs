using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class Achievements : MonoBehaviour
{
    public Button achievementButton;
    public GameObject achievements;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    public Image winsAchieve;
    public Image skinsAchieve;
    public Image krakensAchieve;
    private int krakens;
    private int skins;
    private int wins;

    // Start is called before the first frame update
    void Start()
    {
        achievementButton.onClick.AddListener(TaskOnClick);
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void TaskOnClick()
    {
        achievements.gameObject.SetActive(true);

        /*auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Total Krakens").GetValueAsync().ContinueWith(task =>
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
                krakens = (int)snapshot.Value;
                //krakensHeld.text = snapshot.Value.ToString();
                //krakensHeld.gameObject.SetActive(true);
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });

        Debug.Log("Krakens after read " + krakens.ToString());

        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Total Wins").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("Failed to load Wins");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //string ss = snapshot.Child("Total Krakens").Value.ToString();
                //Debug.Log(ss);
                wins = (int)snapshot.Value;
                //krakensHeld.text = snapshot.Value.ToString();
                //krakensHeld.gameObject.SetActive(true);
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Skins Owned").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("Failed to load Skins owned");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //string ss = snapshot.Child("Total Krakens").Value.ToString();
                //Debug.Log(ss);
                skins = (int)snapshot.Value;
                //krakensHeld.text = snapshot.Value.ToString();
                //krakensHeld.gameObject.SetActive(true);
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });

        Debug.Log("Wins " + wins.ToString());
        Debug.Log("Skins " + skins.ToString());
        Debug.Log("Krakens " + krakens.ToString());
        if (wins >= 5)
        {
            winsAchieve.gameObject.SetActive(true);
        }
        if (skins >= 5)
        {
            skinsAchieve.gameObject.SetActive(true);
        }
        if (krakens >= 5)
        {
            krakensAchieve.gameObject.SetActive(true);
        }*/
    }

    /*void Update()
    {
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Total Krakens").GetValueAsync().ContinueWith(task =>
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
                krakens = (int)snapshot.Value;
                //krakensHeld.text = snapshot.Value.ToString();
                //krakensHeld.gameObject.SetActive(true);
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });

        Debug.Log("Krakens after read " + krakens.ToString());

        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Total Wins").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("Failed to load Wins");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //string ss = snapshot.Child("Total Krakens").Value.ToString();
                //Debug.Log(ss);
                wins = (int)snapshot.Value;
                //krakensHeld.text = snapshot.Value.ToString();
                //krakensHeld.gameObject.SetActive(true);
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Skins Owned").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("Failed to load Skins owned");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //string ss = snapshot.Child("Total Krakens").Value.ToString();
                //Debug.Log(ss);
                skins = (int)snapshot.Value;
                //krakensHeld.text = snapshot.Value.ToString();
                //krakensHeld.gameObject.SetActive(true);
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });

        Debug.Log("Wins " + wins.ToString());
        Debug.Log("Skins " + skins.ToString());
        Debug.Log("Krakens " + krakens.ToString());
        if (wins >= 5)
        {
            winsAchieve.gameObject.SetActive(true);
        }
        if (skins >= 5)
        {
            skinsAchieve.gameObject.SetActive(true);
        }
        if (krakens >= 5)
        {
            krakensAchieve.gameObject.SetActive(true);
        }
    }*/
}
