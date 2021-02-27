using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    
    private Unit selectedUnit;
    private int selectedSpeed;
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
    }

    private void UnitSelectionMethod()
    {
        if (selectedUnit)
        {
            if (selectedUnit.hasMoved == false)
            {
                foreach (GameObject tile in tilesList)
                {
                    if (Mathf.Abs(selectedUnit.transform.position.x - tile.transform.position.x) + Mathf.Abs(selectedUnit.transform.position.y - tile.transform.position.y) <= selectedSpeed + 0.5f)
                    {
                        Tile obj = tile.GetComponent<Tile>();
                        obj.HighLightMe(Color.red);
                    }
                }
                foreach (GameObject unit in unitsList)
                {
                    if (Mathf.Abs(selectedUnit.transform.position.x - unit.transform.position.x) + Mathf.Abs(selectedUnit.transform.position.y - unit.transform.position.y) <= selectedSpeed + 1.5f)
                    {
                        Unit obj = unit.GetComponent<Unit>();
                        obj.HighLightMe(Color.green);
                    }
                }
            }
            else
            {
                foreach (GameObject unit in unitsList)
                {
                    if (Mathf.Abs(selectedUnit.transform.position.x - unit.transform.position.x) + Mathf.Abs(selectedUnit.transform.position.y - unit.transform.position.y) <= 1.5f)
                    {
                        Unit obj = unit.GetComponent<Unit>();
                        obj.HighLightMe(Color.green);
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
         
            Debug.Log("Unselected");
        }
    }
    public void UnitSelection(Unit unit, int speed)
    {
        selectedUnit = unit;
        selectedSpeed = speed;
    }
    public void MoveUnit(Vector2 value)
    {
        selectedUnit.StartMovement(value, moveStep);
    }
}
