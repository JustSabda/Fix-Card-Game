using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class UnitManager : MonoBehaviour
{
    private List<ScriptableUnit> _units;
    public static UnitManager Instance;
    private bool _isMoving;

    public BaseHero SelectedHero;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;

    void Awake()
    {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }
    
    //This Section is where Card become Unit
    public void SpawnHeroes()
    {
        var heroCount = 1;

        for (int i = 0; i < heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            spawnedHero.transform.tag = "Units";
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

            /*spawnedHero.transform.position = randomSpawnTile.transform.position;
            randomSpawnTile.OccupiedUnit = spawnedHero;
            spawnedHero.OccupiedTile = randomSpawnTile;*/
            randomSpawnTile.SetUnit(spawnedHero);
        }
        GameManager.Instance.UpdateGameState(GameState.PlayerAttackTurn);
    }
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }
    public void SpawnEnemies()
    {

    }

    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        //MenuManager.Instance.ShowSelectedHero(hero);
    }
    public void MoveUnits()
    {
        if (GameManager.Instance.State == GameState.PlayerAttackTurn)
        {
            Debug.Log("UnitMoved");
            StartCoroutine(MovePlayer(Vector3.up));
        }
    }
    private IEnumerator MovePlayer(Vector3 direction)
    {
        _isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        _isMoving = false;
    }
}
