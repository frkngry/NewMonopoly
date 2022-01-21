using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Monopoly_Player : MonoBehaviour
{
    PhotonView myPV;
    public Vector2 init_pos;
    public int i, index, squaresToMove, money;
    public bool passedGo;
    public int play_order;
    public int actor_ID;
    public List<int> properties;
    public string nickname;

    public int loyality_rate = 0; //bu2 satýr
    public int rent_paid_count = 0;

    public bool isinjail = false;
    public int jail_turns = 0;
    public bool can_buy_house = false;
    public bool trade_offer_send = false;
    public string trade_sender;

    public PlayerPropertyItem playerpropertyitem;

    Vector2[] pos = new Vector2[]
    {
        new Vector2(0,0),
        new Vector2(-16,0),
        new Vector2(-28,0),
        new Vector2(-40.5f,0),
        new Vector2(-53,0),
        new Vector2(-65,0),
        new Vector2(-77,0),
        new Vector2(-89.5f,0),
        new Vector2(-102,0),
        new Vector2(-114,0),
        new Vector2(-128,0),
        new Vector2(-128,16),
        new Vector2(-128,28),
        new Vector2(-128,40.5f),
        new Vector2(-128,53),
        new Vector2(-128,65),
        new Vector2(-128,77),
        new Vector2(-128,89.5f),
        new Vector2(-128,102),
        new Vector2(-128,114),
        new Vector2(-128,128),
        new Vector2(-114,128),
        new Vector2(-102,128),
        new Vector2(-89.5f,128),
        new Vector2(-77,128),
        new Vector2(-65,128),
        new Vector2(-53,128),
        new Vector2(-40.5f,128),
        new Vector2(-28,128),
        new Vector2(-16,128),
        new Vector2(0,128),
        new Vector2(0,114),
        new Vector2(0,102),
        new Vector2(0,89.5f),
        new Vector2(0,77),
        new Vector2(0,65),
        new Vector2(0,53),
        new Vector2(0,40.5f),
        new Vector2(0,28),
        new Vector2(0,16)
    };

    Color[] color_array = new Color[]
{
        Color.white,
        Color.black,
        Color.gray,
        Color.red,
        Color.cyan,
        Color.blue
};

    Vector2 start_point = new Vector2(144, -65);

    Dictionary<string, int> positions = new Dictionary<string, int>()
    {

        {"Go", 0},
        {"Boardwalk", 39},
        {"Illinois Avenue", 24},
        {"St. Charles Place", 11},
        {"Reading Railroad", 5},
        {"Electric Company", 12},
        {"Water Works", 28},
        {"Jail", 10}
    };

// Start is called before the first frame update
void Start()
    {
        myPV = GetComponent<PhotonView>();
        passedGo = false;
        i = 0; index = 0; squaresToMove = 0; money = 1400;
    }

    // Update is called once per frame
    void Update()
    {
        if (myPV.IsMine)
        {
            //Movement
            if (i < 50) { i++; }//60 is there so that the game updates positions
            else if (i == 50 && squaresToMove > 0)//Forward movement
            {
                i = 0;
                index = (index + 1) % 40; //40 because of the number of squares on the board.
                int[] temp_array = { actor_ID, index };
                GetComponent<PhotonView>().RPC("set_index_for_player", RpcTarget.All, temp_array);
                this.transform.position = start_point + pos[index];
                squaresToMove--;
                if (index == 0)
                {
                    passedGo = true;
                }
            }
            else if (i == 50 && squaresToMove < 0)//Backwards movement
            {
                i = 0;
                index = (index + 39) % 40; //40 because of the number of squares on the board. +39 instead of -1 because modulus operation could return a negative number.
                int[] temp_array = { actor_ID, index };
                GetComponent<PhotonView>().RPC("set_index_for_player", RpcTarget.All, temp_array);
                this.transform.position = start_point + pos[index];
                squaresToMove++;
            }
        }
    }

    public void move(int x)
    {
        squaresToMove = x;
    }

    [PunRPC]
    public void move(string x)
    {
        int result = 0;
        if (x == "Railroad")//Nearest railroad
        {
            result = (45 - index) % 10;
        }

        else if (x == "Utility")//Nearest Utility
        {
            if(index < positions["Electric Company"])
            {
                result = (positions["Electric Company"] + 40 - index) % 40;
            }
            else if(index > positions["Electric Company"] && index < positions["Water Works"])
            {
                result = (positions["Water Works"] + 40 - index) % 40;
            }
            else
            {
                result = (positions["Electric Company"] + 40 - index) % 40;
            }
        }

        else//Anything else
        {
            result = (positions[x] + 40 - index) % 40;
        }
        this.move(result);
    }

    public void SetPlayOrder(int order, int actor_id)
    {
        play_order = order;
        actor_ID = actor_id;
    }

    [PunRPC]
    public void set_name_for_prefab(string[] names)
    {
        GameObject temp = GameObject.Find(names[0]);
        temp.name = names[1];
        Monopoly_Player temp_player = temp.GetComponent<Monopoly_Player>();
        temp_player.actor_ID = int.Parse(names[1]);
        temp_player.nickname = names[2];
    }

    [PunRPC]
    public void set_index_for_player(int[]array)
    {
        GameObject temp = GameObject.Find(array[0].ToString());
        Monopoly_Player temp_monopoly_player = temp.GetComponent<Monopoly_Player>();
        temp_monopoly_player.index = array[1];
    }

    [PunRPC]
    public void set_prefab_color(int [] array)
    {
        GameObject temp = GameObject.Find(array[0].ToString());
        temp.GetComponent<SpriteRenderer>().color = color_array[array[1]];
    }

    [PunRPC]
    public void go_to_jail(string actor_id)
    {
        GameObject temp = GameObject.Find(actor_id);
        Monopoly_Player temp_player = temp.GetComponent<Monopoly_Player>();
        temp_player.isinjail = true;
        temp_player.jail_turns = 3;
        temp_player.index = 10;
        temp.transform.position = new Vector3(12, -66, 0);
    }
}
