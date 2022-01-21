using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChestPanel : MonoBehaviour
{

    public GameLogic gameLogic;
    public Button okButton;
    public Text chestText;
    public int card;


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
                chestText.text = "Advance to Go (Collect $200)";
                break;
            case 2:
                chestText.text = "Bank error in your favor. Collect $200";
                break;
            case 3:
                chestText.text = "Doctor’s fee. Pay $50";
                break;
            case 4:
                chestText.text = "From sale of stock you get $50";
                break;
            case 5:
                chestText.text = "You have won second prize in a beauty contest. Collect $10";
                break;
            case 6:
                chestText.text = "Go to Jail. Go directly to jail, do not pass Go, do not collect $200";
                break;
            case 7:
                chestText.text = "Holiday fund matures. Receive $100";
                break;
            case 8:
                chestText.text = "Income tax refund. Collect $20";
                break;
            case 9:
                chestText.text = "It is your birthday. Collect $10 from every player";
                break;
            case 10:
                chestText.text = "Life insurance matures. Collect $100";
                break;
            case 11:
                chestText.text = "Pay hospital fees of $100";
                break;
            case 12:
                chestText.text = "Pay school fees of $50";
                break;
            case 13:
                chestText.text = "Receive $25 consultancy fee";
                break;
            case 14:
                chestText.text = "You are assessed for street repair. $40 per house. $115 per hotel";
                break;
            case 15:
                chestText.text = "You have won second prize in a beauty contest. Collect $10";
                break;
        }
    }


    public void Action()
    {

        int[] temp_array = { 0, 0 };
        int[] temp_array2 = { 0, 0, 0 };
        string str_temp = "";
        string[] str_temp_array = { "", "" };

        switch (card)
        {
            case 1:
                str_temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                str_temp_array[1] = "Go";
                gameLogic.GetComponent<PhotonView>().RPC("move_to_location", RpcTarget.All, str_temp_array);
                break;
            case 2:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 200;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 3:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = -50;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 4:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 50;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 5:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 100;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 6:
                //Go to Jail. Go directly to jail, do not pass Go, do not collect $200
                str_temp = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                gameLogic.GetComponent<PhotonView>().RPC("go_to_jail_with_card", RpcTarget.All, str_temp);
                break;
            case 7:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 100;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 8:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 20;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 9:
                //Taking money to local player
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 10 * PhotonNetwork.PlayerList.Length;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                //Giving money to other players
                for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                {
                    temp_array[0] = PhotonNetwork.PlayerList[i].ActorNumber;
                    temp_array[1] = 10;
                    gameLogic.GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp_array);
                }
                break;
            case 10:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 100;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 11:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = -100;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 12:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = -50;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 13:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 25;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
            case 14:
                //You are assessed for street repair. $40 per house. $115 per hotel
                temp_array2[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array2[1] = 40;
                temp_array2[2] = 115;
                gameLogic.GetComponent<PhotonView>().RPC("pay_property_cost", RpcTarget.All, temp_array2);
                break;
            case 15:
                temp_array[0] = PhotonNetwork.LocalPlayer.ActorNumber;
                temp_array[1] = 10;
                gameLogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                break;
        }
    }

}
