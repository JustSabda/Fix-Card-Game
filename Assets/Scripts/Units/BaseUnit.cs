using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BaseUnit : MonoBehaviour
{
    public static BaseUnit Instance;

    public string UnitName;
    [SerializeField]
    public Tile OccupiedTile;
    public Faction Faction;
    public List<Tile> TileList = new List<Tile>();

    private bool _isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;
    GameObject[] unitList;
    public Tile unitA;
    public bool Occupied;
    //public GameObject ThisUnit;
   
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void MoveUnits()
    {
        //unitA.SetUnit(this);
        await Task.Delay(500);
        //unitList = GameObject.FindGameObjectsWithTag("Units");
        
        if (GameManager.Instance.State == GameState.PlayerAttackTurn)
        {
            Debug.Log("UnitMoved : " + OccupiedTile);
            if (Occupied = true)
            {
                StartCoroutine(MovePlayer(Vector3.up));
            }
            
        }

        GameManager.Instance.UpdateGameState(GameState.PlayerDraw);
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
        //OccupiedTile = transform.position;

        _isMoving = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Units") Occupied = true;
        Debug.Log(collision.gameObject.name);
        OccupiedTile = collision.gameObject.GetComponent<Tile>();
        //Tile._isWalkable = false;
        //Debug.Log(Tile._isWalkable);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Units") Occupied = false;
        //Tile._isWalkable = true;
    }
}
