using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class PlayerPropertyItem : MonoBehaviour
{
    Monopoly_Player Player_who_is_shown;
    public Text Player_Name;
    public Text Player_Money;
    public Text loyality_Rate;  //bu satýr

    public Text propertyTextPrefab;
    public Transform propertyTextParent;

    private GameLogic real_game_logic;
    public int action_id;
    public string player_name;
    public int player_money = 1400;
    bool if_started = true;
    public bool update_properties = false;
    List<Text> text_array = new List<Text>();
    int element_count = 0;
    private void Update()
    {
        if (if_started)
        {
            GameObject temp = GameObject.Find(action_id.ToString());
            if (temp != null)
            {
                Player_who_is_shown = temp.GetComponent<Monopoly_Player>();

                Player_Name.text = Player_who_is_shown.nickname;
                Player_Money.text = player_money + "$";
                if_started = false;
                GameObject temp2 = GameObject.Find("GameLogic");
                real_game_logic = temp2.GetComponent<GameLogic>();
            }
        }
        else
        {
            if (update_properties)
            {
                if (element_count != 0)
                {
                    for (int i = text_array.Count - 1; i != -1; i--)
                    {
                        Destroy(text_array[i]);
                    }
                    element_count = 0;
                    text_array.Clear();
                }
                for(int i = 0; i < Player_who_is_shown.properties.Count; i++)
                {
                    element_count++;
                    Text temp = Text.Instantiate(propertyTextPrefab, propertyTextParent);
                    temp.text = real_game_logic.squares[Player_who_is_shown.properties[i]].name;
                    text_array.Add(temp);
                }
                update_properties = false;
            }
            Player_Money.text = Player_who_is_shown.money + "$";
        }
    }

    [PunRPC]
    public void set_name_for_prefab(string[] names)
    {
        GameObject temp = GameObject.Find(names[0]);
        GameObject temp2 = GameObject.Find("Canvas");
        temp.transform.SetParent(temp2.transform);
        temp.name = names[1] + "p";
        temp.GetComponent<PlayerPropertyItem>().action_id = int.Parse( names[1]);
    }

    [PunRPC]    //bu func
    public void set_loyality_rate(string actor_id)
    {
        GameObject temp = GameObject.Find(actor_id);
        Monopoly_Player temp_player = temp.GetComponent<Monopoly_Player>();
        int loyality_Rate = temp_player.loyality_rate;
        temp = GameObject.Find(actor_id + "p");
        PlayerPropertyItem temp_item = temp.GetComponent<PlayerPropertyItem>();
        temp_item.loyality_Rate.text = "%" + loyality_Rate.ToString();
    }
}
