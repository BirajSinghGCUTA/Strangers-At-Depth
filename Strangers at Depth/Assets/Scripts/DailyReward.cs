using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class DailyReward : MonoBehaviour
{

    private int krakens;
    private int skins;
    private int wins;
    public int krakensToAdd;
    public System.DateTime date;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    public Text claimText;
    public Button collectButton;
    public Image winsAchieve;
    public Image skinsAchieve;
    public Image krakensAchieve;

    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        //Button btn = collectButton.GetComponent<Button>();
        collectButton.onClick.AddListener(TaskOnClick);
        //krakens = 0;

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
                Debug.Log("Snapshot " + snapshot.Value);
                //krakensHeld.text = snapshot.Value.ToString();
                //krakensHeld.gameObject.SetActive(true);
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });

        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Daily Reward").GetValueAsync().ContinueWith(task =>
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
                date = System.DateTime.Parse((string)snapshot.Value);
                //krakensHeld.text = snapshot.Value.ToString();
                //krakensHeld.gameObject.SetActive(true);
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });

        Debug.Log("Krakens from first read " + krakens.ToString());
        Debug.Log("Date from first read " + date.ToString());
    }

   /* void Update()
    {
        auth = FirebaseAuth.DefaultInstance;
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
        }
    }*/

    void TaskOnClick()
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
               Debug.Log("Snapshot " + snapshot.Value);
               //krakensHeld.text = snapshot.Value.ToString();
               //krakensHeld.gameObject.SetActive(true);
               // Do something with snapshot...
           }
           else
           {
               Debug.Log("Not Working");
           }
       });

        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Daily Reward").GetValueAsync().ContinueWith(task =>
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
                date = System.DateTime.Parse((string)snapshot.Value);
                //krakensHeld.text = snapshot.Value.ToString();
                //krakensHeld.gameObject.SetActive(true);
                // Do something with snapshot...
            }
            else
            {
                Debug.Log("Not Working");
            }
        });

        Debug.Log("Krakens from read " + krakens.ToString());
        Debug.Log("Date from read " + date.ToString());

        //int.TryParse(krakensHeld.text, out krakens);
        if ((int)date.Day < (int)System.DateTime.Now.Day)
        {
            //Debug.Log("Krakens before add " + krakens.ToString());
            krakens += krakensToAdd;
            addKraken(krakens);
            // pop up text saying rewards have been claimed successfully
            //claimText.color = "green";
            //claimText.text = "Reward Claimed Successfully!";
            StartCoroutine(textWait("Reward Claimed Successfully!", Color.green));
        }
        else
        {
            //Debug.Log("Else Krakens " + krakens.ToString());
            // pop up text saying rewards have already been claimed for the day
            //claimText.color = "red";
            //claimText.text = "Reward has already been claimed for today";
            StartCoroutine(textWait("Reward has already been claimed", Color.red));
        }
    }

    IEnumerator textWait(string text, Color textColor)
    {
        claimText.gameObject.SetActive(true);
        claimText.text = text;
        claimText.color = textColor;
        yield return new WaitForSeconds(1f);
        claimText.gameObject.SetActive(false);
    }

    private void addKraken(int krakens)
    {
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Total Krakens").SetValueAsync(krakens);
        FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Daily Reward").SetValueAsync(System.DateTime.Now.ToString("yyyy/MM/dd"));

        // test
        /*FirebaseDatabase.DefaultInstance.GetReference($"/users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/Total Krakens").GetValueAsync().ContinueWith(task =>
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

        Debug.Log("Krakens after read " + krakens.ToString());*/
    }
}
