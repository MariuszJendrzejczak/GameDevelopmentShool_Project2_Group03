using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSwapUnits : Skill
{
    [SerializeField]
    private GameObject unitToSwap1, unitToSwap2;
    private enum PreviewState { leftPlayer, rightPlayer}
    private PreviewState previewState;
    // Start is called before the first frame update
    void Start()
    {
        CMEventBroker.SwapUnitsPeak += AddUnitToSwap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapUnitsState()
    {
        if(GameManager.Instance.gameState == GameManager.GameState.LeftPlayerTurn)
        {
            previewState = PreviewState.leftPlayer;
        }
        else if (GameManager.Instance.gameState == GameManager.GameState.RightPlayerTurn)
        {
            previewState = PreviewState.rightPlayer;
        }
        GameManager.Instance.gameState = GameManager.GameState.SkillSwapUnits;
    }

    public void AddUnitToSwap(GameObject unit)
    {
        if (unitToSwap1 == null)
        {
            unitToSwap1 = unit;
            unitToSwap1.transform.localScale += Vector3.one * 1.3f;
        }
        else if (unitToSwap2 == null)
        {
            unitToSwap2 = unit;
            unitToSwap2.transform.localScale += Vector3.one * 1.3f;
        }
        else
        {
            //wiadomość, że 2 unity juz zostały wybrane;
            Debug.Log("Dwa unity już są wybrane");
        }
    }
    public void ResetSwapUnits()
    {
        unitToSwap1.transform.localScale -= Vector3.one * 1.3f;
        unitToSwap1 = null;
        unitToSwap2.transform.localScale -= Vector3.one * 1.3f;
        unitToSwap2 = null;
    } 

    public void SwapUnit()
    {
        Vector3 trans1 = unitToSwap1.transform.position;
        Vector3 trans2 = unitToSwap2.transform.position;
        unitToSwap1.transform.position = trans2;
        unitToSwap2.transform.position = trans1;
        unitToSwap1.transform.localScale -= Vector3.one * 1.3f;
        unitToSwap2.transform.localScale -= Vector3.one * 1.3f;
        isActive = false;
        if(previewState == PreviewState.leftPlayer)
        {
            GameManager.Instance.gameState = GameManager.GameState.LeftPlayerTurn;
        }
        else if (previewState == PreviewState.rightPlayer)
        {
            GameManager.Instance.gameState = GameManager.GameState.RightPlayerTurn;
        }
    }
}
