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

    public Sprite thisSpriteCard;
    public Image thatImage;

    public Sprite thisIkonCard;
    public Image thatIcon;

    public int move;

    public bool cardBack;
    CardBack CardBackScript;

    public GameObject Hand;

    public int numberOfCardsInDeck;

    public bool canBeSummon;
    public bool summoned;

    
    public GameObject battleZone;
    public GameObject battleZone2;
    public GameObject battleZone3;
    public GameObject battleZone4;
    public GameObject battleZone5;
    public GameObject battleZone6;
    public GameObject battleZone7;
    public GameObject battleZone8;

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

    public GameObject CardVisual;
    public GameObject IkonVisual;

    public int position;

    public GameObject[] Zone;

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
        if (this.tag != "Deck")
        {
            CardVisual.SetActive(true);
            IkonVisual.SetActive(false);
        }

        Zone = GameObject.FindGameObjectsWithTag("Zone");
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

        move = thisCard[0].move;
        


        thisSpriteCard = thisCard[0].thisImage;

        draw_cards = thisCard[0].draw_Card;
        add_CurrentMana = thisCard[0].add_CurrentMana;

        healSpell = thisCard[0].healBase;

        //nameText.text = "" + cardName;
        //costText.text = "" + cost;
        //powerText.text = "" + power;
        //descriptionText.text = "" + cardDescription;

        thatImage.sprite = thisSpriteCard;

        CardBackScript.UpdateCard(cardBack);

        if(this.tag == "Clone")
        {
            thisCard[0] = PlayerDeck.staticDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            PlayerDeck.deckSize -= 1;
            if(numberOfCardsInDeck == -1 && PlayerDeck.deckSize == -1)
            {
                numberOfCardsInDeck = 14;
                PlayerDeck.deckSize = 14;
            }
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
            battleZone2 = GameObject.Find("Zone1");
            battleZone3 = GameObject.Find("Zone2");
            battleZone4 = GameObject.Find("Zone3");
            battleZone5 = GameObject.Find("Zone4");
            battleZone6 = GameObject.Find("Zone5");
            battleZone7 = GameObject.Find("Zone6");
            battleZone8 = GameObject.Find("Zone7");

            if (summoned == false && (this.transform.parent == battleZone.transform|| this.transform.parent == battleZone2.transform || this.transform.parent == battleZone3.transform || this.transform.parent == battleZone4.transform || this.transform.parent == battleZone5.transform || this.transform.parent == battleZone6.transform|| this.transform.parent == battleZone7.transform || this.transform.parent == battleZone8.transform))
            {
                Summon();
            }
            if(this.transform.parent == battleZone.transform)
            {
                position = 1;
            }
            if(this.transform.parent == battleZone2.transform)
            {
                position = 2;
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
        if(TurnSystem.startTurn == true)
        {
            
        }
    }
    public void Summon()
    {
        TurnSystem.currentMana -= cost;
        summoned = true;

        TurnSystem.currentMana += add_CurrentMana;
        TurnSystem.DrawCount += 1;

        drawX = draw_cards;

        CardVisual.SetActive(false);
        IkonVisual.SetActive(true);

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
    IEnumerator Move(int x)
    {
        yield return new WaitForSeconds(1);
        
        //Debug.Log("Pindah Lek");
        //if (position == 1 && x == 1)
        {
            //this.transform.parent = battleZone4.transform;
            //position = 4;
        }
    }
}
