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
    private GameObject tilesContaainter;
    [SerializeField]
    private List<GameObject> tilesList;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        selectedUnit = null;
        for (int i = 0; i < tilesContaainter.transform.childCount; i++)
        {
            tilesList.Add(tilesContaainter.transform.GetChild(i).gameObject);
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
            Debug.Log("Selected");
            foreach (GameObject tile in tilesList)
            {
                if (Mathf.Abs(selectedUnit.transform.position.x - tile.transform.position.x) + Mathf.Abs(selectedUnit.transform.position.y - tile.transform.position.y) <= selectedSpeed + 0.5f)
                {
                    Tile obj = tile.GetComponent<Tile>();
                    obj.HighLightMe(Color.red);
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
