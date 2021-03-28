using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI humanTag, humanHealth, humanArmor, humanAttack, humanAttackRange, humanSpeed, elfeTag, elfeHealth, elfeArmor, elfeAttack, elfeAttackRange, elfeSpeed, elfesScoreText, humansScoreText;

    public static int elfesScore = 0;
    public static int humansScore = 0;

    private void Start()
    {
        UIEventBroker.SelectedUnit += ShowSelectedUnitStat;
        UIEventBroker.AtackedUnit += ShowAttackedUnitStat;
        UIEventBroker.WasAttacked += AttackedUnitScore;
    }
    private void Update()
    {
        elfesScoreText.text = "Elfes: " + elfesScore;
        humansScoreText.text = "Humans: " + humansScore;
    }

    public void ShowSelectedUnitStat(GameObject selected)
    {
        if (GameManager.Instance.selectedUnit)
        {
            if (GameManager.Instance.selectedUnit.owner == Unit.Owner.Elfes)
            {
                 elfeHealth.text = "Health: " + GameManager.Instance.selectedUnit.unitHealth;
                 elfeTag.text = "Name: " + GameManager.Instance.selectedUnit.name;
                 elfeArmor.text = "Armor: " + GameManager.Instance.selectedUnit.unitArmor;
                 elfeAttack.text = "Attack: " + GameManager.Instance.selectedUnit.unitAttack;
                 elfeAttackRange.text = "Attack Range: " + GameManager.Instance.selectedUnit.unitAttackRange;
                 elfeSpeed.text = "Speed: " + GameManager.Instance.selectedUnit.unitSpeed;
            }
            else
            {
                humanHealth.text = "Health: " + GameManager.Instance.selectedUnit.unitHealth;
                humanTag.text = "Name: " + GameManager.Instance.selectedUnit.name;
                humanArmor.text = "Armor: " + GameManager.Instance.selectedUnit.unitArmor;
                humanAttack.text = "Attack: " + GameManager.Instance.selectedUnit.unitAttack;
                humanAttackRange.text = "Attack Range: " + GameManager.Instance.selectedUnit.unitAttackRange;
                humanSpeed.text = "Speed: " + GameManager.Instance.selectedUnit.unitSpeed;
            }
        }
        else
        {
            Debug.Log("Error");
        }
    }
    private void ShowAttackedUnitStat(GameObject attacked)
    {
        if (GameManager.Instance.attackedUnit.owner == Unit.Owner.Elfes)
        {
            elfeHealth.text = "Health: " + GameManager.Instance.selectedUnit.unitHealth;
            elfeTag.text = "Name: " + GameManager.Instance.selectedUnit.name;
            elfeArmor.text = "Armor: " + GameManager.Instance.selectedUnit.unitArmor;
            elfeAttack.text = "Attack: " + GameManager.Instance.selectedUnit.unitAttack;
            elfeAttackRange.text = "Attack Range: " + GameManager.Instance.selectedUnit.unitAttackRange;
            elfeSpeed.text = "Speed: " + GameManager.Instance.selectedUnit.unitSpeed;
        }
        else 
        {
            humanHealth.text = "Health: " + GameManager.Instance.selectedUnit.unitHealth;
            humanTag.text = "Name: " + GameManager.Instance.selectedUnit.name;
            humanArmor.text = "Armor: " + GameManager.Instance.selectedUnit.unitArmor;
            humanAttack.text = "Attack: " + GameManager.Instance.selectedUnit.unitAttack;
            humanAttackRange.text = "Attack Range: " + GameManager.Instance.selectedUnit.unitAttackRange;
            humanSpeed.text = "Speed: " + GameManager.Instance.selectedUnit.unitSpeed;
        }
    }
    private void AttackedUnitScore(GameObject score)
    {
        if(GameManager.Instance.attackedUnit.owner == Unit.Owner.Humans)
        {
         elfesScore +=  GameManager.Instance.attackedUnit.unitAttack;

        }
        else if(GameManager.Instance.attackedUnit.owner == Unit.Owner.Elfes)
        {
         humansScore += GameManager.Instance.attackedUnit.unitAttack;
        }
    }
}
