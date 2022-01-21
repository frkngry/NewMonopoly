using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class ShowPricesItem : MonoBehaviour
{
    public GameLogic gameLogic;
    float inflation = 1;

    public void update_prices() // enflasyon gelicek
    {
        GetComponent<PhotonView>().RPC("set_prices", RpcTarget.All);
    }

    public void update_inflation(float _inflation)
    {
        GetComponent<PhotonView>().RPC("set_inflation", RpcTarget.All, _inflation);
    }

    [PunRPC]
    public void set_inflation(float _inflation)
    {
        inflation = _inflation;
    }

    [PunRPC]
    public void set_prices()
    {
        GameObject temp = GameObject.Find("sq1");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[1].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq3");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[3].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq4");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[4].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq5");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[5].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq6");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[6].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq8");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[8].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq9");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[9].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq11");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[11].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq12");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[12].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq13");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[13].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq14");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[14].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq15");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[15].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq16");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[16].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq18");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[18].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq19");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[19].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq21");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[21].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq23");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[23].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq24");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[24].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq25");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[25].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq26");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[26].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq27");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[27].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq28");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[28].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq29");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[29].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq31");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[31].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq32");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[32].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq34");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[34].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq35");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[35].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq37");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[37].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq38");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[38].value * inflation)).ToString() + "$";

        temp = GameObject.Find("sq39");
        temp.GetComponent<Text>().text = ((int)Math.Round(gameLogic.squares[39].value * inflation)).ToString() + "$";

    }

}
