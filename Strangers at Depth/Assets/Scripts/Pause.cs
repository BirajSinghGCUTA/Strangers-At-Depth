using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public int countdownTime;
    public Text countdownDisplay;
    public Text timer;
    private float startTime;
    //public GameObject spawnKraken;
    //public GameObject pauseGame;
    private void Start()
    {
        StartCoroutine(PauseGame());
        startTime = Time.time;
        //spawnKraken = GameObject.Find("spawnKraken");
        //pauseGame = GameObject.Find("PauseGame");
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        Debug.Log("Starting Countdown");
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);
        ResumeGame();
        countdownDisplay.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timer.text = minutes + ":" + seconds;

        /*if (string.Equals(minutes, "0") && string.Equals(seconds,"3.00"))
        {
            countdownDisplay.text = "KRAKEN ALERT!";
            
        }

        if (string.Equals(minutes, "0") && string.Equals(seconds, "6.00"))
        {
            countdownDisplay.text = "";
            //KrakenSpawn();

        }*/

    }

    IEnumerator PauseGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    /*void KrakenSpawn()
    {
        spawnKraken.SetActive(true);
    }*/

}
