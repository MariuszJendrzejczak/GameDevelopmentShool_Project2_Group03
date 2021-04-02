using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentPrepareMainScene : MonoBehaviour
{
    private Pathfinding pathfinding; 
    private void Start()
    {
        pathfinding = GameObject.Find("GameManager").GetComponent<Pathfinding>();
    }
    public void StartMainScene()
    {
        GameManager.Instance.gameState = GameManager.GameState.DeploymentLeft;
        GameManager.Instance.EnterNewState();
        GameManager.Instance.gameState = GameManager.GameState.LeftPlayerTurn;
        pathfinding.GetNodeList();
    }
}
