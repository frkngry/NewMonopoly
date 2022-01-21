using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameCard : MonoBehaviour
{

    public GameLogic gamelogic;
    public GameObject game_object;
    public Text name;
    public Text rent;
    public Text house1;
    public Text house2;
    public Text house3;
    public Text house4;
    public Text hotel;
    public Text housing;
    public Text rent_text;
    public Text house1_text;
    public Text house2_text;
    public Text house3_text;
    public Text house4_text;
    public Text hotel_text;
    public Text housing_text;


    public void set_cardd(int[] int_arr)
    {
        if(int_arr[7] == 0)
        {
            GetComponent<PhotonView>().RPC("set_card_property", RpcTarget.All, int_arr);
        }
        else if(int_arr[7] == 1)
        {
            GetComponent<PhotonView>().RPC("set_card_railroad", RpcTarget.All, int_arr);
        }
        else
        {
            GetComponent<PhotonView>().RPC("set_card_utility", RpcTarget.All, int_arr);
        }
    }

    [PunRPC]
    public void set_card_property(int[] arr )
    {
        game_object.SetActive(true);
        name.text = gamelogic.squares[arr[8]].name;
        rent_text.text = "Rent";
        house1_text.text = "House1";
        house2_text.text = "House2";
        house3_text.text = "House3";
        house4_text.text = "House4";
        hotel_text.text = "Hotel";
        housing_text.text = "Housing";
        rent.text = arr[0].ToString();
        house1.text = arr[1].ToString();
        house2.text = arr[2].ToString();
        house3.text = arr[3].ToString();
        house4.text = arr[4].ToString();
        hotel.text = arr[5].ToString();
        housing.text = arr[6].ToString();
    }

    [PunRPC]
    public void set_card_railroad(int[] arr)
    {
        game_object.SetActive(true);
        name.text = gamelogic.squares[arr[8]].name;
        rent_text.text = "1 Owned";
        house1_text.text = "2 Owned";
        house2_text.text = "3 Owned";
        house3_text.text = "4 Owned";
        house4_text.text = "";
        hotel_text.text = "";
        housing_text.text = "";
        game_object.SetActive(true);
        rent.text = arr[0].ToString();
        house1.text = arr[1].ToString();
        house2.text = arr[2].ToString();
        house3.text = arr[3].ToString();
        house4.text = "";
        hotel.text = "";
        housing.text = "";
    }
    [PunRPC]
    public void set_card_utility(int[] arr)
    {
        game_object.SetActive(true);
        name.text = gamelogic.squares[arr[8]].name;
        rent_text.text = "1 Owned";
        house1_text.text = "2 Owned";
        house2_text.text = "";
        house3_text.text = "";
        house4_text.text = "";
        hotel_text.text = "";
        housing_text.text = "";
        game_object.SetActive(true);
        rent.text = arr[0].ToString();
        house1.text = arr[1].ToString();
        house2.text = "";
        house3.text = "";
        house4.text = "";
        hotel.text = "";
        housing.text = "";
    }


    [PunRPC]
    public void deactivate()
    {
        game_object.SetActive(false);
    }

}
