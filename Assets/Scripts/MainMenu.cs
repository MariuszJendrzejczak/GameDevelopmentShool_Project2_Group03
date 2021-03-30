using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("ładowanie sceny");
        GameManager.Instance.gameState = GameManager.GameState.UnitChoosing; // zmiena stanu gry
        Debug.Log("zminan stanu");
        CMEventBroker.CallChangeGameState(); // wywołanie eventu po zmianie stanu gry
        Debug.Log("event");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
