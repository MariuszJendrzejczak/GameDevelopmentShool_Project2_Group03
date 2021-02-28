using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.WSA;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    
    public Unit selectedUnit, attackedUnit;
    [SerializeField]
    private float moveStep = 0.04f;
    [SerializeField]
    private GameObject tilesContaainter, unitsContainer;
    [SerializeField]
    private List<GameObject> tilesList, unitsList;
    

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        /// wypełnianie list będzie wykonywane po UnitSelection state, w trakcie w trakcie Deployment State. 
        selectedUnit = null;
        for (int i = 0; i < tilesContaainter.transform.childCount; i++)
        {
            tilesList.Add(tilesContaainter.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < unitsContainer.transform.childCount; i++)
        {
            unitsList.Add(unitsContainer.transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        UnitSelectionMethod();
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (GameObject obj in unitsList)
            {
                obj.GetComponent<Unit>().NewTurnForUnit();
            }
        }
    }

    private void UnitSelectionMethod()
    {
        if (selectedUnit)
        {
            if (selectedUnit.canMove)
            {
                foreach (GameObject tile in tilesList)
                {
                    if (Mathf.Abs(selectedUnit.transform.position.x - tile.transform.position.x) + Mathf.Abs(selectedUnit.transform.position.y - tile.transform.position.y) <= selectedUnit.unitSpeed + 0.5f)
                    {
                        Tile obj = tile.GetComponent<Tile>();
                        obj.HighLightMe(Color.red);
                    }
                }
                foreach (GameObject unit in unitsList)
                {
                    if (Mathf.Abs(selectedUnit.transform.position.x - unit.transform.position.x) + Mathf.Abs(selectedUnit.transform.position.y - unit.transform.position.y) <= selectedUnit.unitAttackRange + 0.5f)
                    {
                        Unit obj = unit.GetComponent<Unit>();
                        obj.HighLightMe(Color.black);
                    }
                }
            }
            else if (selectedUnit.canAtteck)
            {
                foreach (GameObject unit in unitsList)
                {
                    if (Mathf.Abs(selectedUnit.transform.position.x - unit.transform.position.x) + Mathf.Abs(selectedUnit.transform.position.y - unit.transform.position.y) <= selectedUnit.unitAttackRange + 0.5f)
                    {
                        Unit obj = unit.GetComponent<Unit>();
                        obj.HighLightMe(Color.black);
                    }
                }
            }
        }
        else
        {
            foreach (GameObject tile in tilesList)
                if (tile.GetComponent<Tile>().isWalkAble)
                {
                    tile.GetComponent<Tile>().UnHighLightMe();
                }
            foreach (GameObject unit in unitsList)
            {
                unit.GetComponent<Unit>().UnHighLightMe();
            }
        }
    }
    public void AfterAttack()
    {
        selectedUnit.canAtteck = false;
        foreach (GameObject tile in tilesList)
            if (tile.GetComponent<Tile>().isWalkAble)
            {
                tile.GetComponent<Tile>().UnHighLightMe();
            }
        foreach (GameObject unit in unitsList)
        { 
            unit.GetComponent<Unit>().UnHighLightMe();  
        }
        
        UnitSelection(null);
    }
    public void CounterAttack()
    {
        selectedUnit.TakeCounterDamage(attackedUnit.unitAttack);
        attackedUnit.canCounter = false;
        attackedUnit = null;
    }
    public void UnitSelection(Unit unit)
    {
        selectedUnit = unit;
    }
    public void MoveUnit(Vector2 value)
    {
        selectedUnit.StartMovement(value, moveStep);
    }
    public void GrabHaldeToAttacked(Unit attacked)
    {
        attackedUnit = attacked;
    }
}
