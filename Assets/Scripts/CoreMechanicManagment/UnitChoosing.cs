using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class UnitChoosing : MonoBehaviour
{
    private enum ChoosingState { greetings, humanForHuman, elfForHuman, elfForElf, humanForElf}
    [SerializeField]
    private ChoosingState state;
    [SerializeField]
    private int counter = 0;
    [SerializeField]
    private List<GameObject> humanPlayerUnitList, elfesPlayerUnitList, tempList;
    [SerializeField]
    private List<Transform> humanPlayerTransformList, elfesPlayerTransforList;
    [SerializeField]
    private List<GameObject> choosedHumanPlayerUnitList, choosedElfesPlayerUnitList;
    [SerializeField]
    private GameObject unitsContainer;

    private void Awake()
    {

        if (unitsContainer == null)
        {
            unitsContainer = GameObject.Find("UnitsContainer");
        }
    }
    private void Start()
    {
        CMEventBroker.UnitChoosed += UnitChosed;
        CMEventBroker.UnitChoosed += AddUnitToContainer;
        EnterNewState();
    }

    public void NextState()
    {
        switch (state)
        {
            case ChoosingState.greetings:
                state = ChoosingState.humanForHuman;
                EnterNewState();
                break;
            case ChoosingState.humanForHuman:
                state = ChoosingState.elfForHuman;
                EnterNewState();
                break;
            case ChoosingState.elfForHuman:
                state = ChoosingState.elfForElf;
                EnterNewState();
                break;
            case ChoosingState.elfForElf:
                state = ChoosingState.humanForElf;
                EnterNewState();
                break;
            case ChoosingState.humanForElf:
                tempList.Clear();
                break;
        }
        counter = 0;
    }

    public void EnterNewState()
    {
        switch (state)
        {
            case ChoosingState.greetings:
                for (int i = 0; i < humanPlayerUnitList.Count; i++)
                {
                    var obj = Instantiate(humanPlayerUnitList[i], humanPlayerTransformList[i].position, Quaternion.identity);
                    tempList.Add(obj);
                }
                break;
            case ChoosingState.humanForHuman:
                //nothing
                break;
            case ChoosingState.elfForHuman:
                SetActiveFaleseForTempList();              
                for (int i = 0; i < humanPlayerUnitList.Count; i++)
                {
                    var obj = Instantiate(humanPlayerUnitList[i], humanPlayerTransformList[i].position, Quaternion.identity);
                    tempList.Add(obj);
                }
                break;
            case ChoosingState.elfForElf:
            case ChoosingState.humanForElf:
                SetActiveFaleseForTempList();
                for (int i = 0; i < elfesPlayerUnitList.Count; i++)
                {
                    var obj = Instantiate(elfesPlayerUnitList[i], elfesPlayerTransforList[i].position, Quaternion.identity);
                    tempList.Add(obj);
                }
                break;
        }
    }
     private void SetActiveFaleseForTempList()
    {
        foreach (GameObject obj in tempList)
        {
            obj.SetActive(false);
        }
    }

    public void UnitChosed(GameObject unit)
    {
        if (counter < 3)
        {
            if (unit.GetComponent<Unit>().owner == Unit.Owner.Humans)
            {
                choosedHumanPlayerUnitList.Add(unit);
                UIEventBroker.CallHumanChosedUnitPanel();
                UIEventBroker.CallShowHumanUnitsChoosed(unit);
                Debug.Log(unit.name + "został dodany do armii Ludzi (UnitChoosingScript)"); // może zostac zamienione na wiadomość dla UIManagera
                unit.SetActive(false);
                counter++;
                unit.SetActive(false);
            }
            if (unit.GetComponent<Unit>().owner == Unit.Owner.Elfes)
            {
                choosedElfesPlayerUnitList.Add(unit);
                UIEventBroker.CallElfChosedUnitPanel();
                UIEventBroker.CallShowElfesUnitsChoosed(unit);
                Debug.Log(unit.name + "został dodany do armii Elfów (UnitChoosingScript)"); // może zostac zamienione na wiadomość dla UIManagera
                unit.SetActive(false);
                counter++;
                unit.SetActive(false);
            }
        }
        
    }

    public void AddUnitToContainer(GameObject unit)
    {
        unit.transform.SetParent(unitsContainer.transform);
    }

    public void SendFinalUnitListsToGameManager()
    {
        GameManager.Instance.unitsContainer = unitsContainer;
        CMEventBroker.CallAllUnitsChoosed(choosedHumanPlayerUnitList, choosedElfesPlayerUnitList);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SendFinalUnitListsToGameManager();
            Debug.Log("Wybór jednostek zakończony!");
            this.gameObject.SetActive(false);
        }
    }
}
