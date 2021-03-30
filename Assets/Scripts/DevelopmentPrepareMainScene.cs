using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentPrepareMainScene : MonoBehaviour
{ 
    public void StartMainScene()
    {
        Debug.Log("Click");
        GameManager.Instance.gameState = GameManager.GameState.DeploymentLeft;
        GameManager.Instance.EnterNewState();
        GameManager.Instance.gameState = GameManager.GameState.LeftPlayerTurn;
    }
}
