using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public static Tile Instance;
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] public bool _isWalkable;

    Vector2 truePos;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    void awake()
    {
        Instance = this;
    }
    
    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }
    void OnMouseOver()
    {
        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
        }
    }
    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
    
    public void SetUnit(BaseUnit unit)
    {
        if (GameManager.Instance.State == GameState.SpawnHeroes )
        {
            if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
            //BaseUnit.Instance.MoveUnits();
            unit.transform.position = transform.position;
            //OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }
        /*else if (GameManager.Instance.State == GameState.PlayerAttackTurn)
        {
            if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
            unit.transform.position = transform.position;
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }
        
        if (GameManager.Instance.State == GameState.PlayerAttackTurn)
        {
            unit.OccupiedTile = this;
        }*/
        
    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeholderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.parentToReturnTo = this.transform;

        }

        if (UnitManager.Instance.SelectedHero != null)
        {
            SetUnit(UnitManager.Instance.SelectedHero);
            UnitManager.Instance.SetSelectedHero(null);
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        OccupiedUnit = collision.gameObject.GetComponent<BaseUnit>();
        Debug.Log(collision.gameObject.GetComponent<BaseUnit>());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OccupiedUnit = null;
        Debug.Log(OccupiedUnit);
    }
}
