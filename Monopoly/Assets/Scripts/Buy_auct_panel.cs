using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class Buy_auct_panel : MonoBehaviour
{
    public GameLogic gameLogic;
    public Button buyButton, auctButton;
    public AuctionPanel auctionPanel;

    public void Start()
    {
        buyButton.onClick.AddListener(buy_property);
        auctButton.onClick.AddListener(AuctionStart);
        gameObject.SetActive(false);
    }

    public void AuctionStart()
    {
        auctionPanel.AuctionStart_Master();
        Remove();
    }

    public void Popup()
    {
        gameObject.SetActive(true);
        if (!gameLogic.check_if_buyable())
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }
    }

    public void Remove()
    {
        gameObject.SetActive(false);
    }

    public void buy_property()
    {
        buyButton.interactable = false;
        int actor_id = PhotonNetwork.LocalPlayer.ActorNumber;
        gameLogic.GetComponent<PhotonView>().RPC("BuyProperty", RpcTarget.All, actor_id);
        gameLogic.GetComponent<PhotonView>().RPC("set_update", RpcTarget.All, actor_id.ToString());
        gameLogic.GetComponent<PhotonView>().RPC("set_housability", RpcTarget.All);
        gameLogic.buy_or_auction_happened();
        Remove();
    }
}
