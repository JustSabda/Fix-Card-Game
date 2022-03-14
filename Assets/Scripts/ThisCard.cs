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

    public GameObject Target;
    public GameObject Enemy;

    public GameObject attackBorder;
    public bool summoningSickness;
    public bool cantAttack;

    public bool canAttack;

    public static bool staticTargeting;
    public static bool staticTargetEnemy;

    public bool targeting;
    public bool targetingEnemy;

    public bool onlyThisCardAttack;

    public int healSpell;
    public bool canHeal;


    void Start()
    {
        CardBackScript = GetComponent<CardBack>();
        thisCard[0] = CardDataBase.cardList[thisId];
        numberOfCardsInDeck = PlayerDeck.deckSize;

        canBeSummon = false;
        summoned = false;

        drawX = 0;
        canAttack = false;
        summoningSickness = true;

        Enemy = GameObject.Find("EnemyHP");

        targeting = false;
        targetingEnemy = false;
        canHeal = true;
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

        healSpell = thisCard[0].healBase;

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
            if (canAttack == true)
            {
                attackBorder.SetActive(true);
            }
            else
            {
                attackBorder.SetActive(false);
            }
        }


        if (TurnSystem.isYourTurn == false && summoned == true)
        {
            summoningSickness = false;
            cantAttack = false;
        }
        if (TurnSystem.isYourTurn ==true&&summoningSickness == false && cantAttack == false)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        targeting = staticTargeting;
        targetingEnemy = staticTargetEnemy;
        
        if (targetingEnemy == true)
        {
            Target = Enemy;
        }
        else
        {
            Target = null;
        }

        if(targeting == true && targetingEnemy == true && onlyThisCardAttack == true)
        {
            Attack();
        }
        if(canHeal == true && summoned == true)
        {
            Heal();
            canHeal = false;
        }
    }
    public void Summon()
    {
        TurnSystem.currentMana -= cost;
        summoned = true;

        TurnSystem.currentMana += add_CurrentMana;
        TurnSystem.DrawCount += 1;
        //CurrentMana(add_CurrentMana);
        drawX = draw_cards;
    }
    public void CurrentMana(int x)
    {
        TurnSystem.currentMana += x;
    }

    public void Attack()
    {
        if(canAttack == true&&summoned==true)
        {
            if(Target != null)
            {
                if (Target == Enemy)
                {
                    EnemyHP.staticHP -= power;
                    targeting = false;
                    cantAttack = true;
                }
                if (Target.name == "CardToHand(Clone)")
                {
                    canAttack = true;
                }
            }
        }
    }

    public void UnTargetEnemy()
    {
        staticTargetEnemy = false;
    }
    public void TargetEnemy()
    {
        staticTargetEnemy = true;
    }
    public void StartAttack()
    {
        staticTargeting = true;
    }
    public void StopAttack()
    {
        staticTargeting = false;
    }
    public void OneCardAttack()
    {
        onlyThisCardAttack = true;
    }
    public void OneCardAttackStop()
    {
        onlyThisCardAttack = false;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void Heal()
    {
        PlayerHP.staticHP += healSpell;
    }
}
