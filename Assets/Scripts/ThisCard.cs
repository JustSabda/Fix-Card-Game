using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThisCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisId;

    public int id;
    public string cardName;
    public int cost;
    public int power;
    public string cardDescription;

    //public Text nameText;
    //public Text costText;
    //public Text powerText;
    //public Text descriptionText;

    public Sprite thisSprite;
    public Image thatImage;

    public bool cardBack;
    CardBack CardBackScript;

    public GameObject Hand;

    public int numberOfCardsInDeck;

    public bool canBeSummon;
    public bool summoned;
    public GameObject battleZone;

    public GameObject skinCare;

    public static int drawX;
    public int draw_cards;
    public int add_CurrentMana;

    void Start()
    {
        CardBackScript = GetComponent<CardBack>();
        thisCard[0] = CardDataBase.cardList[thisId];
        numberOfCardsInDeck = PlayerDeck.deckSize;

        canBeSummon = false;
        summoned = false;

        drawX = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        Hand = GameObject.Find("Hand");
        if(this.transform.parent == Hand.transform.parent)
        {
            cardBack = false;
        }
        id = thisCard[0].id;
        cardName = thisCard[0].cardName;
        cost = thisCard[0].cost;
        power = thisCard[0].power;
        cardDescription = thisCard[0].cardDescription;

        thisSprite = thisCard[0].thisImage;

        draw_cards = thisCard[0].draw_Card;
        add_CurrentMana = thisCard[0].add_CurrentMana;

        //nameText.text = "" + cardName;
        //costText.text = "" + cost;
        //powerText.text = "" + power;
        //descriptionText.text = "" + cardDescription;

        thatImage.sprite = thisSprite;

        CardBackScript.UpdateCard(cardBack);

        if(this.tag == "Clone")
        {
            thisCard[0] = PlayerDeck.staticDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            PlayerDeck.deckSize -= 1;
            cardBack = false;
            this.tag = "Untagged";
        }
        if (this.tag != "Deck")
        {
            if (TurnSystem.currentMana >= cost && summoned == false)
            {
                canBeSummon = true;
            }
            else
            {
                canBeSummon = false;
            }

            if (canBeSummon == true)
            {
                gameObject.GetComponent<Draggable>().enabled = true;
                skinCare.SetActive(true);
            }
            else
            {
                gameObject.GetComponent<Draggable>().enabled = false;
                skinCare.SetActive(false);
            }

            battleZone = GameObject.Find("Zone");

            if (summoned == false && this.transform.parent == battleZone.transform)
            {
                Summon();
            }
        }
    }
    public void Summon()
    {
        TurnSystem.currentMana -= cost;
        summoned = true;

        TurnSystem.currentMana += add_CurrentMana;
        //CurrentMana(add_CurrentMana);
        drawX = draw_cards;
    }
    public void CurrentMana(int x)
    {
        TurnSystem.currentMana += x;
    }

}
