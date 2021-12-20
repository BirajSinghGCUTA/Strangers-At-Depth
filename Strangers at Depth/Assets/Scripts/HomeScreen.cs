using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{

    private CanvasGroup fadeGroup;
    private readonly float fadeInSpeed = .33f;
    public GameObject confirmationPage;
    // Start is called before the first frame update
    void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    }

    /*public void OnPlayClick()
    {
        SceneManager.LoadScene("LobbyLoadScreen");
        Debug.Log("Go to Play");
    }*/

    public void OnTerminateClick()
    {
        //SceneManager.LoadScene("PlayScreenScene");
        Debug.Log("Go to Terminate");
        confirmationPage.SetActive(true);
    }
    public void OnCloseTerminate()
    {
        confirmationPage.SetActive(false);
    }

    public void OnConfirmTerminate()
    {
        Application.Quit();
    }

    public void OnProfileClick()
    {
        SceneManager.LoadScene("Profile");
        Debug.Log("Go to Profile");
    }

    public void OnLeaderboardClick()
    {
        SceneManager.LoadScene("Leaderboard");
        Debug.Log("Go to Leaderboard");
    }

    public void OnStoreClick()
    {
        SceneManager.LoadScene("ShopScreen");
        
    }

    public void OnSettingsClick()
    {
        //SceneManager.LoadScene("PlayScreenScene");
        Debug.Log("Go to Settings");
    }

}
