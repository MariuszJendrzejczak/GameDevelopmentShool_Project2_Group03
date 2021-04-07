using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupScene : MonoBehaviour
{
    private Pathfinding pathfinding;
    [SerializeField]
    private GameObject tilesContainer, unitContainer;

    private void Awake()
    {
        if (tilesContainer == null)
        {
            tilesContainer = GameObject.Find("TilesContainer");
        }
    }

    void Start()
    {
        GameManager.Instance.AssignTilesContainer(tilesContainer);
        pathfinding = GameManager.Instance.GetComponent<Pathfinding>();
        GameManager.Instance.SetupScene();
        pathfinding.GetNodeList();

        for (int i = 0; i < 6; i++)
        {
            GameObject unit = GameManager.Instance.humanPlayerUnitList[i];
            unit.transform.position = new Vector2(0, i + 5);
            unit.SetActive(true);
        }
        for (int i = 0; i < 6; i++)
        {
            GameObject unit = GameManager.Instance.elfesPlayerUnitList[i];
            unit.transform.position = new Vector2(14, i + 5);
            unit.SetActive(true);
        }
        GameManager.Instance.gameState = GameManager.GameState.PreDeployment;
        CMEventBroker.CallChangeGameState();
    }


}
