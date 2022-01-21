using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameLogic : MonoBehaviourPunCallbacks
{
    public ShowPricesItem price_show;
    public PlayerPropertyItem PlayerPropertyItemPrefab;
    public GameObject gamelogic;

    public GameObject PlayerPrefab;
    public Buy_auct_panel buy_auct_panel;
    public Monopoly_Player player;
    public AuctionPanel auctionPanel;

    //Chance and Chest panel
    public ChancePanel chance_panel;
    public ChestPanel chest_panel;


    public GameObject scrollview;
    public House HousePrefab;
    List<House> HouseList = new List<House>();

    public Hotel HotelPrefab;
    List<Hotel> HotelList = new List<Hotel>();

    public GameObject CloseListButton;
    public GameObject CloseTradeButton;

    public BuildHouseItem BuildHousePrefab;
    List<BuildHouseItem> BuildHouseItemList = new List<BuildHouseItem>();
    public Transform contentObject;

    public Button BuyHouseButton;
    public Button BuyHotelButton;

    public Button SellHouseButton;
    public Button SellHotelButton;

    public Button mortgageButton;
    public Button unmortgageButton;

    public Button EndTurnButton;
    public Button RollDiceButton;


    public GameObject ConfirmButton;

    public Button TradeButton;
    public TradeItem TradeItemprefab;
    List<TradeItem> TradeItemList = new List<TradeItem>();
    public GameObject Trade_image;
    public Text trade_Text;

    public GameObject trade_area;
    public Transform trade1_area;
    public Transform trade2_area;
    public Transform trade3_area;
    public Transform trade4_area;

    public InputField player1_money;
    public InputField player2_money;

    public button set_for_player1;
    public button set_for_player2;

    public Text player1_trade_money;
    public Text player2_trade_money;

    public TradePropertyItem tradepropertyitemprefab;
    List<TradePropertyItem> TradepropertyItemList1 = new List<TradePropertyItem>();
    List<TradePropertyItem> TradepropertyItemList2 = new List<TradePropertyItem>();
    List<TradePropertyItem> TradepropertyItemList3 = new List<TradePropertyItem>();
    List<TradePropertyItem> TradepropertyItemList4 = new List<TradePropertyItem>();

    public int trade_confirmed_count = 0;

    public Dice dices;
    bool dice_rolled = false;
    bool update_can_work = false;
    bool buy_button_clicked = false;
    private int dicenum1 = 0;
    private int dicenum2 = 0;

    public int whoseTurn = 0;
    public Monopoly_Player trader1;
    public Monopoly_Player trader2;

    private bool doublePayRailroad;
    private bool tenPayUtility;

    bool needs_to_sell = false;

    //RandomEvent stuff
    public int turnCount;
    public static int game_mode;// variable
    public int[] randomEventFrequencies;
    string[] RandomEvents = { "AdjustRent", "AdjustPrice" };

    //Inflation stuff
    public float inflation;
    int initialTotalMoney;

    public GameObject Cardarea;
    public GameCard gamecard;

    bool same_dice;

    SquareList squares_data = new SquareList();
    public Square[] squares = new Square[40];


    Vector2[] start_positions = new Vector2[]
    {
        new Vector2(138,-60),
        new Vector2(144,-60),
        new Vector2(150,-60),
        new Vector2(138,-66),
        new Vector2(144,-66),
        new Vector2(150,-66),
    };

    Vector3[] start_positions_for_list = new Vector3[]
    {
        new Vector3(-86, 62,0),
        new Vector3(-86, 2,0),
        new Vector3(-86,-57,0),
        new Vector3(-29,62,0),
        new Vector3(-29,2,0),
        new Vector3(-29,-57,0),
    };

    Vector2[] house_positions = new Vector2[]
    {
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(132,-57),
        new Vector2(130,-57),
        new Vector2(128,-57),
        new Vector2(126,-57),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(107,-57),
        new Vector2(105,-57),
        new Vector2(103,-57),
        new Vector2(101,-57),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(70,-57),
        new Vector2(68,-57),
        new Vector2(66,-57),
        new Vector2(64,-57),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(46,-57),
        new Vector2(44,-57),
        new Vector2(42,-57),
        new Vector2(40,-57),

        new Vector2(33,-57),
        new Vector2(31,-57),
        new Vector2(29,-57),
        new Vector2(27,-57),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(21,-53),
        new Vector2(21,-51),
        new Vector2(21,-49),
        new Vector2(21,-47),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(21,-29),
        new Vector2(21,-27),
        new Vector2(21,-25),
        new Vector2(21,-23),

        new Vector2(21,-16),
        new Vector2(21,-14),
        new Vector2(21,-12),
        new Vector2(21,-10),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(21,8),
        new Vector2(21,10),
        new Vector2(21,12),
        new Vector2(21,14),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(21,32),
        new Vector2(21,34),
        new Vector2(21,36),
        new Vector2(21,38),

        new Vector2(21,45),
        new Vector2(21,47),
        new Vector2(21,49),
        new Vector2(21,51),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(25,57),
        new Vector2(27,57),
        new Vector2(29,57),
        new Vector2(31,57),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(49,57),
        new Vector2(51,57),
        new Vector2(53,57),
        new Vector2(55,57),

        new Vector2(62,57),
        new Vector2(64,57),
        new Vector2(66,57),
        new Vector2(68,57),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(87,57),
        new Vector2(89,57),
        new Vector2(91,57),
        new Vector2(93,57),

        new Vector2(99,57),
        new Vector2(101,57),
        new Vector2(103,57),
        new Vector2(105,57),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(123,57),
        new Vector2(125,57),
        new Vector2(127,57),
        new Vector2(129,57),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(136,53),
        new Vector2(136,51),
        new Vector2(136,49),
        new Vector2(136,47),

        new Vector2(136,41),
        new Vector2(136,39),
        new Vector2(136,37),
        new Vector2(136,35),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(136,16),
        new Vector2(136,14),
        new Vector2(136,12),
        new Vector2(136,10),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(136,-21),
        new Vector2(136,-23),
        new Vector2(136,-25),
        new Vector2(136,-27),

        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0),

        new Vector2(136,-45),
        new Vector2(136,-47),
        new Vector2(136,-49),
        new Vector2(136,-51),

    };

    [System.Serializable]
    public class Square
    {
        public string name;
        public string type;
        public int value, owner = -1, rent, house1, house2, house3, house4, hotel;
        public int housing_price;
        public int color;
        public int house_count = 0;
        public bool has_hotel = false;
        public bool ismortgaged = false; // 0 for mortgaged property  
        public bool ishousable = false;
        public bool ishotelable = false;
        public bool ishousesellable = false;
        public bool ishotelsellable = false;
    }

    [System.Serializable]
    public class SquareList
    {
        public Square[] sq;
    }

    void Start()
    {

        turnCount = 0;
        randomEventFrequencies = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        gamecard = Cardarea.GetComponent<GameCard>();
        Cardarea.SetActive(false);
        RollDiceButton.interactable = false;
        EndTurnButton.interactable = false;
        BuyHouseButton.interactable = false;
        BuyHotelButton.interactable = false;
        SellHouseButton.interactable = false;
        SellHotelButton.interactable = false;
        mortgageButton.interactable = false;
        unmortgageButton.interactable = false;
        TradeButton.interactable = false;
        doublePayRailroad = false;
        tenPayUtility = false;
        RollDiceButton.onClick.AddListener(startfunc1);
        EndTurnButton.onClick.AddListener(endTurn);
        BuyHouseButton.onClick.AddListener(open_property_menu_for_house);
        BuyHotelButton.onClick.AddListener(open_property_menu_for_hotel);
        SellHouseButton.onClick.AddListener(open_property_menu_for_house_sell);
        SellHotelButton.onClick.AddListener(open_property_menu_for_hotel_sell);
        mortgageButton.onClick.AddListener(open_property_menu_for_mortgage);
        unmortgageButton.onClick.AddListener(open_property_menu_for_unmortgage);
        TradeButton.onClick.AddListener(open_player_menu_for_trade);

        TextAsset jsondata = new TextAsset();
        jsondata = Resources.Load<TextAsset>("properties");
        squares_data = JsonUtility.FromJson<SquareList>(jsondata.text);
        squares = squares_data.sq;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (i == (int)PhotonNetwork.LocalPlayer.CustomProperties["turn_order"])
            {
                GameObject temp = PhotonNetwork.Instantiate(PlayerPrefab.name, start_positions[i], Quaternion.identity);
                string[] temp_array = { temp.name, PhotonNetwork.LocalPlayer.ActorNumber.ToString(), PhotonNetwork.LocalPlayer.NickName };
                temp.GetComponent<PhotonView>().RPC("set_name_for_prefab", RpcTarget.All, temp_array);

                int[] temp_int_array = { PhotonNetwork.LocalPlayer.ActorNumber, i };
                temp.GetComponent<PhotonView>().RPC("set_prefab_color", RpcTarget.All, temp_int_array);

                player = temp.GetComponent<Monopoly_Player>();
                player.SetPlayOrder(i, PhotonNetwork.LocalPlayer.ActorNumber);

                temp = PhotonNetwork.Instantiate(PlayerPropertyItemPrefab.name, start_positions_for_list[i], Quaternion.identity);
                temp_array[0] = temp.name;
                temp_array[1] = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
                temp.GetComponent<PhotonView>().RPC("set_name_for_prefab", RpcTarget.All, temp_array);
                temp.GetComponent<PhotonView>().RPC("set_loyality_rate", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber.ToString()); // bu satýr
            }
        }
        price_show.update_inflation(inflation);
        price_show.update_prices();
        update_can_work = true;

        //Set inflation to 1 as a starting point
        inflation = 1;
        int moneyPerPlayerStart = 1400;

        //Calculate initial total money
        initialTotalMoney = PhotonNetwork.PlayerList.Length * moneyPerPlayerStart;
        /*
        if (PhotonNetwork.IsMasterClient)
        {
            GetComponent<PhotonView>().RPC("BuyProperty", RpcTarget.All, player.actor_ID, 1);
            GetComponent<PhotonView>().RPC("BuyProperty", RpcTarget.All, player.actor_ID, 3);
            GetComponent<PhotonView>().RPC("set_update", RpcTarget.All, player.actor_ID.ToString());
            GetComponent<PhotonView>().RPC("set_housability", RpcTarget.All);
        }
        */
    }

    private void Update()
    {

        if (update_can_work && !player.trade_offer_send)
        {
            if (whoseTurn == player.play_order && player.money < 0)
            {
                needs_to_sell = true;
                RollDiceButton.interactable = false;
                EndTurnButton.interactable = false;
                if (if_player_can_mortgage() && !buy_button_clicked)
                {
                    mortgageButton.interactable = true;
                }
                else
                {
                    mortgageButton.interactable = false;
                }
                if (if_player_can_sell_hotel() && !buy_button_clicked)
                {
                    SellHotelButton.interactable = true;
                }
                else
                {
                    SellHotelButton.interactable = false;
                }
                if (if_player_can_sell_house() && !buy_button_clicked)
                {
                    SellHouseButton.interactable = true;
                }
                else
                {
                    SellHouseButton.interactable = false;
                }
            }
            else if (needs_to_sell == true && player.money >= 0)
            {
                needs_to_sell = false;
                buy_or_auction_happened();
            }
            else
            {
                if (whoseTurn == player.play_order && !dice_rolled)
                {
                    if (if_player_can_buy_house() && !buy_button_clicked)
                    {
                        BuyHouseButton.interactable = true;
                    }
                    else
                    {
                        BuyHouseButton.interactable = false;
                    }
                    if (if_player_can_buy_hotel() && !buy_button_clicked)
                    {
                        BuyHotelButton.interactable = true;
                    }
                    else
                    {
                        BuyHotelButton.interactable = false;
                    }

                    if (if_player_can_unmortgage() && !buy_button_clicked)
                    {
                        unmortgageButton.interactable = true;
                    }
                    else
                    {
                        unmortgageButton.interactable = false;
                    }
                    if (!buy_button_clicked)
                    {
                        TradeButton.interactable = true;
                    }
                    else
                    {
                        TradeButton.interactable = false;
                    }
                    if (if_player_can_mortgage() && !buy_button_clicked)
                    {
                        mortgageButton.interactable = true;
                    }
                    else
                    {
                        mortgageButton.interactable = false;
                    }
                    if (if_player_can_sell_hotel() && !buy_button_clicked)
                    {
                        SellHotelButton.interactable = true;
                    }
                    else
                    {
                        SellHotelButton.interactable = false;
                    }
                    if (if_player_can_sell_house() && !buy_button_clicked)
                    {
                        SellHouseButton.interactable = true;
                    }
                    else
                    {
                        SellHouseButton.interactable = false;
                    }
                    RollDiceButton.interactable = true;
                }
                else if (whoseTurn == player.play_order && dice_rolled)
                {
                    if (if_player_can_mortgage() && !buy_button_clicked)
                    {
                        mortgageButton.interactable = true;
                    }
                    else
                    {
                        mortgageButton.interactable = false;
                    }
                    if (if_player_can_sell_hotel() && !buy_button_clicked)
                    {
                        SellHotelButton.interactable = true;
                    }
                    else
                    {
                        SellHotelButton.interactable = false;
                    }
                    if (if_player_can_sell_house() && !buy_button_clicked)
                    {
                        SellHouseButton.interactable = true;
                    }
                    else
                    {
                        SellHouseButton.interactable = false;
                    }
                    RollDiceButton.interactable = false;
                }
            }
        }
        else
        {
            RollDiceButton.interactable = false;
        }
    }
    void startfunc1() { StartCoroutine(func1()); }
    IEnumerator afterLanding()
    {
        while (player.squaresToMove != 0)
        {
            yield return null;
        }


        

        //Do action on property
        if (player.passedGo) {
            int[] temp_array = { PhotonNetwork.LocalPlayer.ActorNumber, 200 };
            gamelogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
            player.passedGo = false;

        }
        int index = player.index;
        Square square = squares[index];

        if ((square.type == "property") || (square.type == "utility") || (square.type == "railroad"))
        {
            int type = 0;
            if(square.type == "property")
            {
                type = 0;
            }
            else if (square.type == "railroad")
            {
                type = 1;
            }
            else
            {
                type = 2;
            }
            int[] int_array = {(int)Math.Round(squares[index].rent*inflation), (int)Math.Round(squares[index].house1*inflation),
           (int)Math.Round(squares[index].house2*inflation), (int)Math.Round(squares[index].house3*inflation),
            (int)Math.Round(squares[index].house4*inflation), (int)Math.Round(squares[index].hotel*inflation),
            (int)Math.Round(squares[index].housing_price*inflation), type, index};
            gamecard.set_cardd(int_array);
            if (square.owner == -1)
            {
                buy_auct_panel.Popup();
            }
            else if (square.owner == player.actor_ID)
            {
                buy_or_auction_happened();
            }
            else if (square.ismortgaged == false)
            {
                GameObject temp = GameObject.Find(square.owner.ToString());
                Monopoly_Player temp_player = temp.GetComponent<Monopoly_Player>();
                int rent = 0;
                if (squares[index].type == "property")
                {
                    if (squares[index].house_count == 4)
                    {
                        rent = squares[index].house4;
                    }
                    else if (squares[index].house_count == 3)
                    {
                        rent = squares[index].house3;
                    }
                    else if (squares[index].house_count == 2)
                    {
                        rent = squares[index].house2;
                    }
                    if (squares[index].house_count == 4)
                    {
                        rent = squares[index].house4;
                    }
                    else if (squares[index].house_count == 3)
                    {
                        rent = squares[index].house3;
                    }
                    else if (squares[index].house_count == 2)
                    {
                        rent = squares[index].house2;
                    }
                    else if (squares[index].house_count == 1)
                    {
                        rent = squares[index].house1;
                    }
                    else if (squares[index].ishousable)
                    {
                        rent = 2 * squares[index].rent;
                    }
                    else
                    {
                        rent = squares[index].rent;
                    }
                }
                else if (squares[index].type == "railroad")
                {
                    int count = 0;
                    foreach (Square item in squares)
                    {
                        if (item.type == "railroad" && item.owner == temp_player.actor_ID)
                        {
                            count++;
                        }
                    }
                    if (count == 4)
                    {
                        rent = squares[index].house3;
                    }
                    if (count == 3)
                    {
                        rent = squares[index].house2;
                    }
                    if (count == 2)
                    {
                        rent = squares[index].house1;
                    }
                    if (count == 1)
                    {
                        rent = squares[index].rent;
                    }
                    if (doublePayRailroad)
                    {
                        rent *= 2;
                    }
                }

                else if (squares[index].type == "utility")
                {
                    int count = 0;
                    foreach (Square item in squares)
                    {
                        if (item.type == "utility" && item.owner == temp_player.actor_ID)
                        {
                            count++;
                        }
                    }
                    if (tenPayUtility)
                    {
                        rent = (dicenum1 + dicenum2) * 10;
                    }
                    else
                    {
                        if (count == 1)
                        {
                            rent = (dicenum1 + dicenum2) * 4;
                        }
                        else if (count == 2)
                        {
                            rent = (dicenum1 + dicenum2) * 10;
                        }
                    }
                    
                }
                //burasý
                if(player.loyality_rate > 0)
                {
                    rent = rent * (100 - player.loyality_rate) / 100;

                }
                int[] temp_array = { temp_player.actor_ID, rent };
                gamelogic.GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp_array);
                if (check_if_rent_payable(rent)) // deðiþti
                {
                    temp_array[0] = player.actor_ID;
                    GetComponent<PhotonView>().RPC("increase_loyality_rate", RpcTarget.All, player.actor_ID.ToString()); //bu satýr
                    GameObject.Find(player.actor_ID.ToString() + "p").GetComponent<PlayerPropertyItem>().GetComponent<PhotonView>().RPC("set_loyality_rate", RpcTarget.All, player.actor_ID.ToString());
                    gamelogic.GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp_array);
                    buy_or_auction_happened();
                }
                else
                {
                    temp_array[0] = player.actor_ID;
                    GetComponent<PhotonView>().RPC("increase_loyality_rate", RpcTarget.All, player.actor_ID.ToString()); //bu satýr
                    GameObject.Find(player.actor_ID.ToString() + "p").GetComponent<PlayerPropertyItem>().GetComponent<PhotonView>().RPC("set_loyality_rate", RpcTarget.All, player.actor_ID.ToString());
                    gamelogic.GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp_array);
                }
            }
        }
        else if (square.type == "tax")
        {
            int[] temp_array = { player.actor_ID, square.value };
            if (check_if_rent_payable(square.value))
            {
                gamelogic.GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp_array);
                buy_or_auction_happened();
            }
            else
            {
                gamelogic.GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp_array);
                // mortgage or bankcruptcy
            }
        }
        else if (square.type == "gotojail")
        {
            player.GetComponent<PhotonView>().RPC("go_to_jail", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber.ToString());
            buy_or_auction_happened();
        }
        else if (square.type == "chance")
        {
            chance_panel.Popup();
            buy_or_auction_happened();

        }
        else if (square.type == "comm")
        {
            chest_panel.Popup();
            buy_or_auction_happened();


        }
        else
        {
            buy_or_auction_happened();
        }
    }

    IEnumerator func1()
    {
        price_show.update_inflation(inflation);
        price_show.update_prices();

        dice_rolled = true;
        BuyHouseButton.interactable = false;
        BuyHotelButton.interactable = false;
        unmortgageButton.interactable = false;
        TradeButton.interactable = false;
        //Roll the dice
        yield return StartCoroutine(dices.roll_dice());


        dicenum1 = dices.leftDiceNum + 1;
        dicenum2 = dices.rightDiceNum + 1;
        same_dice = dicenum1 == dicenum2;


        bool can_move = true;

        if (player.isinjail)
        {
            player.jail_turns--;
            if (player.jail_turns == 0)
            {
                can_move = true;
                player.isinjail = false;
            }
            else
            {
                if (same_dice)
                {
                    can_move = true;
                }
                else
                {
                    can_move = false;
                }
            }
        }
        if (can_move)
        {
            player.move(dicenum1 + dicenum2);
        }
        //Start Coroutine because otherwise it wont wait for the animation to be over
        yield return StartCoroutine(afterLanding());
    }

    public void buy_or_auction_happened()
    {
        if (same_dice)
        {
            dice_rolled = false;
            RollDiceButton.interactable = true;
            EndTurnButton.interactable = false;
        }

        
        else
        {
            dice_rolled = true;
            RollDiceButton.interactable = false;
            EndTurnButton.interactable = true;
        }
    }

    void endTurn()
    {
        if (Cardarea.activeInHierarchy)
        {
            gamecard.GetComponent<PhotonView>().RPC("deactivate", RpcTarget.All);
        }
        
        if (whoseTurn == PhotonNetwork.PlayerList.Length - 1)
        {
            gamelogic.GetComponent<PhotonView>().RPC("set_turn", RpcTarget.All, 0);
        }
        else if (whoseTurn != PhotonNetwork.PlayerList.Length - 1)
        {
            gamelogic.GetComponent<PhotonView>().RPC("set_turn", RpcTarget.All, whoseTurn + 1);
        }
        EndTurnButton.interactable = false;
        dice_rolled = false;

        randomEventCheck();
        gamelogic.GetComponent<PhotonView>().RPC("UpdateInflation", RpcTarget.All);
    }

    public void open_player_menu_for_trade()
    {
        BuyHouseButton.interactable = false;
        BuyHotelButton.interactable = false;
        mortgageButton.interactable = false;
        unmortgageButton.interactable = false;
        SellHotelButton.interactable = false;
        BuyHouseButton.interactable = false;
        TradeButton.interactable = false;
        RollDiceButton.interactable = false;
        buy_button_clicked = true;
        scrollview.SetActive(true);
        CloseListButton.SetActive(true);
        CloseListButton.GetComponent<CloseListButton>().if_instentiated_for_trade = true;

        foreach (Player item in PhotonNetwork.PlayerListOthers)
        {
            GameObject temp_object = GameObject.Find(item.ActorNumber.ToString());
            string playername = temp_object.GetComponent<Monopoly_Player>().nickname;
            int actor_id = temp_object.GetComponent<Monopoly_Player>().actor_ID;
            TradeItem temp = Instantiate(TradeItemprefab, contentObject);
            temp.SetPlayerName(playername);
            temp.SetPlayeryID(actor_id.ToString());
            TradeItemList.Add(temp);
        }
    }

    public void open_property_menu_for_mortgage()
    {
        BuyHouseButton.interactable = false;
        BuyHotelButton.interactable = false;
        mortgageButton.interactable = false;
        unmortgageButton.interactable = false;
        SellHotelButton.interactable = false;
        BuyHouseButton.interactable = false;
        TradeButton.interactable = false;
        buy_button_clicked = true;
        scrollview.SetActive(true);
        CloseListButton.SetActive(true);
        CloseListButton.GetComponent<CloseListButton>().if_instentiated_for_trade = false;

        foreach (int item in player.properties)
        {
            if (is_property_mortgageable(squares[item]))
            {
                BuildHouseItem temp = Instantiate(BuildHousePrefab, contentObject);
                temp.SetPropertyName(squares[item].name);
                temp.SetIndex(item);
                temp.if_house = false;
                temp.if_sell = false;
                temp.if_mortgage = true;
                temp.if_unmortgage = false;
                BuildHouseItemList.Add(temp);
            }
        }
    }

    public void open_property_menu_for_unmortgage()
    {
        BuyHouseButton.interactable = false;
        BuyHotelButton.interactable = false;
        mortgageButton.interactable = false;
        SellHotelButton.interactable = false;
        BuyHouseButton.interactable = false;
        unmortgageButton.interactable = false;
        TradeButton.interactable = false;
        buy_button_clicked = true;
        scrollview.SetActive(true);
        CloseListButton.SetActive(true);
        CloseListButton.GetComponent<CloseListButton>().if_instentiated_for_trade = false;

        foreach (int item in player.properties)
        {
            if (is_property_unmortgageable(squares[item]))
            {
                BuildHouseItem temp = Instantiate(BuildHousePrefab, contentObject);
                temp.SetPropertyName(squares[item].name);
                temp.SetIndex(item);
                temp.if_house = false;
                temp.if_sell = false;
                temp.if_mortgage = false;
                temp.if_unmortgage = true;
                BuildHouseItemList.Add(temp);
            }
        }
    }

    bool is_property_mortgageable(Square temp)
    {
        if (temp.ismortgaged == true)
        {
            return false;
        }
        for (int i = 0; i < squares.Length; i++)
        {
            if (temp.color == squares[i].color)
            {
                if (squares[i].house_count > 0)
                    return false;
            }
        }
        return true;
    }

    bool is_property_unmortgageable(Square temp)
    {
        if (temp.ismortgaged == true && player.money >= ((temp.value / 2) * 1.1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void open_property_menu_for_house()
    {
        BuyHouseButton.interactable = false;
        BuyHotelButton.interactable = false;
        mortgageButton.interactable = false;
        SellHotelButton.interactable = false;
        unmortgageButton.interactable = false;
        BuyHouseButton.interactable = false;
        TradeButton.interactable = false;
        buy_button_clicked = true;
        scrollview.SetActive(true);
        CloseListButton.SetActive(true);
        CloseListButton.GetComponent<CloseListButton>().if_instentiated_for_trade = false;

        foreach (int item in player.properties)
        {
            if (squares[item].housing_price <= player.money && squares[item].ishousable)
            {
                BuildHouseItem temp = Instantiate(BuildHousePrefab, contentObject);
                temp.SetPropertyName(squares[item].name);
                temp.SetIndex(item);
                temp.if_house = true;
                temp.if_sell = false;
                temp.if_mortgage = false;
                temp.if_unmortgage = false;
                BuildHouseItemList.Add(temp);
            }
        }
    }

    public void open_property_menu_for_hotel()
    {
        BuyHouseButton.interactable = false;
        BuyHotelButton.interactable = false;
        mortgageButton.interactable = false;
        SellHotelButton.interactable = false;
        unmortgageButton.interactable = false;
        BuyHouseButton.interactable = false;
        TradeButton.interactable = false;
        buy_button_clicked = true;
        scrollview.SetActive(true);
        CloseListButton.SetActive(true);
        CloseListButton.GetComponent<CloseListButton>().if_instentiated_for_trade = false;

        foreach (int item in player.properties)
        {
            if (squares[item].housing_price <= player.money && squares[item].ishotelable)
            {
                BuildHouseItem temp = Instantiate(BuildHousePrefab, contentObject);
                temp.SetPropertyName(squares[item].name);
                temp.SetIndex(item);
                temp.if_house = false;
                temp.if_sell = false;
                temp.if_mortgage = false;
                temp.if_unmortgage = false;
                BuildHouseItemList.Add(temp);
            }
        }
    }

    public void open_property_menu_for_house_sell()
    {
        BuyHouseButton.interactable = false;
        BuyHotelButton.interactable = false;
        mortgageButton.interactable = false;
        SellHotelButton.interactable = false;
        unmortgageButton.interactable = false;
        BuyHouseButton.interactable = false;
        TradeButton.interactable = false;
        CloseListButton.SetActive(true);
        CloseListButton.GetComponent<CloseListButton>().if_instentiated_for_trade = false;

        buy_button_clicked = true;
        scrollview.SetActive(true);
        foreach (int item in player.properties)
        {
            if (squares[item].house_count > 0 && squares[item].has_hotel == false)
            {
                if (check_house_sellability(squares[item]))
                {
                    BuildHouseItem temp = Instantiate(BuildHousePrefab, contentObject);
                    temp.SetPropertyName(squares[item].name);
                    temp.SetIndex(item);
                    temp.if_house = true;
                    temp.if_sell = true;
                    temp.if_mortgage = false;
                    temp.if_unmortgage = false;
                    BuildHouseItemList.Add(temp);
                }
            }
        }
    }
    bool check_house_sellability(Square temp)
    {
        if (temp.house_count == 0 || temp.has_hotel)
        {
            return false;
        }
        for (int i = 0; i < squares.Length; i++) {
            if (temp.color == squares[i].color && temp.name != squares[i].name)
            {
                if (temp.house_count < squares[i].house_count)
                    return false;
            }
        }
        return true;
    }



    public void open_property_menu_for_hotel_sell()
    {
        BuyHouseButton.interactable = false;
        BuyHotelButton.interactable = false;
        mortgageButton.interactable = false;
        SellHotelButton.interactable = false;
        BuyHouseButton.interactable = false;
        unmortgageButton.interactable = false;
        TradeButton.interactable = false;
        CloseListButton.SetActive(true);
        CloseListButton.GetComponent<CloseListButton>().if_instentiated_for_trade = false;

        buy_button_clicked = true;
        scrollview.SetActive(true);
        foreach (int item in player.properties)
        {
            if (squares[item].has_hotel)
            {
                BuildHouseItem temp = Instantiate(BuildHousePrefab, contentObject);
                temp.SetPropertyName(squares[item].name);
                temp.SetIndex(item);
                temp.if_house = false;
                temp.if_sell = true;
                temp.if_mortgage = false;
                temp.if_unmortgage = false;
                BuildHouseItemList.Add(temp);
            }
        }
    }

    bool check_hotel_sellability(Square temp)
    {
        if (temp.has_hotel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void close_trade_list()
    {
        foreach (TradeItem item in TradeItemList)
        {
            Destroy(item.gameObject);
        }
        TradeItemList.Clear();
        scrollview.SetActive(false);
        buy_button_clicked = false;
        CloseListButton.SetActive(false);
    }
    public void trade(string actor_id)
    {
        player.trade_offer_send = true;
        foreach (TradeItem item in TradeItemList)
        {
            Destroy(item.gameObject);
        }
        CloseListButton.SetActive(false);
        TradeItemList.Clear();
        scrollview.SetActive(false);
        string player_who_sends = player.nickname;
        string[] temp = { actor_id, player_who_sends, player.actor_ID.ToString() };
        GetComponent<PhotonView>().RPC("send_trade_request", RpcTarget.All, temp);
    }

    [PunRPC]
    public void send_trade_request(string[] array)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber.ToString() == array[0])
        {
            Monopoly_Player temp_player = GameObject.Find(array[0]).GetComponent<Monopoly_Player>();
            temp_player.trade_sender = array[2];
            Trade_image.SetActive(true);
            trade_Text.text = array[1] + " Send you a trade offer";
        }
    }

    public void close_list()
    {
        foreach (BuildHouseItem item in BuildHouseItemList)
        {
            Destroy(item.gameObject);
        }
        BuildHouseItemList.Clear();
        scrollview.SetActive(false);
        buy_button_clicked = false;
        CloseListButton.SetActive(false);
    }



    public void buyHouse(int index)
    {
        foreach (BuildHouseItem item in BuildHouseItemList)
        {
            Destroy(item.gameObject);
        }
        BuildHouseItemList.Clear();
        scrollview.SetActive(false);
        CloseListButton.SetActive(false);

        int actor_id = player.actor_ID;
        int value = squares[index].housing_price;
        int[] temp = { actor_id, value };
        GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp);
        int house_count = squares[index].house_count;
        GetComponent<PhotonView>().RPC("increase_house_count", RpcTarget.All, index);
        temp[0] = index;
        temp[1] = house_count;
        GetComponent<PhotonView>().RPC("instentiate_house", RpcTarget.All, temp);
        GetComponent<PhotonView>().RPC("set_housability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_housesellability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelsellability", RpcTarget.All);
        buy_button_clicked = false;
    }

    public void buyHotel(int index)
    {
        foreach (BuildHouseItem item in BuildHouseItemList)
        {
            Destroy(item.gameObject);
        }
        BuildHouseItemList.Clear();
        scrollview.SetActive(false);
        CloseListButton.SetActive(false);

        int actor_id = player.actor_ID;
        int value = squares[index].housing_price;
        int[] temp = { actor_id, value };
        GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp);
        int house_count = squares[index].house_count;
        GetComponent<PhotonView>().RPC("set_has_hotel_true", RpcTarget.All, index);
        temp[0] = index;
        temp[1] = house_count;
        GetComponent<PhotonView>().RPC("instentiate_hotel", RpcTarget.All, temp);
        GetComponent<PhotonView>().RPC("set_housability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_housesellability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelsellability", RpcTarget.All);
        buy_button_clicked = false;
    }
    public void sellHouse(int index)
    {
        foreach (BuildHouseItem item in BuildHouseItemList)
        {
            Destroy(item.gameObject);
        }
        BuildHouseItemList.Clear();
        scrollview.SetActive(false);
        CloseListButton.SetActive(false);

        int actor_id = player.actor_ID;
        int value = squares[index].housing_price / 2;
        int[] temp = { actor_id, value };
        GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp);
        int house_count = squares[index].house_count;
        GetComponent<PhotonView>().RPC("decrease_house_count", RpcTarget.All, index);
        temp[0] = index;
        temp[1] = house_count;
        GetComponent<PhotonView>().RPC("destroy_house", RpcTarget.All, temp);
        GetComponent<PhotonView>().RPC("set_housability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_housesellability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelsellability", RpcTarget.All);
        buy_button_clicked = false;
    }

    public void mortgage(int index)
    {
        foreach (BuildHouseItem item in BuildHouseItemList)
        {
            Destroy(item.gameObject);
        }
        BuildHouseItemList.Clear();
        scrollview.SetActive(false);
        CloseListButton.SetActive(false);

        int actor_id = player.actor_ID;
        int value = squares[index].value / 2;
        int[] temp = { actor_id, value };
        GetComponent<PhotonView>().RPC("giveMoney", RpcTarget.All, temp);
        temp[0] = index;
        temp[1] = 1;
        GetComponent<PhotonView>().RPC("set_mortgage_state", RpcTarget.All, temp);
        GetComponent<PhotonView>().RPC("set_housability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_housesellability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelsellability", RpcTarget.All);
        buy_button_clicked = false;
    }

    public void unmortgage(int index)
    {
        foreach (BuildHouseItem item in BuildHouseItemList)
        {
            Destroy(item.gameObject);
        }
        BuildHouseItemList.Clear();
        scrollview.SetActive(false);
        CloseListButton.SetActive(false);

        int actor_id = player.actor_ID;
        int value = squares[index].value / 2;
        value = (int)(value * 1.1);
        int[] temp = { actor_id, value };
        GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp);
        temp[0] = index;
        temp[1] = 0;
        GetComponent<PhotonView>().RPC("set_mortgage_state", RpcTarget.All, temp);
        GetComponent<PhotonView>().RPC("set_housability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_housesellability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelsellability", RpcTarget.All);
        buy_button_clicked = false;
    }



    public void sellHotel(int index)
    {
        foreach (BuildHouseItem item in BuildHouseItemList)
        {
            Destroy(item.gameObject);
        }
        BuildHouseItemList.Clear();
        scrollview.SetActive(false);
        CloseListButton.SetActive(false);

        int actor_id = player.actor_ID;
        int value = squares[index].housing_price;
        int[] temp = { actor_id, value };
        GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp);
        int house_count = squares[index].house_count;
        GetComponent<PhotonView>().RPC("set_has_hotel_false", RpcTarget.All, index);
        temp[0] = index;
        temp[1] = house_count;
        GetComponent<PhotonView>().RPC("destroy_hotel", RpcTarget.All, temp);
        for (int i = 0; i < 4; i++)
        {
            temp[1] = i;
            GetComponent<PhotonView>().RPC("instentiate_house", RpcTarget.All, temp);
        }
        GetComponent<PhotonView>().RPC("set_housability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_housesellability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_hotelsellability", RpcTarget.All);
        buy_button_clicked = false;
    }

    public bool check_if_buyable()
    {
        try
        {
            int price = (int)Math.Round(squares[player.index].value * inflation);
            bool cond1 = player.money >= price;
            return cond1;
        }
        catch (NullReferenceException)
        {
            return false;
        }
    }
    public bool check_if_rent_payable(int value)
    {
        if (player.money >= (int)Math.Round( inflation * value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool if_player_can_buy_house()
    {
        for (int i = 0; i < player.properties.Count; i++) {
            if (squares[player.properties[i]].ishousable && player.money >= (int)Math.Round(squares[player.properties[i]].housing_price * inflation))
            {
                return true;
            }
        }
        return false;
    }

    public bool if_player_can_sell_house()
    {
        for (int i = 0; i < player.properties.Count; i++)
        {
            if (squares[player.properties[i]].ishousesellable)
            {
                return true;
            }
        }
        return false;
    }

    public bool if_player_can_buy_hotel()
    {
        for (int i = 0; i < player.properties.Count; i++)
        {
            if (squares[player.properties[i]].ishotelable && player.money >= (int)Math.Round(squares[player.properties[i]].housing_price * inflation))
            {
                return true;
            }
        }
        return false;
    }

    public bool if_player_can_sell_hotel()
    {
        for (int i = 0; i < player.properties.Count; i++)
        {
            if (squares[player.properties[i]].has_hotel)
            {
                return true;
            }
        }
        return false;
    }

    public bool if_player_can_mortgage()
    {
        for (int i = 0; i < player.properties.Count; i++)
        {
            if (is_property_mortgageable(squares[player.properties[i]]))
            {
                return true;
            }
        }
        return false;
    }

    public bool if_player_can_unmortgage()
    {
        for (int i = 0; i < player.properties.Count; i++)
        {
            if (squares[player.properties[i]].ismortgaged == true)
            {
                if (player.money >= (int)Math.Round(((squares[player.properties[i]].value / 2) * 1.1 * inflation)))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool check_if_player_has_all_colors(int input)
    {
        if (input == 1 || input == 3)
        {
            int temp = 0;
            if (input == 1)
                temp = 3;
            else
                temp = 1;

            if (squares[input].owner == squares[temp].owner && squares[input].ismortgaged == false && squares[temp].ismortgaged == false)
            {
                if ((squares[input].house_count > squares[temp].house_count) || squares[input].house_count == 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 6 || input == 8 || input == 9)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 6)
            {
                temp = 8;
                temp2 = 9;
            }
            else if (input == 8)
            {
                temp = 6;
                temp2 = 9;
            }
            else {
                temp = 6;
                temp2 = 8;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if ((squares[input].house_count > squares[temp].house_count || squares[input].house_count > squares[temp2].house_count) || squares[input].house_count == 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 11 || input == 13 || input == 14)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 11)
            {
                temp = 13;
                temp2 = 14;
            }
            else if (input == 13)
            {
                temp = 11;
                temp2 = 14;
            }
            else
            {
                temp = 11;
                temp2 = 13;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if ((squares[input].house_count > squares[temp].house_count || squares[input].house_count > squares[temp2].house_count) || squares[input].house_count == 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 16 || input == 18 || input == 19)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 16)
            {
                temp = 18;
                temp2 = 19;
            }
            else if (input == 18)
            {
                temp = 16;
                temp2 = 19;
            }
            else
            {
                temp = 16;
                temp2 = 18;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if ((squares[input].house_count > squares[temp].house_count || squares[input].house_count > squares[temp2].house_count) || squares[input].house_count == 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 21 || input == 23 || input == 24)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 21)
            {
                temp = 23;
                temp2 = 24;
            }
            else if (input == 23)
            {
                temp = 21;
                temp2 = 24;
            }
            else
            {
                temp = 21;
                temp2 = 23;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if ((squares[input].house_count > squares[temp].house_count || squares[input].house_count > squares[temp2].house_count) || squares[input].house_count == 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 26 || input == 27 || input == 29)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 26)
            {
                temp = 27;
                temp2 = 29;
            }
            else if (input == 27)
            {
                temp = 26;
                temp2 = 29;
            }
            else
            {
                temp = 26;
                temp2 = 27;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if ((squares[input].house_count > squares[temp].house_count || squares[input].house_count > squares[temp2].house_count) || squares[input].house_count == 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 31 || input == 32 || input == 34)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 31)
            {
                temp = 32;
                temp2 = 34;
            }
            else if (input == 32)
            {
                temp = 31;
                temp2 = 34;
            }
            else
            {
                temp = 31;
                temp2 = 32;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if ((squares[input].house_count > squares[temp].house_count || squares[input].house_count > squares[temp2].house_count) || squares[input].house_count == 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 37 || input == 39)
        {
            int temp = 0;
            if (input == 37)
                temp = 39;
            else
                temp = 37;

            if (squares[input].owner == squares[temp].owner && squares[input].ismortgaged == false && squares[temp].ismortgaged == false)
            {
                if (squares[input].house_count > squares[temp].house_count || squares[input].house_count == 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool check_if_player_can_hotel(int input)
    {
        if (input == 1 || input == 3)
        {
            int temp = 0;
            if (input == 1)
                temp = 3;
            else
                temp = 1;

            if (squares[input].owner == squares[temp].owner && squares[input].ismortgaged == false && squares[temp].ismortgaged == false)
            {
                if (squares[input].house_count != 4 || squares[temp].house_count != 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 6 || input == 8 || input == 9)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 6)
            {
                temp = 8;
                temp2 = 9;
            }
            else if (input == 8)
            {
                temp = 6;
                temp2 = 9;
            }
            else
            {
                temp = 6;
                temp2 = 8;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if (squares[input].house_count != 4 || squares[temp].house_count != 4 || squares[temp2].house_count != 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 11 || input == 13 || input == 14)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 11)
            {
                temp = 13;
                temp2 = 14;
            }
            else if (input == 13)
            {
                temp = 11;
                temp2 = 14;
            }
            else
            {
                temp = 11;
                temp2 = 13;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if (squares[input].house_count != 4 || squares[temp].house_count != 4 || squares[temp2].house_count != 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 16 || input == 18 || input == 19)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 16)
            {
                temp = 18;
                temp2 = 19;
            }
            else if (input == 18)
            {
                temp = 16;
                temp2 = 19;
            }
            else
            {
                temp = 16;
                temp2 = 18;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if (squares[input].house_count != 4 || squares[temp].house_count != 4 || squares[temp2].house_count != 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 21 || input == 23 || input == 24)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 21)
            {
                temp = 23;
                temp2 = 24;
            }
            else if (input == 23)
            {
                temp = 21;
                temp2 = 24;
            }
            else
            {
                temp = 21;
                temp2 = 23;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if (squares[input].house_count != 4 || squares[temp].house_count != 4 || squares[temp2].house_count != 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 26 || input == 27 || input == 29)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 26)
            {
                temp = 27;
                temp2 = 29;
            }
            else if (input == 27)
            {
                temp = 26;
                temp2 = 29;
            }
            else
            {
                temp = 26;
                temp2 = 27;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if (squares[input].house_count != 4 || squares[temp].house_count != 4 || squares[temp2].house_count != 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 31 || input == 32 || input == 34)
        {
            int temp = 0;
            int temp2 = 0;
            if (input == 31)
            {
                temp = 32;
                temp2 = 34;
            }
            else if (input == 32)
            {
                temp = 31;
                temp2 = 34;
            }
            else
            {
                temp = 31;
                temp2 = 32;
            }
            if ((squares[input].owner == squares[temp].owner && squares[input].owner == squares[temp2].owner) &&
                squares[input].ismortgaged == false && squares[temp].ismortgaged == false && squares[temp2].ismortgaged == false)
            {
                if (squares[input].house_count != 4 || squares[temp].house_count != 4 || squares[temp2].house_count != 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else if (input == 37 || input == 39)
        {
            int temp = 0;
            if (input == 37)
                temp = 39;
            else
                temp = 37;

            if (squares[input].owner == squares[temp].owner && squares[input].ismortgaged == false && squares[temp].ismortgaged == false)
            {
                if (squares[input].house_count != 4 || squares[temp].house_count != 4 || squares[input].has_hotel == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    [PunRPC]
    public void BuyProperty(int actor_id)
    {
        GameObject temp = GameObject.Find(actor_id.ToString());
        Monopoly_Player temp_player = temp.GetComponent<Monopoly_Player>();
        int index = temp_player.index;
        temp_player.properties.Add(index);
        squares[index].owner = actor_id;
        temp_player.money -= (int)Math.Round(squares[index].value * inflation);
    }

    [PunRPC]
    public void set_mortgage_state(int[] arr)
    {
        if (arr[1] == 0)
        {
            squares[arr[0]].ismortgaged = false;
        }
        else
        {
            squares[arr[0]].ismortgaged = true;
        }
    }

    [PunRPC]
    public void set_housability()
    {
        for (int i = 0; i < 40; i++) {
            bool temp = check_if_player_has_all_colors(i);
            squares[i].ishousable = temp;
        }
    }

    [PunRPC]
    public void set_housesellability()
    {
        for (int i = 0; i < 40; i++)
        {
            if (squares[i].type == "property")
            {
                bool temp = check_house_sellability(squares[i]);
                squares[i].ishousesellable = temp;
            }
        }
    }

    [PunRPC]
    public void set_hotelsellability()
    {
        for (int i = 0; i < 40; i++)
        {
            if (squares[i].type == "property")
            {
                bool temp = check_hotel_sellability(squares[i]);
                squares[i].ishotelsellable = temp;
            }
        }
    }

    [PunRPC]
    public void set_hotelability()
    {
        for (int i = 0; i < 40; i++)
        {
            bool temp = check_if_player_can_hotel(i);
            squares[i].ishotelable = temp;
        }
    }

    [PunRPC]
    public void increase_house_count(int index)
    {
        GameObject temp = GameObject.Find("GameLogic");
        GameLogic temp_logic = temp.GetComponent<GameLogic>();
        temp_logic.squares[index].house_count++;
    }

    [PunRPC]
    public void decrease_house_count(int index)
    {
        GameObject temp = GameObject.Find("GameLogic");
        GameLogic temp_logic = temp.GetComponent<GameLogic>();
        temp_logic.squares[index].house_count--;
    }

    [PunRPC]
    public void set_has_hotel_true(int index)
    {
        GameObject temp = GameObject.Find("GameLogic");
        GameLogic temp_logic = temp.GetComponent<GameLogic>();
        temp_logic.squares[index].has_hotel = true;
    }

    [PunRPC]
    public void set_has_hotel_false(int index)
    {
        GameObject temp = GameObject.Find("GameLogic");
        GameLogic temp_logic = temp.GetComponent<GameLogic>();
        temp_logic.squares[index].has_hotel = false;
    }



    [PunRPC]
    public void instentiate_house(int[] array)
    {
        int count = array[0] * 4;
        count = count + array[1];
        House temp = Instantiate(HousePrefab, house_positions[count], Quaternion.identity);
        temp.index = array[0];
        temp.house_order = array[1];
        HouseList.Add(temp);
    }
    [PunRPC]
    public void destroy_house(int[] array)
    {
        int house_count = HouseList.Count;
        for (int i = 0; i < house_count; i++)
        {
            if (HouseList[i].index == array[0] && HouseList[i].house_order == array[1] - 1)
            {
                House house_to_remove = HouseList[i];
                Destroy(house_to_remove.gameObject);
                HouseList.Remove(house_to_remove);
                break;
            }
        }
    }

    [PunRPC]
    public void instentiate_hotel(int[] array)
    {
        int house_count = HouseList.Count;
        for (int i = 0; i < house_count; i++)
        {
            if (HouseList[i].index == array[0])
            {
                House house_to_remove = HouseList[i];
                Destroy(house_to_remove.gameObject);
                HouseList.Remove(house_to_remove);
                house_count--;
                i--;
            }
        }
        int count = array[0] * 4;
        Hotel temp = Instantiate(HotelPrefab, house_positions[count + 2], Quaternion.identity);
        temp.index = array[0];
        HotelList.Add(temp);
    }

    [PunRPC]
    public void destroy_hotel(int[] array)
    {
        int hotel_count = HotelList.Count;
        for (int i = 0; i < hotel_count; i++)
        {
            if (HotelList[i].index == array[0])
            {
                squares[array[0]].has_hotel = false;
                Hotel hotel_to_remove = HotelList[i];
                Destroy(hotel_to_remove.gameObject);
                HotelList.Remove(hotel_to_remove);
                break;
            }
        }
    }


    [PunRPC]
    public void set_turn(int given_turn)
    {
        whoseTurn = given_turn;
    }

    [PunRPC]
    public void giveMoney(int[] array)
    {
        GameObject temp = GameObject.Find(array[0].ToString());
        Monopoly_Player money_given_player = temp.GetComponent<Monopoly_Player>();
        money_given_player.money += (int)Math.Round(array[1] * inflation);
    }

    [PunRPC]
    public void take_money(int[] array)
    {
        GameObject temp = GameObject.Find(array[0].ToString());
        Monopoly_Player money_given_player = temp.GetComponent<Monopoly_Player>();
        money_given_player.money -= (int)Math.Round(array[1] * inflation);
    }

    [PunRPC]
    public void set_update(string actor_number)
    {
        string tempstr = actor_number + "p";
        GameObject temp = GameObject.Find(tempstr);
        PlayerPropertyItem temp_item = temp.GetComponent<PlayerPropertyItem>();
        temp_item.update_properties = true;

    }

    public bool istradeable(Square temp)
    {
        for (int i = 0; i < squares.Length; i++)
        {
            if (squares[i].color == temp.color && squares[i].house_count > 0)
            {
                return false;
            }
        }
        return true;
    }

    public void accept_trade_offer()
    {
        Trade_image.SetActive(false);
        string[] temp = { player.actor_ID.ToString(), player.trade_sender };
        GetComponent<PhotonView>().RPC("accept_trade_offer", RpcTarget.All, temp);
    }
    public void reject_trade_offer()
    {
        Trade_image.SetActive(false);
        GetComponent<PhotonView>().RPC("reject_trade_offer", RpcTarget.All, player.trade_sender);
    }

    [PunRPC]
    public void accept_trade_offer(string[] temp)
    {
        GameObject temp_object = GameObject.Find("GameLogic");
        GameLogic temp_logic = temp_object.GetComponent<GameLogic>();
        temp_logic.trader1 = GameObject.Find(temp[0]).GetComponent<Monopoly_Player>();
        temp_logic.trader2 = GameObject.Find(temp[1]).GetComponent<Monopoly_Player>();
        trade_area.SetActive(true);
        // 0 inputs
        Text temp_text = GameObject.Find("Player1money").GetComponent<Text>();
        temp_text.text = 0.ToString();
        temp_text = GameObject.Find("Player2money").GetComponent<Text>();
        temp_text.text = 0.ToString();
        player1_money.text = 0.ToString();
        player2_money.text = 0.ToString();

        Monopoly_Player temp_player1 = GameObject.Find(temp[0]).GetComponent<Monopoly_Player>();
        Monopoly_Player temp_player2 = GameObject.Find(temp[1]).GetComponent<Monopoly_Player>();
        for (int i = 0; i < temp_player1.properties.Count; i++)
        {
            if (istradeable(squares[temp_player1.properties[i]]))
            {
                Square square_temp = squares[temp_player1.properties[i]];
                TradePropertyItem temp_item = Instantiate(tradepropertyitemprefab, trade1_area);
                temp_item.set_name(square_temp.name);
                temp_item.set_index(temp_player1.properties[i]);
                temp_item.set_trader_or_not(0);
                temp_item.set_clicker(temp[0]);
                temp_item.set_player(0);
                TradepropertyItemList1.Add(temp_item);
            }
        }
        for (int i = 0; i < temp_player2.properties.Count; i++)
        {
            if (istradeable(squares[temp_player2.properties[i]]))
            {
                Square square_temp = squares[temp_player2.properties[i]];
                TradePropertyItem temp_item = Instantiate(tradepropertyitemprefab, trade4_area);
                temp_item.set_name(square_temp.name);
                temp_item.set_index(temp_player2.properties[i]);
                temp_item.set_trader_or_not(0);
                temp_item.set_clicker(temp[1]);
                temp_item.set_player(1);
                TradepropertyItemList4.Add(temp_item);
            }
        }
        ConfirmButton.SetActive(false);
        if (PhotonNetwork.LocalPlayer.ActorNumber.ToString() == temp[1] || PhotonNetwork.LocalPlayer.ActorNumber.ToString() == temp[0])
        {
            ConfirmButton.SetActive(true);
            CloseTradeButton.SetActive(true);
        }

        set_for_player1.set_clicker(temp[0]);
        set_for_player1.set_player(0);
        set_for_player2.set_clicker(temp[1]);
        set_for_player2.set_player(1);
    }


    public void close_trade_onclick()
    {
        GetComponent<PhotonView>().RPC("close_trade", RpcTarget.All);
    }

    [PunRPC]
    public void close_trade()
    {
        trade_area.SetActive(false);
        foreach (TradePropertyItem item in TradepropertyItemList1)
        {
            Destroy(item.gameObject);
        }
        foreach (TradePropertyItem item in TradepropertyItemList2)
        {
            Destroy(item.gameObject);
        }
        foreach (TradePropertyItem item in TradepropertyItemList3)
        {
            Destroy(item.gameObject);
        }
        foreach (TradePropertyItem item in TradepropertyItemList4)
        {
            Destroy(item.gameObject);
        }
        GameObject temp = GameObject.Find("GameLogic");
        GameLogic logic = temp.GetComponent<GameLogic>();
        TradepropertyItemList1.Clear();
        TradepropertyItemList2.Clear();
        TradepropertyItemList3.Clear();
        TradepropertyItemList4.Clear();
        trade_area.SetActive(false);
        trade_confirmed_count = 0;
        trader2.trade_offer_send = false;
        logic.buy_button_clicked = false;
        CloseTradeButton.SetActive(false);

    }

    public void change_position(int index, int new_position, int player)
    {
        int[] temp = { index, new_position, player };
        GetComponent<PhotonView>().RPC("change_positions", RpcTarget.All, temp);
    }

    public void set_for_money(int player_who)
    {
        if (player_who == 0)
        {
            int money = int.Parse(player1_money.text);
            if (money > player.money)
            {

            }
            else
            {
                int[] temp = { 0, money };
                GetComponent<PhotonView>().RPC("set_money_show", RpcTarget.All, temp);
            }
        }
        else
        {
            int money = int.Parse(player2_money.text);
            if (money > player.money)
            {

            }
            else
            {
                int[] temp = { 1, money };
                GetComponent<PhotonView>().RPC("set_money_show", RpcTarget.All, temp);
            }
        }
    }

    [PunRPC]
    public void set_money_show(int [] arr)
    {
        if (arr[0] == 0)
        {
            Text temp = GameObject.Find("Player1money").GetComponent<Text>();
            temp.text = arr[1].ToString();
            player1_money.text = 0.ToString();
        }
        else
        {
            Text temp = GameObject.Find("Player2money").GetComponent<Text>();
            temp.text = arr[1].ToString();
            player2_money.text = 0.ToString();
        }
    }


    [PunRPC]
    public void change_positions(int[] arr)
    {
        if (arr[2] == 0)
        {
            if (arr[1] == 0)
            {
                string clicker = "";
                for (int i = 0; i < TradepropertyItemList2.Count; i++)
                {
                    TradePropertyItem temp = TradepropertyItemList2[i];
                    clicker = temp.clicker;
                    TradepropertyItemList2.Remove(temp);
                    Destroy(temp.gameObject);
                }
                Square square_temp = squares[arr[0]];
                TradePropertyItem temp_item = Instantiate(tradepropertyitemprefab, trade1_area);
                temp_item.set_name(square_temp.name);
                temp_item.set_index(arr[0]);
                temp_item.set_trader_or_not(0);
                temp_item.set_clicker(clicker);
                temp_item.set_player(arr[2]);
                TradepropertyItemList1.Add(temp_item);
            }
            else
            {
                string clicker = "";
                for (int i = 0; i < TradepropertyItemList1.Count; i++)
                {
                    TradePropertyItem temp = TradepropertyItemList1[i];
                    clicker = temp.clicker;
                    TradepropertyItemList1.Remove(temp);
                    Destroy(temp.gameObject);
                }
                Square square_temp = squares[arr[0]];
                TradePropertyItem temp_item = Instantiate(tradepropertyitemprefab, trade2_area);
                temp_item.set_name(square_temp.name);
                temp_item.set_index(arr[0]);
                temp_item.set_trader_or_not(1);
                temp_item.set_clicker(clicker);
                temp_item.set_player(arr[2]);
                TradepropertyItemList2.Add(temp_item);
            }
        }
        else
        {
            if (arr[1] == 0)
            {
                string clicker = "";
                for (int i = 0; i < TradepropertyItemList3.Count; i++)
                {
                    TradePropertyItem temp = TradepropertyItemList3[i];
                    clicker = temp.clicker;
                    TradepropertyItemList3.Remove(temp);
                    Destroy(temp.gameObject);
                }
                Square square_temp = squares[arr[0]];
                TradePropertyItem temp_item = Instantiate(tradepropertyitemprefab, trade4_area);
                temp_item.set_name(square_temp.name);
                temp_item.set_index(arr[0]);
                temp_item.set_trader_or_not(0);
                temp_item.set_clicker(clicker);
                temp_item.set_player(arr[2]);
                TradepropertyItemList4.Add(temp_item);
            }
            else
            {
                string clicker = "";
                for (int i = 0; i < TradepropertyItemList4.Count; i++)
                {
                    TradePropertyItem temp = TradepropertyItemList4[i];
                    clicker = temp.clicker;
                    TradepropertyItemList4.Remove(temp);
                    Destroy(temp.gameObject);
                }
                Square square_temp = squares[arr[0]];
                TradePropertyItem temp_item = Instantiate(tradepropertyitemprefab, trade3_area);
                temp_item.set_name(square_temp.name);
                temp_item.set_index(arr[0]);
                temp_item.set_trader_or_not(1);
                temp_item.set_clicker(clicker);
                temp_item.set_player(arr[2]);
                TradepropertyItemList3.Add(temp_item);
            }
        }
    }

    [PunRPC]
    public void reject_trade_offer(string sender)
    {
        Monopoly_Player temp_player = GameObject.Find(sender).GetComponent<Monopoly_Player>();
        temp_player.trade_offer_send = false;
        GameObject.Find("GameLogic").GetComponent<GameLogic>().buy_button_clicked = false;
    }

    public void increase_confirm_count()
    {
        ConfirmButton.SetActive(false);
        GetComponent<PhotonView>().RPC("increase_confirm_count_for_all", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_housability", RpcTarget.All);
        GetComponent<PhotonView>().RPC("set_update", RpcTarget.All, trader1.actor_ID.ToString());
        GetComponent<PhotonView>().RPC("set_update", RpcTarget.All, trader2.actor_ID.ToString());
    }


    [PunRPC]
    public void increase_confirm_count_for_all()
    {
        GameObject temp = GameObject.Find("GameLogic");
        GameLogic logic = temp.GetComponent<GameLogic>();
        logic.trade_confirmed_count++;

        if(logic.trade_confirmed_count == 2)
        {
            for(int i = 0; i < TradepropertyItemList2.Count; i++)
            {
                squares[TradepropertyItemList2[i].index].owner = trader2.actor_ID;
                trader2.properties.Add(TradepropertyItemList2[i].index);
                trader1.properties.Remove(TradepropertyItemList2[i].index);
            }
            for (int i = 0; i < TradepropertyItemList3.Count; i++)
            {
                squares[TradepropertyItemList3[i].index].owner = trader1.actor_ID;
                trader1.properties.Add(TradepropertyItemList3[i].index);
                trader2.properties.Remove(TradepropertyItemList3[i].index);
            }
            trader1.money = trader1.money - int.Parse(player1_trade_money.text);
            trader1.money = trader1.money + int.Parse(player2_trade_money.text);
            trader2.money = trader2.money - int.Parse(player2_trade_money.text);
            trader2.money = trader2.money + int.Parse(player1_trade_money.text);

            foreach (TradePropertyItem item in TradepropertyItemList1)
            {
                Destroy(item.gameObject);
            }
            foreach (TradePropertyItem item in TradepropertyItemList2)
            {
                Destroy(item.gameObject);
            }
            foreach (TradePropertyItem item in TradepropertyItemList3)
            {
                Destroy(item.gameObject);
            }
            foreach (TradePropertyItem item in TradepropertyItemList4)
            {
                Destroy(item.gameObject);
            }
            TradepropertyItemList1.Clear();
            TradepropertyItemList2.Clear();
            TradepropertyItemList3.Clear();
            TradepropertyItemList4.Clear();
            trade_area.SetActive(false);
            trade_confirmed_count = 0;
            trader2.trade_offer_send = false;
            logic.buy_button_clicked = false;
            CloseTradeButton.SetActive(false);
        }
    }


    public void go_bankrupt()
    {
        int actor_id = player.actor_ID;
        int turn_order = player.play_order;
        int[] temp = { actor_id, turn_order };
        GetComponent<PhotonView>().RPC("go_bankrupt_all", RpcTarget.All, temp);
        Application.Quit();
    }

    [PunRPC]
    public void go_bankrupt_all(int[] arr)
    {
        GameObject temp = GameObject.Find(arr[0].ToString());
        Monopoly_Player player_to_bankrupt = temp.GetComponent<Monopoly_Player>();
        for(int i = 0; i < player_to_bankrupt.properties.Count; i++)
        {
            if (squares[player_to_bankrupt.properties[i]].has_hotel){
                for(int a = 0; a < HotelList.Count; a++)
                {
                    if(HotelList[a].index == player_to_bankrupt.properties[i])
                    {
                        Hotel hotel_to_remove = HotelList[a];
                        Destroy(hotel_to_remove.gameObject);
                        HotelList.Remove(hotel_to_remove);
                        break;
                    }
                }
            }
            else if (squares[player_to_bankrupt.properties[i]].house_count > 0)
            {
                int house_count = HouseList.Count;
                for (int a = 0; a < house_count; a++)
                {
                    if (HouseList[a].index == player_to_bankrupt.properties[i])
                    {
                        House house_to_remove = HouseList[i];
                        Destroy(house_to_remove.gameObject);
                        HouseList.Remove(house_to_remove);
                        house_count--;
                        a--;
                    }
                }
            }
        }
        for(int i = 0; i < player_to_bankrupt.properties.Count; i++)
        {
            squares[player_to_bankrupt.properties[i]].house_count = 0;
            squares[player_to_bankrupt.properties[i]].has_hotel = false;
            squares[player_to_bankrupt.properties[i]].ishotelable = false;
            squares[player_to_bankrupt.properties[i]].ishotelsellable = false;
            squares[player_to_bankrupt.properties[i]].ishousable = false;
            squares[player_to_bankrupt.properties[i]].ismortgaged = false;
            squares[player_to_bankrupt.properties[i]].ishousesellable = false;
            squares[player_to_bankrupt.properties[i]].owner = -1;
        }
        GameObject player_property_to_destroy = GameObject.Find(arr[0].ToString() + "p");
        Destroy(player_property_to_destroy);

        if(player.play_order > player_to_bankrupt.play_order){
            player.play_order--;
        }
    }

    [PunRPC]
    public void BuyPropertyCustom(int actor_id, int price, int property_index)
    {
        GameObject temp = GameObject.Find(actor_id.ToString());
        Monopoly_Player temp_player = temp.GetComponent<Monopoly_Player>();
        temp_player.properties.Add(property_index);
        squares[property_index].owner = actor_id;
        temp_player.money -= price;
    }

    [PunRPC]
    public void OpenAllAuctions()
    {
        auctionPanel.gameObject.SetActive(true);
    }

    //Chance and Chest Functions
    [PunRPC]

    public void move_to_location(string[] array)
    {
        if (array[1] == "Railroad")
            doublePayRailroad = true;
        else if (array[1] == "Utility")
            tenPayUtility = true;

        GameObject temp = GameObject.Find(array[0]);
        Monopoly_Player player = temp.GetComponent<Monopoly_Player>();
        player.move(array[1]);
    }


    [PunRPC]
    public void move_back(int[] array)
    {
        GameObject temp = GameObject.Find(array[0].ToString());
        Monopoly_Player player = temp.GetComponent<Monopoly_Player>();
        player.move(array[1]);
    }



    [PunRPC]
    public void pay_property_cost(int[] array)
    {
        GameObject temp = GameObject.Find(array[0].ToString());
        Monopoly_Player player = temp.GetComponent<Monopoly_Player>();
        foreach (int item in player.properties)
        {
            int[] temp_array = { 0, 0 };
            if (squares[item].house_count > 0)
            {
                temp_array[0] = player.actor_ID;
                temp_array[1] = array[1] * squares[item].house_count;
                gamelogic.GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp_array);
            }
            else if (squares[item].has_hotel)
            {
                temp_array[0] = player.actor_ID;
                temp_array[1] = array[1];
                gamelogic.GetComponent<PhotonView>().RPC("take_money", RpcTarget.All, temp_array);
            }
        }
    }

    [PunRPC]

    public void go_to_jail_with_card(string actor_number)
    {
        GameObject temp = GameObject.Find(actor_number);
        Monopoly_Player player = temp.GetComponent<Monopoly_Player>();
        player.GetComponent<PhotonView>().RPC("go_to_jail", RpcTarget.All, actor_number);
    }

    [PunRPC]


    // bu func
    public void increase_loyality_rate(string actor_number)
    {
        GameObject temp = GameObject.Find(actor_number);
        Monopoly_Player temp_player = temp.GetComponent<Monopoly_Player>();
        temp_player.rent_paid_count += 1;
        if(temp_player.rent_paid_count % 5 == 0 && temp_player.loyality_rate != 20)
        {
            temp_player.loyality_rate += 5;
        }
    }





    [PunRPC]
    public void IncrementTurnCount()
    {
        turnCount++;
    }

    public void randomEventCheck()
    {
        //In order to prevent out of bound indices, take minimum of game_mode and frequency list's length
        int game_mode_index = Math.Min(game_mode, randomEventFrequencies.Length);
        if (turnCount % randomEventFrequencies[game_mode_index] == 0)
        {
            //Decide on the event
            int eventIndex = UnityEngine.Random.Range(0, RandomEvents.Length);
            //Decide on the property
            int propertyIndex = UnityEngine.Random.Range(0, 40);
            //Decide on how much the price/rents will change
            float multiplier = UnityEngine.Random.Range(0.5f, 2.0f);
            while (squares[propertyIndex].type != "property")
            {
                propertyIndex = (propertyIndex + 1) % 40;
            }
            gamelogic.GetComponent<PhotonView>().RPC(RandomEvents[eventIndex], RpcTarget.All, propertyIndex, multiplier);
        }
    }

    [PunRPC]
    public void AdjustRent(int property_index, float multiplier)
    {
        squares[property_index].rent = Mathf.RoundToInt(squares[property_index].rent * multiplier);
        squares[property_index].house1 = Mathf.RoundToInt(squares[property_index].house1 * multiplier);
        squares[property_index].house2 = Mathf.RoundToInt(squares[property_index].house2 * multiplier);
        squares[property_index].house3 = Mathf.RoundToInt(squares[property_index].house3 * multiplier);
        squares[property_index].house4 = Mathf.RoundToInt(squares[property_index].house4 * multiplier);
        squares[property_index].hotel = Mathf.RoundToInt(squares[property_index].hotel * multiplier);
        print("Adjusted rent of: " + property_index.ToString() + " by multiplying by: " + multiplier.ToString());
        print("New rent of: " + property_index.ToString() + " is: " + squares[property_index].rent.ToString());
    }

    [PunRPC]
    public void AdjustPrice(int property_index, float multiplier)
    {
        squares[property_index].value = Mathf.RoundToInt(squares[property_index].value * multiplier);
        print("Adjusted price of: " + property_index.ToString() + " by multiplying by: " + multiplier.ToString());
        print("New price of: " + property_index.ToString() + " is: " + squares[property_index].value.ToString());
    }

    [PunRPC]
    public void UpdateInflation()
    {
        int total_money = 0;
        for (int i = 0; i < 40; i++)
        {
            string property_type = squares[i].type;
            if (squares[i].owner != -1)
            {
                if (property_type == "property")
                {
                    total_money += squares[i].value + squares[i].house_count * squares[i].housing_price;
                }
                else if (property_type == "utility" || property_type == "railroad")
                {
                    total_money += squares[i].value;
                }
            }
        }
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            GameObject temp = GameObject.Find((i + 1).ToString());//Tekrar bak
            Monopoly_Player temp_player = temp.GetComponent<Monopoly_Player>();
            total_money += temp_player.money;
        }
        inflation = (float)total_money / (float)initialTotalMoney;
    }
}