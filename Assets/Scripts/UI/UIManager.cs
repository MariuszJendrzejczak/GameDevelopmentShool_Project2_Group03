using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }
    [SerializeField]
    private TextMeshProUGUI leftPlayerTurn, rightPlayerTurn;


    public void LeftPlayerTurn()
    {
        leftPlayerTurn.color = Color.green;
    }
    public void RightPlayerTurn()
    {
        rightPlayerTurn.color = Color.green;
    }



}
