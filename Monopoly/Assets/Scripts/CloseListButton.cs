using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseListButton : MonoBehaviour
{
    public bool if_instentiated_for_trade;

    GameLogic gamelogic;

    void Start()
    {
        gamelogic = FindObjectOfType<GameLogic>();
    }



    public void onclick()
    {
        if (if_instentiated_for_trade)
        {
            gamelogic.close_trade_list();
        }
        else
        {
            gamelogic.close_list();
        }
    }
}
