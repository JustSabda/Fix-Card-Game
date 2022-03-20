using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject _endTurnButton, _endEnemyButton, _selectedHeroObject;

    
    private void Awake()
    {
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    public void ShowSelectedHero(BaseHero hero)
    {
        if(hero == null)
        {
            //_selectedHeroObject.SetActive(false);
            return;
        }
        //_selectedHeroObject.GetComponentInChildren<Text>().text = hero.UnitName;
        //_selectedHeroObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    void GameManagerOnOnGameStateChanged(GameState state)
    {
        

    }

    
    public void EndButtonPressed()
    {
        GameManager.Instance.UpdateGameState(GameState.EnemyTurn);
    }
}
