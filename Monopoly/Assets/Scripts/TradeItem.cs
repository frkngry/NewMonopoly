using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeItem : MonoBehaviour
{
    public Text PlayerName;
    GameLogic gamelogic;
    public string actor_id;


    void Start()
    {
        gamelogic = FindObjectOfType<GameLogic>();
    }
    public void SetPlayerName(string _playername)
    {
        PlayerName.text = _playername;
    }
    public void SetPlayeryID(string _playerID)
    {
        actor_id = _playerID;
    }


    public void OnClickItem()
    {
        gamelogic.trade(actor_id);
    }

}
