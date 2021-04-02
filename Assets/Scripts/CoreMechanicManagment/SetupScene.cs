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
    }


}
