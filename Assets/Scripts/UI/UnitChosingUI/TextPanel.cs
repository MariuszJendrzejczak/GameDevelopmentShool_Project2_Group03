using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textComponent;
    [SerializeField] State startingState;
    [SerializeField] GameObject instructionPanel;
   
    State state;

    void Start()
    {
        state = startingState;
        textComponent.text = state.GetStateStory();
    }
    public void ManageState()
    {
        var nextStates = state.GetNextStates();
        if (nextStates != null)
        {
            state = nextStates[0];  
        }   
        if (state != null)
        {
            textComponent.text = state.GetStateStory();
        }
        else
        {
            instructionPanel.SetActive(false);
        }

    }
}
