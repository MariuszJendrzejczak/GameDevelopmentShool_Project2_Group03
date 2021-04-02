using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitChoosing : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> humanPlayerUnitList, elfesPlayerUnitList;
    [SerializeField]
    private List<Transform> humanPlayerTransformList, elfesPlayerTransforList;
    [SerializeField]
    private List<GameObject> choosedHumanPlayerUnitList, choosedElfesPlayerUnitList;

    private void Start()
    {
        CMEventBroker.UnitChoosed += UnitChosed;
        // poniższy kod wykonuje instancje jednostek do wyboru w zadanych pozycjach transform. Z założenia wykonywane na poczatku sceny ChoosingUnits.
        for (int i = 0; i < humanPlayerUnitList.Count; i++)
        {
            Instantiate(humanPlayerUnitList[i], humanPlayerTransformList[i].position, Quaternion.identity);
            Debug.Log(i);
        }
        for (int i = 0; i < elfesPlayerUnitList.Count; i++)
        {
            Instantiate(elfesPlayerUnitList[i], elfesPlayerTransforList[i].position, Quaternion.identity);
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
        }
        if (unit.GetComponent<Unit>().owner == Unit.Owner.Elfes)
        {
            choosedElfesPlayerUnitList.Add(unit);
            UIEventBroker.CallElfChosedUnitPanel();
            UIEventBroker.CallShowElfesUnitsChoosed(unit);
            Debug.Log(unit.name + "został dodany do armii Elfów (UnitChoosingScript)"); // może zostac zamienione na wiadomość dla UIManagera
            unit.SetActive(false);
        }
        //tutaj może zostac scallowany event, który bedzie przekazywał listy wybranych jednostek do UIManagera, który będzie mógł tę liste w jakiś sposób wyświetlić.
    }

    public void SendFinalUnitListsToGameManager()
    {
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
