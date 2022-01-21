using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    public Button normal_button;
    public Button hard_button;
    public Button extreme_button;
    public Text Difficulty_text;
    int game_mode;

    public InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemList = new List<RoomItem>();
    public Transform contentObject;

    public float timebetweenupdates = 1.5f;
    float nextUpdateTime;

    private List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playButton;
    private void Start()
    {
        PhotonNetwork.JoinLobby();
        normal_button.onClick.AddListener(set_normal);
        hard_button.onClick.AddListener(set_hard);
        extreme_button.onClick.AddListener(set_extreme);
        GameLogic.game_mode = 0;
        game_mode = 0;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playButton.SetActive(true);
        }    
        else
        {
            playButton.SetActive(false);
        }
    }

    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = 6});
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timebetweenupdates;
        }  
    }

    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach(RoomItem item in roomItemList)
        {
            Destroy(item.gameObject);
        }
        roomItemList.Clear();
    
    
        foreach(RoomInfo room in list)
        {
            
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemList.Add(newRoom); 
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }


    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }


    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public void OnClickPlayButton()
    {
        GetComponent<PhotonView>().RPC("set_difficulty", RpcTarget.All, game_mode);
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel("Sorting");
    }
    public void set_normal()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GetComponent<PhotonView>().RPC("set_text", RpcTarget.All, "normal");
        }
    }
    public void set_hard()
    {
        if (PhotonNetwork.IsMasterClient)
        {         
            GetComponent<PhotonView>().RPC("set_text", RpcTarget.All, "hard");
        }
    }
    public void set_extreme()
    {
        if (PhotonNetwork.IsMasterClient)
        {  
            GetComponent<PhotonView>().RPC("set_text", RpcTarget.All, "extreme");
        }
    }

    [PunRPC]
    public void set_text(string difficulty)
    {
        if(difficulty == "normal")
        {
            GameLogic.game_mode = 0;
        }
        else if (difficulty == "hard")
        {
            GameLogic.game_mode = 1;
        }
        else if (difficulty == "extreme")
        {
            GameLogic.game_mode = 2;
        }
        GameObject temp = GameObject.Find("GameDifficultyText");
        temp.GetComponent<Text>().text = difficulty;
    }

    [PunRPC]
    public void set_difficulty(int difficulty)
    {
        GameLogic.game_mode = difficulty;
    }
}














