using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public static PlayerDeck Instance;

    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public static List<Card> staticDeck = new List<Card>(); 

    public int x;
    public static int deckSize = 15;

    //public GameObject cardInDeck1;
    //public GameObject cardInDeck2;
    //public GameObject cardInDeck3;
    //public GameObject cardInDeck4;

    public GameObject CardToHand;
    public GameObject Hand;
    GameObject[] handList;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void a()
    {
        x = 0;
        deckSize = 15;

        for(int i = 0;i < deckSize; i++)
        {

            x = Random.Range(0, 4);
            deck[i] = CardDataBase.cardList[x];

        }

        StartCoroutine(StartGame());
        //GameManager.Instance.UpdateGameState(GameState.PlayerTurn);

        /*if (ThisCard.drawX > 0)
        {
            StartCoroutine(Draw(ThisCard.drawX));
            ThisCard.drawX = 0;
        }
        if (TurnSystem.startTurn == true)
        {
            StartCoroutine(Draw(TurnSystem.DrawCount));
            TurnSystem.startTurn = false;
            TurnSystem.DrawCount = 0;
        }*/
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }

    // Update is called once per frame
    public void Update()
    {
        staticDeck = deck;
        //if (deckSize < 30)
        {
            //cardInDeck1.SetActive(false);
        }
        //if (deckSize < 20)
        {
            //cardInDeck1.SetActive(false);
        }
        //if (deckSize < 10)
        {
            //cardInDeck1.SetActive(false);
        }
        //if (deckSize < 5)
        {
            //cardInDeck1.SetActive(false);
        }


        /*if(ThisCard.drawX > 0)
        {
            StartCoroutine(Draw(ThisCard.drawX));
            ThisCard.drawX = 0;
        }
        if (TurnSystem.startTurn == true)
        {
            StartCoroutine(Draw(TurnSystem.DrawCount));
            TurnSystem.startTurn = false;
            TurnSystem.DrawCount = 0;
        }*/
        //GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }
    public void DrawCard()
    {
        Debug.Log("Start Section Draw");
        handList = GameObject.FindGameObjectsWithTag("CardHand");
        x = 0;
        deckSize = 15;
        
        if (0 <= handList.Length && handList.Length < 3 )
        {
            int gap;
            gap = 4 - handList.Length;
            for (int i = 0; i < deckSize; i++)
            {

                x = Random.Range(0, 4);
                deck[i] = CardDataBase.cardList[x];
            }

            //StartCoroutine(StartGame());
            StartCoroutine(Draw(gap));
            Debug.Log("Finish Section Draw b");
            GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
        }
        else
        {
            Debug.Log("No Card to Draw");
            GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
        }

    }

    IEnumerator StartGame()
    {
        for(int i = 0; i <= 3; i++)
        {
            yield return new WaitForSeconds(1);
            Instantiate(CardToHand, transform.position, transform.rotation);
            
            
        }
    }
    IEnumerator Draw(int x)
    {
        for (int i = 0; i < x; i++)
        {
            yield return new WaitForSeconds(1);
            Instantiate(CardToHand, transform.position, transform.rotation);
        }
    }
    public void Shuffle()
    {
        for(int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }

    }
        
}
