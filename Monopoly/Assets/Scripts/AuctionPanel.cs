using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AuctionPanel : MonoBehaviour
{
    public GameLogic gameLogic;
    public Button bidButton, foldButton;
    public InputField inputField;
    public Text propertyName, propertyValue, highestBidValText;
    public Vector2Int max_player_and_offer;//[0] = playerid, [1] = max_offer
    public int whoseTurn;
    public List<Monopoly_Player> player_list;
    int property_index;
    string auction_starter;

    // Start is called before the first frame update
    void Start()
    {
        bidButton.onClick.AddListener(bidAction);
        foldButton.onClick.AddListener(foldAction);
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameLogic.player.actor_ID == player_list[whoseTurn].actor_ID)
        {
            bidButton.interactable = true;
            foldButton.interactable = true;
        }
        else
        {
            bidButton.interactable = false;
            foldButton.interactable = false;
        }
        try
        {
            highestBidValText.text = max_player_and_offer[1].ToString();
        }
        catch (System.Exception e)
        {
            print(e);
        }
    }

    void bidAction()
    {
        int val = 0;
        try
        {
            val = int.Parse(inputField.text);
        }
        catch (System.Exception e)
        {
            print(e);
        }

        if (val <= max_player_and_offer[1])
        {
            foldAction();
        }
        else
        {
            gameObject.GetComponent<PhotonView>().RPC("UpdateVals", RpcTarget.All, gameLogic.player.actor_ID, val);
        }
        gameObject.GetComponent<PhotonView>().RPC("NextTurn", RpcTarget.All);
    }

    void foldAction()
    {
        //Remove the current player from the auction
        gameObject.GetComponent<PhotonView>().RPC("WithdrawPlayer", RpcTarget.All, whoseTurn);
        //Check if only one player is left
        print("Player count after fold: " + player_list.Count.ToString());
        if (player_list.Count == 1)
        {
            GetComponent<PhotonView>().RPC("set_end_button", RpcTarget.All);

            print("auction over");
            AuctionOver();
        }
        else
        {
            print("next turn");
            gameObject.GetComponent<PhotonView>().RPC("NextTurn", RpcTarget.All);
        }
    }

    public void Remove()
    {
        gameObject.SetActive(false);
        whoseTurn = 0;
        player_list.Clear();
        max_player_and_offer[0] = 0;
        max_player_and_offer[1] = 0;
    }

    public void AuctionStart_Master()
    {
        auction_starter = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
        gameObject.SetActive(true);
        player_list = new List<Monopoly_Player>();
        int[] temp_player_list = new int[PhotonNetwork.PlayerList.Length];
        player_list.Clear();
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            int playerid = PhotonNetwork.PlayerList[i].ActorNumber;
            GameObject temp = GameObject.Find(playerid.ToString());
            Monopoly_Player temp_player = temp.GetComponent<Monopoly_Player>();
            player_list.Add(temp_player);
            temp_player_list[i] = temp_player.actor_ID;
        }
        whoseTurn = 0;
        gameLogic.GetComponent<PhotonView>().RPC("OpenAllAuctions", RpcTarget.All);
        gameObject.GetComponent<PhotonView>().RPC("AuctionStart", RpcTarget.All, temp_player_list, whoseTurn);
        int player_index = gameLogic.player.index;
        var sqr = gameLogic.squares[player_index];
        gameObject.GetComponent<PhotonView>().RPC("InitAuctionValues", RpcTarget.All, sqr.name, sqr.value.ToString(), player_index);
        print("Count:" + player_list.Count.ToString());
    }

    void AuctionOver()
    {
        int actor_id = max_player_and_offer[0];
        // deðiþecek buy_property
        gameLogic.GetComponent<PhotonView>().RPC("BuyPropertyCustom", RpcTarget.All, actor_id, max_player_and_offer[1], property_index);
        gameLogic.GetComponent<PhotonView>().RPC("set_update", RpcTarget.All, actor_id.ToString());
        gameLogic.GetComponent<PhotonView>().RPC("set_housability", RpcTarget.All);
        gameObject.GetComponent<PhotonView>().RPC("CloseAll", RpcTarget.All);
    }

    [PunRPC]
    public void WithdrawPlayer(int x)
    {
        player_list.RemoveAt(x);
        print(player_list.Count);
    }

    [PunRPC]
    public void NextTurn()
    {
        whoseTurn = (whoseTurn + 1) % player_list.Count;
    }

    [PunRPC]
    void InitAuctionValues(string name, string value, int property_index_)
    {
        //int player_index = gameLogic.player.index;
        //var sqr = gameLogic.squares[player_index];
        propertyName.text = name;
        propertyValue.text = "Original Price:" + value.ToString();
        property_index = property_index_;
    }

    [PunRPC]
    public void UpdateVals(int highestBidder, int highestBid)
    {
        max_player_and_offer[0] = highestBidder;
        max_player_and_offer[1] = highestBid;
    }

    [PunRPC]
    public void AuctionStart(int[] player_list_, int whoseTurn_)
    {
        max_player_and_offer[1] = 0;
        player_list.Clear();
        for (int i = 0; i < player_list_.Length; i++)
        {
            GameObject temp = GameObject.Find(player_list_[i].ToString());
            Monopoly_Player temp_player = temp.GetComponent<Monopoly_Player>();
            player_list.Add(temp_player);
        }
        whoseTurn = whoseTurn_;
    }

    [PunRPC]
    public void CloseAll()
    {
        Remove();
    }

    [PunRPC]

    public void set_end_button()
    {
        if(PhotonNetwork.LocalPlayer.ActorNumber.ToString() == auction_starter)
        {
            gameLogic.buy_or_auction_happened();
        }
    }
}