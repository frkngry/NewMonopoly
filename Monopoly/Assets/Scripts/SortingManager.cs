using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SortingManager : MonoBehaviourPunCallbacks, IPunObservable
{

    public GameObject Roll_Dice_Button;
    public GameObject Start_Game_Button;
    bool room_property_change_ready = false;
    bool player_property_change_ready = true;
    bool phase1 = false;
    bool phase2 = false;
    bool end = false;
    bool ranking_done = false;
    bool master_finished = false;
    int[] order_array = new int[PhotonNetwork.PlayerList.Length];
    int[] rolled_dice_array = new int[PhotonNetwork.PlayerList.Length];


    public SortingItem sortingItemPrefab;
    public Transform sortingItemParent;
    public Text Rolleddice;

    Player[] player_list = new Player[PhotonNetwork.PlayerList.Length];

    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.SerializationRate = 40;
        PhotonNetwork.SendRate = 40;
        if (PhotonNetwork.IsMasterClient)
        {
            player_list = PhotonNetwork.PlayerList;
            int count = PhotonNetwork.PlayerList.Length;
            for (int i = 0; i < count; i++)
            {
                order_array[i] = i;
                Hashtable hash = new Hashtable();
                hash.Add("turn_order", i);
                PhotonNetwork.PlayerList[i].SetCustomProperties(hash);
            }
            Hashtable hash2 = new Hashtable();
            hash2.Add("turn", 0);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash2);
        }
        phase1 = true;
    }

    private void Update()
    {
        if (room_property_change_ready && (!ranking_done) && player_property_change_ready)
        {
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["turn"] == PhotonNetwork.PlayerList.Length && phase2 == false && end == false && PhotonNetwork.IsMasterClient)
            {
                phase1 = false;
                phase2 = true;
            }
            else if (phase1)
            {
                if ((int)PhotonNetwork.CurrentRoom.CustomProperties["turn"] == (int)PhotonNetwork.LocalPlayer.CustomProperties["turn_order"] && Roll_Dice_Button.activeInHierarchy == false)
                {
                    Roll_Dice_Button.SetActive(true);
                }
            }
            else if (phase2 && PhotonNetwork.IsMasterClient)
            {
                int count = PhotonNetwork.PlayerList.Length;
                Array.Sort(rolled_dice_array, order_array);
                Array.Reverse(order_array);
                Array.Reverse(rolled_dice_array);
                for (int i = 0; i < count; i++)
                {
                    Hashtable hash = new Hashtable();
                    hash.Add("turn_order", order_array[i]);
                    PhotonNetwork.PlayerList[i].SetCustomProperties(hash);
                }
                phase2 = false;
                end = true;
            }
            else if (end && PhotonNetwork.IsMasterClient)
            {
                ranking_done = true;
                for(int a = 0; a < PhotonNetwork.PlayerList.Length; a++)
                {
                    SortingItem newSortingItem = Instantiate(sortingItemPrefab, sortingItemParent);
                    newSortingItem.SetPlayerInfo(PhotonNetwork.PlayerList[order_array[a]], rolled_dice_array[a], a+1);
                }
                master_finished = true;
            }
            else if (master_finished)
            {
                ranking_done = true;
                for (int a = 0; a < PhotonNetwork.PlayerList.Length; a++)
                {
                    SortingItem newSortingItem = Instantiate(sortingItemPrefab, sortingItemParent);
                    newSortingItem.SetPlayerInfo(player_list[order_array[a]], rolled_dice_array[a], a+1);
                }
            }
        }
        else if (ranking_done && PhotonNetwork.IsMasterClient)
        {
            Start_Game_Button.SetActive(true);
        }
    }

    public void roll_dice()
    {
        room_property_change_ready = false;
        int num1 = UnityEngine.Random.Range(1, 6);
        Hashtable hash = new Hashtable();
        hash.Add("rolled_dice", num1);
        PhotonNetwork.PlayerList[(int)PhotonNetwork.LocalPlayer.CustomProperties["turn_order"]].SetCustomProperties(hash);
        if (PhotonNetwork.IsMasterClient)
        {
            rolled_dice_array[(int)PhotonNetwork.LocalPlayer.CustomProperties["turn_order"]] = num1;
        }
        Roll_Dice_Button.SetActive(false);
        Rolleddice.text = num1.ToString();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(master_finished);
            stream.SendNext(player_list);
            stream.SendNext(order_array);
            stream.SendNext(rolled_dice_array);
            stream.SendNext(room_property_change_ready);
            stream.SendNext(phase1);
            stream.SendNext(phase2);
            stream.SendNext(end);
        }
        if (stream.IsReading)
        {
            master_finished = (bool)stream.ReceiveNext();
            player_list = (Player[])stream.ReceiveNext();
            order_array = (int[])stream.ReceiveNext();
            rolled_dice_array = (int[])stream.ReceiveNext();
            room_property_change_ready = (bool)stream.ReceiveNext();
            phase1 = (bool)stream.ReceiveNext();
            phase2 = (bool)stream.ReceiveNext();
            end = (bool)stream.ReceiveNext();
        }
    }

    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(target, changedProps);
        if (changedProps.ContainsKey("rolled_dice"))
        {
            Rolleddice.text =  ((int)target.CustomProperties["rolled_dice"]).ToString();
            if (PhotonNetwork.IsMasterClient && phase1 == true)   //increase turn
            {
                rolled_dice_array[(int)target.CustomProperties["turn_order"]] = (int)target.CustomProperties["rolled_dice"];
                int temp_turn = (int)PhotonNetwork.CurrentRoom.CustomProperties["turn"];
                temp_turn++;
                Hashtable hash = new Hashtable();
                hash.Add("turn", temp_turn);
                PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
            }
        }

    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        if (propertiesThatChanged.ContainsKey("turn"))
        {
            if(room_property_change_ready == false)
            {
                room_property_change_ready = true;
            }
        }
    }
    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
