using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;
    public GameObject pauseGame;
    // Start is called before the first frame update
    private void Start()
    {
        pauseGame = GameObject.Find("PauseGame");
        StartCoroutine(CountdownToStart(countdownTime));
    }

    IEnumerator CountdownToStart(int seconds)
    {
        Debug.Log("Starting Countdown");
        while(seconds > 0)
        {
            countdownDisplay.text = seconds.ToString();
            yield return new WaitForSeconds(1f);
            Debug.Log("" + seconds);
            seconds--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1f);
        pauseGame.gameObject.SetActive(false);
    }
}
