using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Database;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private IEnumerator LoadScoreboardData()
    {
        var DBTask = DBreference.Child("users").OrderByChild("Total Wins").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed tp register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            // Wipe the existing scoreboard
            foreach(Transform child in scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach(DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int wins = int.Parse(childSnapshot.Child("Total Wins").Value.ToString());
                int krakens = int.Parse(childSnapshot.Child("Total Krakens").Value.ToString());

                GameObject scoreboardElement = Instantiate(scoreboardElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElemen(username, wins, krakens);
            }

            UIManager.instance.ScoreboardScreen();
        }
    }*/
}
