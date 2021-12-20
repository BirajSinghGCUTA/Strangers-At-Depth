using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject endgameUI;
    public TextMeshProUGUI winText;
    public static string info;
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

    public void EndGamePanel()
    {
        winText.text = info;
        endgameUI.SetActive(true);
    }
}
