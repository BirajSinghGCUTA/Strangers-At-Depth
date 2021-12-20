using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class Leaderboard : MonoBehaviour
{

    List<ScoreElement> scoreList = new List<ScoreElement>();
    public ScoreElement scorePrefab;
    public Transform contentObject;
    public string username;
    public int krakens;
    public int wins;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    public DataSnapshot snapshot;
    //var DBTask;

    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        //var DBTask = DBreference.Child("users").OrderByChild("Total Wins").GetValueAsync();
        DBWait();
        //yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        Debug.Log("Snapshot size" + snapshot.ChildrenCount.ToString());

        foreach (DataSnapshot childSnapshot in snapshot.Children)
        {
            username = childSnapshot.Child("username").Value.ToString();
            wins = (int)childSnapshot.Child("Total Wins").Value;
            krakens = (int)childSnapshot.Child("Total Krakens").Value;

            ScoreElement newScore = Instantiate(scorePrefab, contentObject);
            newScore.NewScoreElement(username, wins, krakens);
            scoreList.Add(newScore);
        }
    }

    IEnumerator DBWait()
    {
        var DBTask = DBreference.Child("users").OrderByChild("Total Wins").GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        snapshot = DBTask.Result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void UpdateBoardList(List<ScoreElement> list)
    {
        foreach (ScoreElement item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        scoreList.Clear();

        foreach (ScoreElement room in list)
        {
            ScoreItem newScore = Instantiate(scorePrefab, contentObject);
            newScore.NewScoreElement(username, wins, krakens);
            scoreList.Add(newScore);
        }
    }*/
}
