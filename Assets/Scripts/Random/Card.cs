using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Card
{
    public int id;
    public string cardName;
    public int cost;
    public int power;
    public string cardDescription;

    public Sprite thisImage;
    public int draw_Card;
    public int add_CurrentMana;

    public int healBase;
    public int move;

    public Card()
    {

    }

    public Card(int Id ,string CardName, int Cost, int Power, string CardDescription, Sprite ThisImage ,int Draw_Card , int Add_CurrentMana , int HealBase , int Move)
    {
        id = Id;
        cardName = CardName;
        cost = Cost;
        power = Power;
        cardDescription = CardDescription;
        thisImage = ThisImage;
        draw_Card = Draw_Card;
        add_CurrentMana = Add_CurrentMana;
        healBase = HealBase;
        move = Move;
    }
}
