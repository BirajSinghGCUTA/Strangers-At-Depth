using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomItem : MonoBehaviour
{
    public TMP_Text roomName;
    // Start is called before the first frame update
    public CreateAndJoinRoom manager;

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    void Start()
    {
        manager = FindObjectOfType<CreateAndJoinRoom>();
    }
    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
