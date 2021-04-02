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

    [Header("Player Move")]
    private Color basicColor = Color.white;
    [SerializeField]
    private Color turnColor = new Color(0.7735f, 0.2138951f, 0.2079922f, 1f);
    [SerializeField]
    private Image turnHuman, turnElfes;

    [SerializeField]
    TextMeshProUGUI humanScoreText, elfesScoreText;

    [SerializeField]
    GameObject humanSanktuariumBtn, elfesSwapBtn, humanSwapBtn, elfSanktuariumBtn;

    Unit unit;
    SkillSanctuarium skillSanctuarium;
    SkillSwapUnits skillSwap;


    void Start()
    {
        UIEventBroker.HumanMove += HumansMove;
        UIEventBroker.ElfesMove += ElfesMove;
        UIEventBroker.UIElfesScore += ElfesScore;
        UIEventBroker.UIHumanScore += HumanScore;
        CMEventBroker.UpdateElfesScore += ElfesScore;
        CMEventBroker.UpdateHumanScore += HumanScore;
    }

  

    public void HumansMove()
    {
        turnHuman.GetComponent<Image>().color = turnColor;
        turnElfes.GetComponent<Image>().color = basicColor;
    }

    public void ElfesMove()
    {
        turnElfes.GetComponent<Image>().color = turnColor;
        turnHuman.GetComponent<Image>().color = basicColor;
    }

    private void ElfesScore(int elfesScore)
    {
        elfesScoreText.text  += elfesScore;
    }
    private void HumanScore(int humanScore)
    {
        humanScoreText.text += humanScore.ToString();
    }

    public void HumanSkills()
    {
        if (GameManager.Instance.selectedUnit.owner == Unit.Owner.Humans)
        {
            if (GameManager.Instance.selectedUnit.GetComponent<SkillSwapUnits>())
            {
                humanSwapBtn.SetActive(true);
            }
            else
            {
                humanSwapBtn.SetActive(false);
            }

            if (GameManager.Instance.selectedUnit.GetComponent<SkillSanctuarium>())
            {
                humanSanktuariumBtn.SetActive(true);
            }
            else
            {
                humanSanktuariumBtn.SetActive(false);
            }
        }
    }
    public void ElfesSkills()
    {
        if (GameManager.Instance.selectedUnit.owner == Unit.Owner.Elfes)
        {
            if (GameManager.Instance.selectedUnit.GetComponent<SkillSwapUnits>())
            {
                elfesSwapBtn.SetActive(true);
            }
            else
            {
                elfesSwapBtn.SetActive(false);
            }

            if (GameManager.Instance.selectedUnit.GetComponent<SkillSanctuarium>())
            {
                elfSanktuariumBtn.SetActive(true);
            }
            else
            {
                elfSanktuariumBtn.SetActive(false);
            }
        }
    }
}
