using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class button : MonoBehaviour
{
    int player;
    string clicker;

    GameLogic gamelogic;

    void Start()
    {
        gamelogic = FindObjectOfType<GameLogic>();
    }

    public void set_player(int _player)
    {
        player = _player;
    }

    public void set_clicker(string _clicker)
    {
        clicker = _clicker;
    }

    public void onclick()
    {
        if(clicker == PhotonNetwork.LocalPlayer.ActorNumber.ToString() && gamelogic.trade_confirmed_count == 0)
        {
            gamelogic.set_for_money(player);
        }
    }

}
