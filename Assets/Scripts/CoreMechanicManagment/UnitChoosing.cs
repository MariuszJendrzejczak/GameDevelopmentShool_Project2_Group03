using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitChoosing : MonoBehaviour
{
    private enum ChoosingState { greetings, humanForHuman, elfForHuman, elfForElf, humanForElf}
    private ChoosingState state;
    [SerializeField]
    private List<GameObject> humanPlayerUnitList, elfesPlayerUnitList;
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

    public void EnterNewState()
    {
        switch (state)
        {
            case ChoosingState.greetings:
                for (int i = 0; i < humanPlayerUnitList.Count; i++)
                {
                    Instantiate(humanPlayerUnitList[i], humanPlayerTransformList[i].position, Quaternion.identity);
                }
                for (int i = 0; i < elfesPlayerUnitList.Count; i++)
                {
                    Instantiate(elfesPlayerUnitList[i], elfesPlayerTransforList[i].position, Quaternion.identity);
                }
                break;
            case ChoosingState.humanForHuman:
                break;
            case ChoosingState.elfForHuman:
                break;
            case ChoosingState.elfForElf:
                break;
            case ChoosingState.humanForElf:
                break;
        }
    }
    public void UnitChosed(GameObject unit)
    {
        if(unit.GetComponent<Unit>().owner == Unit.Owner.Humans)
        {
            choosedHumanPlayerUnitList.Add(unit);
            UIEventBroker.CallHumanChosedUnitPanel();
            UIEventBroker.CallShowHumanUnitsChoosed(unit);
            Debug.Log(unit.name + "został dodany do armii Ludzi (UnitChoosingScript)"); // może zostac zamienione na wiadomość dla UIManagera
            unit.SetActive(false);

            for (int i = 2; i < choosedHumanPlayerUnitList.Count; i++ )
            {
                foreach (GameObject obj in choosedHumanPlayerUnitList)
                {
                    if (obj.activeSelf == false)
                    {
                        obj.SetActive(true);
                    }
                }
            }
            unit.SetActive(false);
        }
        if (unit.GetComponent<Unit>().owner == Unit.Owner.Elfes)
        {
            choosedElfesPlayerUnitList.Add(unit);
            UIEventBroker.CallElfChosedUnitPanel();
            UIEventBroker.CallShowElfesUnitsChoosed(unit);
            Debug.Log(unit.name + "został dodany do armii Elfów (UnitChoosingScript)"); // może zostac zamienione na wiadomość dla UIManagera
            unit.SetActive(false);
            for (int i = 2; i < choosedElfesPlayerUnitList.Count; i++)
            {
                foreach (GameObject obj in choosedElfesPlayerUnitList)
                {
                    if (obj.activeSelf == false)
                    {
                        obj.SetActive(true); 
                    }
                }
            }
            unit.SetActive(false);
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
