using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.PackageManager.Requests;
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
    
    public enum GameState { MainManu, UnitChoosing, DeploymentLeft, DeploymentRight, LeftPlayerBeforeTurn, LeftPlayerTurn, RightPlayerBeforeTurn, RightPlayerTurn, EndGame}
    public GameState gameState;
    public enum GameMode { DeveloperMode, NormalMode}
    public GameMode gameMode;
    public Unit selectedUnit, attackedUnit;
    [SerializeField]
    private float moveStep = 0.04f;
    [SerializeField]
    private GameObject tilesContaainter, unitsContainer;
    [SerializeField]
    private List<GameObject> tilesList, unitsList; // unitList najprawdopodobmnie do ununięcia w momencie zaimplementowanie dwóch osobnych list dla graczy.
    private List<GameObject> leftPlayerUnitList, rightPlayerUnitList;
    

    private void Awake()
    {
        instance = this;
        gameState = GameState.MainManu;

    }
    private void Start()
    {
        CMEventBroker.ChangeGameState += EnterNewState;
        CMEventBroker.ChangeGameMode += ChangeGameMode;

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
        switch (gameState)
        {
            case GameState.MainManu:
                // 1. Wybór opcji z MainManu, ustawianie parametrów tj. imiona graczy, itp.
                break;
            case GameState.UnitChoosing:
                // 1. OnMauseClick na wyświetlonej jednostce, umieszcza ją w liście jednostek danego gracza. Zaimplementować w skrypcie Unit! (leftPlayerUnitList, rightPlayerUnitList)
                break;
            case GameState.DeploymentLeft:
                // 1. Bazując na listach graczy wykładamy jednoski wg zadesignowanego schematu na pierwszych dwóch rzędach pól. Do zaimplementowanie state lub bool dla tych pól, żeby w tym stacie mogły się wyświetlać. 
                // 2. Być może będzie trzeba podzielić ten state na 2 osobne, dla każdego gracza jeden.
                break;
            case GameState.DeploymentRight:
                break;
            case GameState.LeftPlayerBeforeTurn:
                break;
            case GameState.LeftPlayerTurn:
                // 1. Gracz lewy może poryuszac i atakowac Swoimi jednostkami
                break;
            case GameState.RightPlayerBeforeTurn:
                break;
            case GameState.RightPlayerTurn:
                // 1 Analogicznie dla prawego gracza
                break;
            case GameState.EndGame:
                //1. Gdy zmienna victorycondition zostanie osiągnięta (bool), blokujemy grę, wyświetlamy ekran końcowy lub podsumowanie itp. Możliwość wejścia do main manu. 
                break;
        }

                UnitSelectionMethod();
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (GameObject obj in unitsList)
            {
                obj.GetComponent<Unit>().NewTurnForUnit();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gameMode == GameMode.DeveloperMode)
            {
                gameMode = GameMode.NormalMode;
            }
            else if (gameMode == GameMode.NormalMode)
            {
                gameMode = GameMode.DeveloperMode;
            }
            CMEventBroker.CallChangeGameMode();
            Debug.Log(gameMode);
        }
    }

    public void ChangeGameMode()
    {
        //tutaj się coś będzie działo, jak się będzie zmieniał tryb gry
        switch (gameMode)
        {
            case GameMode.DeveloperMode:
                break;
            case GameMode.NormalMode:
                break;
        }
    }

    public void EnterNewState()
    {
        switch (gameState)
        {
            case GameState.MainManu:
                // 1. Reset/SetUp/zerowanie ustawień na początek gry. 
                break;
            case GameState.UnitChoosing:
                // 1. wyświetlenie sceny z wyborem jednostek.
                // 2. wyświetlenie dostępnych jednostek (perp scena)
                break;
            case GameState.DeploymentLeft:
                // 1. Wyświetlenie panelu z jednostkami do umieszczenia na planszy dla poszczególnych graczy.
                break;
            case GameState.DeploymentRight:
                break;
            case GameState.LeftPlayerBeforeTurn:
                // 1. Resetowanie boolenów jednostek lewego gracza (tj. canAttack, canMove, canCounter), colddawny jeśli będą. Przygotowanie jednostek lewego gracza przed turą.
                break;
            case GameState.LeftPlayerTurn:
                break;
            case GameState.RightPlayerBeforeTurn:
                // 1. Analogicznie dla prawego gracza
                break;
            case GameState.RightPlayerTurn:
                break;
            case GameState.EndGame:
                // 1. Przygotowanie podsumowania gry (matematyka)
                break;
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
