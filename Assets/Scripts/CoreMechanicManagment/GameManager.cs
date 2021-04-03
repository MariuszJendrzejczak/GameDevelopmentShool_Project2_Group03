using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    
    public enum GameState { MainManu, UnitChoosing, DeploymentLeft, DeploymentRight, LeftPlayerBeforeTurn, LeftPlayerTurn, RightPlayerBeforeTurn, RightPlayerTurn, EndGame, SkillSwapUnits,}
    public GameState gameState;
    public enum GameMode { DeveloperMode, NormalMode}
    public GameMode gameMode;
    public Unit selectedUnit, attackedUnit;
    private Pathfinding pathfinding;
    private int selectedUnitattack, selectedUnitArmor, selectedUnitSpeed, selectedUnitAttackRange, selectedUnitHealth;
    private int attackedUnitattack, attackedUnitArmor, attackedUnitSpeed, attackedUnitAttackRange, attackedUnitHealth;
    [SerializeField]
    Color startingTileHighlightColor, moveRangeHighlightColor, attackAbleUnitHighlightColor;
    [SerializeField]
    private float moveStep = 0.04f;
    [SerializeField]
    public GameObject tilesContaainter, unitsContainer;
    private PathNode startNode;
    [SerializeField]
    public List<GameObject> tilesList, unitsList; // unitList najprawdopodobmnie do ununięcia w momencie zaimplementowanie dwóch osobnych list dla graczy.
    [SerializeField]
    public List<GameObject> humanPlayerUnitList, elfesPlayerUnitList;
    [SerializeField]
    private int humanScore, elfesScore;
    public int humansAlive = 6, elfesAlive = 6;
    public int roundsToEnd = 7;
    public bool guffinPickedUp = false;
    private void Awake()
    {
        instance = this;
        gameState = GameState.MainManu;
        pathfinding = GetComponent<Pathfinding>();
    }
    private void Start()
    {
        CMEventBroker.ChangeGameState += EnterNewState;
        CMEventBroker.ChangeGameMode += GameModeChanged;
        CMEventBroker.AllUnitsChoosed += SetUpChoosedUnitLists;
        CMEventBroker.UpdateElfesScore += UpdateElfesScore;
        CMEventBroker.UpdateHumanScore += UpdateHumanScore;
  
        /// wypełnianie list będzie wykonywane po UnitSelection state, w trakcie w trakcie Deployment State. 
        selectedUnit = null;
    }

    private void Update()
    {
        StateUpdateCall();
        if (gameState == GameState.LeftPlayerTurn || gameState == GameState.RightPlayerTurn)
        {
            UnitSelectionMethod();
        }
        KaybordIntputOnScen();
        if (Input.GetKeyDown(KeyCode.N))
            CMEventBroker.CallChangeGameState();
        EndCheck();
    }

    private void EndCheck()
    {
        if (humansAlive == 0 || elfesAlive == 0 || roundsToEnd == 0)
        {
            gameState = GameState.EndGame;
            CMEventBroker.CallChangeGameState();
        }
    }
    public void EndofTurn()
    {
        if (gameState == GameState.LeftPlayerTurn)
        {
            gameState = GameState.RightPlayerBeforeTurn;
            CMEventBroker.CallChangeGameState();
            gameState = GameState.RightPlayerTurn;
            CMEventBroker.CallChangeGameState();
        }
        else if (gameState == GameState.RightPlayerTurn)
        {
            gameState = GameState.LeftPlayerBeforeTurn;
            CMEventBroker.CallChangeGameState();
            gameState = GameState.LeftPlayerTurn;
            CMEventBroker.CallChangeGameState();
        }
    }

    public void EndofDeployment()
    {
        if (gameState == GameState.DeploymentLeft)
        {
            gameState = GameState.DeploymentRight;
            CMEventBroker.CallChangeGameState();
        }
        else if (gameState == GameState.DeploymentRight)
        {
            gameState = GameState.LeftPlayerBeforeTurn;
            CMEventBroker.CallChangeGameState();
            gameState = GameState.LeftPlayerTurn;
            CMEventBroker.CallChangeGameState();
        }
    }

    public void EnterNewState()
    {
        switch (gameState)
        {
            case GameState.MainManu:
                break;
            case GameState.UnitChoosing:
                break;
            case GameState.DeploymentLeft:
                selectedUnit = null;
                foreach (GameObject tile in tilesList)
                {
                    Tile tileScript = tile.GetComponent<Tile>();
                    if (tileScript.isStartingTile == Tile.StartingTile.leftPlayerStartingTile)
                    {
                        tileScript.HighLightMe(Color.red);
                    }
                }
                break;
            case GameState.DeploymentRight:
                selectedUnit = null;
                foreach (GameObject tile in tilesList)
                {
                    tile.GetComponent<Tile>().UnHighLightMe();
                }
                foreach (GameObject tile in tilesList)
                {
                    Tile tileScript = tile.GetComponent<Tile>();
                    if (tileScript.isStartingTile == Tile.StartingTile.rightPlayerStartingTile)
                    {
                        tileScript.HighLightMe(Color.red);
                    }
                }
                break;
            case GameState.LeftPlayerBeforeTurn:
                if (guffinPickedUp)
                {
                    roundsToEnd--;
                }
                selectedUnit = null;
                foreach (GameObject tile in tilesList)
                {
                    tile.GetComponent<Tile>().UnHighLightMe();
                }
                foreach (GameObject obj in unitsList)
                {
                    Unit unit = obj.GetComponent<Unit>();
                    if (unit.owner == Unit.Owner.Humans)
                    {
                        unit.NewTurnForUnit();
                    }
                }
                break;
            case GameState.LeftPlayerTurn:
                break;
            case GameState.RightPlayerBeforeTurn:
                selectedUnit = null;
                foreach (GameObject tile in tilesList)
                {
                    tile.GetComponent<Tile>().UnHighLightMe();
                }
                foreach (GameObject obj in unitsList)
                {
                    Unit unit = obj.GetComponent<Unit>();
                    if (unit.owner == Unit.Owner.Elfes)
                    {
                        unit.NewTurnForUnit();
                    }
                }
                break;
            case GameState.RightPlayerTurn:
                break;
            case GameState.EndGame:
                UIEventBroker.CallShowEndGame();
                Debug.Log("End of game");
                break;
        }
    }

    private void StateUpdateCall()
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
                //sekcja przygotowujaca gamemanagerza do głownej sceny gry (łapanie referencji do objektów)
                // 1. Bazując na listach graczy wykładamy jednoski wg zadesignowanego schematu na pierwszych dwóch rzędach pól. Do zaimplementowanie state lub bool dla tych pól, żeby w tym stacie mogły się wyświetlać. 
                // 2. Być może będzie trzeba podzielić ten state na 2 osobne, dla każdego gracza jeden.
                break;
            case GameState.DeploymentRight:
                break;
            case GameState.LeftPlayerBeforeTurn:
                break;
            case GameState.LeftPlayerTurn:
                UIEventBroker.CallHumanMove();
                // 1. Gracz lewy może poryuszac i atakowac Swoimi jednostkami
                break;
            case GameState.RightPlayerBeforeTurn:
                break;
            case GameState.RightPlayerTurn:
                UIEventBroker.CallElfesMove();
                // 1 Analogicznie dla prawego gracza
                break;
            case GameState.EndGame:
                //1. Gdy zmienna victorycondition zostanie osiągnięta (bool), blokujemy grę, wyświetlamy ekran końcowy lub podsumowanie itp. Możliwość wejścia do main manu. 
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
                    if (Mathf.Abs(selectedUnit.transform.position.x - tile.transform.position.x) + Mathf.Abs(selectedUnit.transform.position.y - tile.transform.position.y) <= selectedUnitSpeed + 0.5f)
                    {
                        Tile obj = tile.GetComponent<Tile>();
                        obj.HighLightMe(Color.red);
                    }
                }
                foreach (GameObject unit in unitsList)
                {
                    if (Mathf.Abs(selectedUnit.transform.position.x - unit.transform.position.x) + Mathf.Abs(selectedUnit.transform.position.y - unit.transform.position.y) <= selectedUnitAttackRange + 0.5f)
                    {
                        Unit obj = unit.GetComponent<Unit>();
                        obj.HighLightMe(Color.red);
                    }
                }
            }
            else if (selectedUnit.canAtteck)
            {
                foreach (GameObject unit in unitsList)
                {
                    Unit obj = unit.GetComponent<Unit>();
                    if (Mathf.Abs(selectedUnit.transform.position.x - unit.transform.position.x) + Mathf.Abs(selectedUnit.transform.position.y - unit.transform.position.y) <= selectedUnitAttackRange + 0.5f)
                    {
                        switch(gameState)
                        {
                            case GameState.LeftPlayerTurn:                               
                                if (obj.owner == Unit.Owner.Elfes)
                                obj.HighLightMe(Color.black);
                                break;
                            case GameState.RightPlayerTurn:
                                if (obj.owner == Unit.Owner.Humans)
                                    obj.HighLightMe(Color.black);
                                break;
                        }
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

    private void KaybordIntputOnScen()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // sposób zmieniania trybu do zmiany w późniejszym czasie.
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
        // kod tymczasowy, do usunięcie po zaimplementowaniu pełej maszyny stanów.
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (GameObject obj in unitsList)
            {
                obj.GetComponent<Unit>().NewTurnForUnit();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
            SetupScene();
    }

    public void GameModeChanged()
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
        
        UnitSelection(null, 0,0,0,0,0, null);
    }

    public void CounterAttack()
    {
        selectedUnit.TakeCounterDamage(attackedUnitattack);
        attackedUnit.canCounter = false;
        attackedUnit = null;
    }

    public void UnitSelection(Unit unit, int attack, int armor, int healt, int speed, int attackRange, PathNode node)
    {
        selectedUnit = unit;
        selectedUnitattack = attack;
        selectedUnitArmor = armor;
        selectedUnitHealth = healt;
        selectedUnitSpeed = speed;
        selectedUnitAttackRange = attackRange;
        startNode = node;

        UIEventBroker.CallUnitSelected();
        // miejsce na wywołanie eventu do UI, przekazującego parametry wybranego unitu do wyświetlenia. (np. UnitSelected) 
        // UICallUnitWasSelected(); - tutaj, 
        // deklaracja eventu i calla w UIEventBroker,
        // UIEventBroker.UnitWasSelected += Metoda która przekazuje wartości w UIManagerze. 
    }

    public void MoveUnit(Vector2 value)
    {
        selectedUnit.StartMovement(value, moveStep);
    }

    public void GrabHaldeToAttacked(Unit attacked, int attack, int armor, int healt, int speed, int attackRange)
    {
        attackedUnit = attacked;
        attackedUnitattack = attack;
        attackedUnitArmor = armor;
        attackedUnitHealth = healt;
        attackedUnitSpeed = speed;
        attackedUnitAttackRange = attackRange;
        UIEventBroker.CallAtackedUnit();
        // miejsce do wywałanie eventu do UI, przekazującego prametry zaatakowanego unitu do wyświetlanie (np. UnitAttacked)
    }

    public void SetUpChoosedUnitLists( List<GameObject> humans, List<GameObject> elfes)
    {
        foreach(GameObject unit in humans)
        {
            humanPlayerUnitList.Add(unit);
        }
        foreach(GameObject unit in elfes)
        {
            elfesPlayerUnitList.Add(unit);
        }
    }
    private void UpdateHumanScore(int value)
    {
        humanScore += value;
    }
    private void UpdateElfesScore(int value)
    {
        elfesScore += value;
    }
    public List<GameObject> GrabTilesList()
    {
        return tilesList;
    }
    public void SetupScene()
    {
        for (int i = 0; i < tilesContaainter.transform.childCount; i++)
        {
            tilesList.Add(tilesContaainter.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i <unitsContainer.transform.childCount; i++)
        {
            unitsList.Add(unitsContainer.transform.GetChild(i).gameObject);
        }
    }
    public void ClearNodeList()
    {
        pathfinding.ClearPathList();
    }
    
    public void AssignTilesContainer (GameObject container)
    {
        tilesContaainter = container;
    }
}