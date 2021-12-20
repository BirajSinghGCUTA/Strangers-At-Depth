using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;
    public Button pauseButton;
    public GameObject pauseMenuUI;
    /*public static GameObject  endgameUI;
    public TextMeshProUGUI winText;
    public static string info;*/
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameisPaused = false;
        pauseButton.gameObject.SetActive(true);
        
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameisPaused = true;
        pauseButton.gameObject.SetActive(false);
       
    }

    public void Lobby()
    {
        Debug.Log("Lobby");
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom) return;
        SceneManager.LoadScene("LobbyScreen");
    }
    public void Quit()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }

   /* public void EndGame()
    {
        endgameUI.GetComponentInChildren<TextMeshProUGUI>().text = info;
        endgameUI.SetActive(true);
    }*/
}
