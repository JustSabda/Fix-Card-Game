using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    private void Awake()
    {
        // Id , nama , cost , Power, deskripsi , Image , DrawCard , CurrentMana++ , HealBase , Movement  
        cardList.Add(new Card(0, "Cecep", 1 , 2, "None",Resources.Load<Sprite>("Kian Santang"),0,1,1,1));
        cardList.Add(new Card(1, "Semprul", 2, 5, "None", Resources.Load<Sprite>("Sutawijaya"),0,1,1,1));
        cardList.Add(new Card(2, "Bambang", 3, 1, "None",Resources.Load<Sprite>("Sultan Agung"),0,1,1,1));
        cardList.Add(new Card(3, "Wiluyo", 2, 1, "None", Resources.Load<Sprite>("Ken Arok"),0,1,1,1));
        cardList.Add(new Card(4, "Asep", 1, 1, "None", Resources.Load<Sprite>("Siliwangi"),0,1,1,1));
    }
}
