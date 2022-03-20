using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.GeneratorGrid);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.GeneratorGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.PlayerDraw:
                PlayerDeck.Instance.DrawCard();
                break;
            case GameState.PlayerTurn:
                Debug.Log("PlayerTurn");

                //TurnSystem.Instance.EndYourOpponentTurn();
                TurnSystem.Instance.YourTurn();
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.EnemyAttackTurn:
                Debug.Log("EnemyTurn");
                break;
            case GameState.EnemyTurn:
                //HandleEnemyTurn();
                //TurnSystem.Instance.EndYourTurn();
                break;
            case GameState.PlayerAttackTurn:
                Debug.Log("PlayerAttackTurn");
                BaseUnit.Instance.MoveUnits();
                break;
            case GameState.Decide:
                HandleDecide();
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
    
    private void HandlePlayerTurn()
    {

    }
    private async void HandleEnemyTurn()
    {
        await Task.Delay(2000);

        //UnitManager.Instance.Attack(Faction.Enemy);

        await Task.Delay(2000);
    }
    private async void HandleDecide()
    {
        /*var units = FindObjectOfType<Unit>();
        await Task.Delay(500);

        if (units.Any(units => units.Faction == Faction.Enemy)) UpdateGameState(GameState.Victory);
        else if (units.Any(units => units.Faction == Faction.Player)) UpdateGameState(GameState.Lose);
        else UpdateGameState(GameState.PlayerTurn);*/
    }
}

public enum GameState
{
    GeneratorGrid,
    PlayerDraw,
    PlayerTurn,
    SpawnHeroes,
    EnemyAttackTurn,
    EnemyTurn,
    PlayerAttackTurn,
    Decide,
    Victory,
    Lose
}
