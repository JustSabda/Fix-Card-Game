using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance;

    public static bool isYourTurn;
    public int yourTurn;
    public int yourOponentTurn;
    public Text turnText;

    public int maxMana;
    public static int currentMana;
    public Text manaText;

    public static bool startTurn;
    public static int DrawCount;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void a()
    {
        isYourTurn = true;
        yourTurn = 1;
        yourOponentTurn = 0;

        maxMana = 1;
        currentMana = 1;

        startTurn = false;
    }
    public void YourTurn()
    {
        isYourTurn = true;
        yourTurn += 1;
        //yourOponentTurn = 0;

        maxMana += 1;
        currentMana = maxMana;

        startTurn = false;
        Debug.Log("Your Turn");
    }

    // Update is called once per frame
    void Update()
    {
        if(isYourTurn == true)
        {
            turnText.text = "Your Turn";
        }
        else
        {
            turnText.text = "Enemy Turn";
        }

        manaText.text = currentMana + "/" + maxMana;
    }

    public void EndYourTurn()
    {
        if(GameManager.Instance.State != GameState.PlayerTurn) return;
        isYourTurn = false;
        yourOponentTurn += 1;
        //yourTurn += 1;

        //maxMana += 1;
        //currentMana = maxMana;

        GameManager.Instance.UpdateGameState(GameState.SpawnHeroes);
    }

    public void EndYourOpponentTurn()
    {
        isYourTurn = true;
        yourTurn += 1;

        maxMana += 1;
        currentMana = maxMana;

        startTurn = true;

        GameManager.Instance.UpdateGameState(GameState.PlayerAttackTurn);
    }
}
