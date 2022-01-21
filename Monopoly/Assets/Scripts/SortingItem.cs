using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SortingItem : MonoBehaviour
{
    SortingManager manager;

    public Text Player_Name;
    public Text Player_dice;
    public Text Player_turn_order;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<SortingManager>();
    }

    public void SetPlayerInfo(Player _player, int dice, int turn)
    {
        Player_Name.text = _player.NickName;
        Player_dice.text = "Rolled " + dice;
        Player_turn_order.text = turn.ToString();
    }

}
