using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TradePropertyItem : MonoBehaviour
{
    GameLogic gamelogic;
    public Text PropertyName;
    public int index;
    public int traded_or_not;
    public string clicker;
    public int player;


    void Start()
    {
        gamelogic = FindObjectOfType<GameLogic>();
    }
    public void set_name(string _name)
    {
        PropertyName.text = _name;
    }
    public void set_index(int _index)
    {
        index = _index;
    }

    public void set_trader_or_not(int a)
    {
        traded_or_not = a;
    }

    public void set_clicker(string _clicker)
    {
        clicker = _clicker;
    }
    public void set_player(int _player)
    {
        player = _player;
    }

    public void onclick()
    {
        if(clicker == PhotonNetwork.LocalPlayer.ActorNumber.ToString() && gamelogic.trade_confirmed_count == 0)
        {
            if(traded_or_not == 0)
            {
                traded_or_not = 1;
            }
            else
            {
                traded_or_not = 0;
            }
            gamelogic.change_position(index, traded_or_not, player);
        }
    }

}
