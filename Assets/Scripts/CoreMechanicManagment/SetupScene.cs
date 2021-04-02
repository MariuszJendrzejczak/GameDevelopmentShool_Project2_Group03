using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupScene : MonoBehaviour
{
    private Pathfinding pathfinding;
    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
        GameManager.Instance.SetupScene();
        pathfinding.GetNodeList();
        /*for (int i = 0; i < 6; i++)
        {
            GameManager.Instance.humanPlayerUnitList[i].transform.position = new Vector2(0, i + 5);
        }
        for (int i = 0; i < 6; i++)
        {
            GameManager.Instance.elfesPlayerUnitList[i].transform.position = new Vector2(14, i + 5);
        }*/
    }


}
