using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Dice : MonoBehaviour

{
    public Button diceButton;
    public GameObject dice1, dice2;
    public Sprite[] diceSides;
    public SpriteRenderer rend1, rend2;
    public int leftDiceNum, rightDiceNum;
    private bool coroutineAllowed;

    public IEnumerator roll_dice()
    {
        for (int i = 0; i <= 20; i++)
        {
            leftDiceNum = Random.Range(0, 6);
            rightDiceNum = Random.Range(0, 6);
            int[] array = { leftDiceNum, rightDiceNum };
            GetComponent<PhotonView>().RPC("set_sprites", RpcTarget.All, array);
            yield return new WaitForSeconds(0.05f);
        }
    }

    [PunRPC]
    public void set_sprites(int[] array)
    {
        rend1 = dice1.GetComponent<SpriteRenderer>();
        rend2 = dice2.GetComponent<SpriteRenderer>();
        rend1.sprite = diceSides[array[0]];
        rend2.sprite = diceSides[array[1]];
    }
}
