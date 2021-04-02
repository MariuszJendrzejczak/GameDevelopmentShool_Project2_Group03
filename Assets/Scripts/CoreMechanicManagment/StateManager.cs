using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public void TurnEnd()
    {
        GameManager.Instance.EndofTurn();
    }
    public void DeploymentEnd()
    {
        GameManager.Instance.EndofDeployment();
    }
}
