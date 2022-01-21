using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildHouseItem : MonoBehaviour
{
    public Text PropertyName;
    GameLogic gamelogic;
    public int index;
    public bool if_house;
    public bool if_sell;
    public bool if_mortgage;
    public bool if_unmortgage;


    void Start()
    {
        gamelogic = FindObjectOfType<GameLogic>();
    }


    public void SetPropertyName(string _propertyname)
    {
        PropertyName.text = _propertyname;
    }
    public void SetIndex(int _propertyindex)
    {
        index = _propertyindex;
    }
    public void OnClickItem()
    {
        if (!if_sell && !if_mortgage && !if_unmortgage)
        {
            if (if_house)
            {
                gamelogic.buyHouse(index);
            }
            else
            {
                gamelogic.buyHotel(index);
            }
        }
        else if(if_sell && !if_mortgage && !if_unmortgage)
        {
            if (if_house)
            {
                gamelogic.sellHouse(index);
            }
            else
            {
                gamelogic.sellHotel(index);
            }
        }
        else
        {
            if (if_mortgage && !if_unmortgage)
            {
                gamelogic.mortgage(index); // mortgage
            }
            else
            {
                gamelogic.unmortgage(index); //unmortgage
            }
        }
    }


}
