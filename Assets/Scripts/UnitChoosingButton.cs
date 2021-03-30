using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitChoosingButton : MonoBehaviour
{
    private UnitChoosing unitChoosing;
    private void Start()
    {
        unitChoosing = GetComponent<UnitChoosing>();
    }
    public void EndUnitChoosingScene()
    {
        unitChoosing.SendFinalUnitListsToGameManager();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.Instance.gameState = GameManager.GameState.DeploymentLeft;
        CMEventBroker.CallChangeGameState();
    }
}
