using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{

    public Text roomName;
    LobbyManager manager;

    private void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string _roomname)
    {
        roomName.text = _roomname;
    }

    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
    }

}
