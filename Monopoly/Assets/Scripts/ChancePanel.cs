using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChancePanel : MonoBehaviour
{

    public GameLogic gameLogic;
    public Button okButton;
    public Text chanceText;
    public int card;
    public Monopoly_Player player;

    void Start()
    {
        gameObject.SetActive(false);
        okButton.onClick.AddListener(Action);
        okButton.onClick.AddListener(Remove);
    }

    public void Remove()
    {
        gameObject.SetActive(false);
    }

    public void Popup()
    {
        gameObject.SetActive(true);
        card = Random.Range(1, 15);

        switch (card)
        {
            case 1:
                chanceText.text = "Advance to Boardwalk";
                break;
            case 2:
                chanceText.text = "Advance to Go (Collect $200)";
                break;
            case 3:
                chanceText.text = "Advance to Illinois Avenue. If you pass Go, collect $200";
                break;
            case 4:
                chanceText.text = "Advance to St. Charles Place. If you pass Go, collect $200";
                break;
            case 5:
                chanceText.text = "Advance to the nearest Railroad. If unowned, you may buy it from the Bank. If owned, pay wonder twice the rental to which they are otherwise entitled";
                break;
            case 6:
                chanceText.text = "Advance to the nearest Railroad. If unowned, you may buy it from the Bank. If owned, pay wonder twice the rental to which they are otherwise entitled";
                break;
            case 7:
                chanceText.text = "Advance token to nearest Utility. If unowned, you may buy it from the Bank. If owned, throw dice and pay owner a total ten times amount thrown.";
                break;
            case 8:
                chanceText.text = "Go Back 3 Spaces";
                break;
            case 9:
                chanceText.text = "Take a trip to Reading Railroad. If you pass Go, collect $200";
                break;
            case 10:
                chanceText.text = "Bank pays you dividend of $50";
                break;
            case 11:
                chanceText.text = "Go to Jail. Go directly to Jail, do not pass Go, do not collect $200";
                break;
            case 12:
                chanceText.text = "Make general repairs on all your property. For each house pay $25. For each hotel pay $100";
                break;
            case 13:
                chanceText.text = "Speeding fine $15";
                break;
            case 14:
                chanceText.text = "Your building loan matures. Collect $150";
                break;
            case 15:
                chanceText.text = "You have been elected Chairman of the Board. Pay each player $50";
                break;
        }
    }

    public void Action()
    {
        int[] temp_array = { 0, 0 };
        int[] temp_array2 = { 0, 0, 0 };
        string[] str_temp_array = { "", "" };
        string str_temp = "";


        switch (card)
        {
            case 1:
                str_temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                str_temp_array[1] = "Boardwalk";
                gameLogic.GetComponent<PhotonView>().RPC("move_to_location", RpcTarget.All, str_temp_array);
                break;
            case 2:
                str_temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                str_temp_array[1] = "Go";
                gameLogic.GetComponent<PhotonView>().RPC("move_to_location", RpcTarget.All, str_temp_array);
                break;
            case 3:
                str_temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                str_temp_array[1] = "Illinois Avenue";
                gameLogic.GetComponent<PhotonView>().RPC("move_to_location", RpcTarget.All, str_temp_array);
                break;
            case 4:
                str_temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                str_temp_array[1] = "St. Charles Place";
                gameLogic.GetComponent<PhotonView>().RPC("move_to_location", RpcTarget.All, str_temp_array);
                break;
            case 5:
                str_temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                str_temp_array[1] = "Railroad";
                gameLogic.GetComponent<PhotonView>().RPC("move_to_location", RpcTarget.All, str_temp_array);
                break;
            case 6:
                str_temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                str_temp_array[1] = "Railroad";
                gameLogic.GetComponent<PhotonView>().RPC("move_to_location", RpcTarget.All, str_temp_array);
                break;
            case 7:
                str_temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                str_temp_array[1] = "Utility";
                gameLogic.GetComponent<PhotonView>().RPC("move_to_location", RpcTarget.All, str_temp_array);
                break;
            case 8:
                //Go Back 3 Spaces
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = -3;
                gameLogic.GetComponent<PhotonView>().RPC("move_back", RpcTarget.All, temp_array);
                break;
            case 9:
                str_temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                str_temp_array[1] = "Reading Railroad";
                gameLogic.GetComponent<PhotonView>().RPC("move_to_location", RpcTarget.All, str_temp_array);
                break;
            case 10:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 50;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 11:
                //Go to Jail. Go directly to Jail, do not pass Go, do not collect $200
                str_temp = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                gameLogic.GetComponent<PhotonView>().RPC("go_to_jail_with_card", RpcTarget.All, str_temp);
                break;
            case 12:
                //Make general repairs on all your property. For each house pay $25. For each hotel pay $100"
                temp_array2[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array2[1] = 25;
                temp_array2[2] = 100;
                gameLogic.GetComponent<PhotonView>().RPC("pay_property_cost", RpcTarget.All, temp_array2);
                break;
            case 13:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = -15;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 14:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 150;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 15:
                //Taking money to local player
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 50 * PhotonNetwork.PlayerList.Length;
                gameLogic.GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp_array);
                //Giving money to other players
                for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                {
                    temp_array[0] = PhotonNetwork.PlayerList[i].ActorNumber;
                    temp_array[1] = 50;
                    gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                }
                break;
        }
    }
}
