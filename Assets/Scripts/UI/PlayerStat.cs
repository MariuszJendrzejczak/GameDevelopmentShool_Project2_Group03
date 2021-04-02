using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI humanTag, humanHealth, humanArmor, humanAttack, humanAttackRange, humanSpeed, elfeTag, elfeHealth, elfeArmor, elfeAttack, elfeAttackRange, elfeSpeed;

    private void Start()
    {
        UIEventBroker.SelectedUnit += ShowSelectedUnitStat;
        UIEventBroker.AtackedUnit += ShowAttackedUnitStat;
    }

    public void ShowSelectedUnitStat()
    {
        if (GameManager.Instance.selectedUnit)
        {
            if (GameManager.Instance.selectedUnit.owner == Unit.Owner.Elfes)
            {
                if (GameManager.Instance.selectedUnit == true)
                {
                    elfeHealth.text = "Health: " + GameManager.Instance.selectedUnit.unitHealth;
                    elfeTag.text = "Name: " + GameManager.Instance.selectedUnit.name;
                    elfeArmor.text = "Armor: " + GameManager.Instance.selectedUnit.unitArmor;
                    elfeAttack.text = "Attack: " + GameManager.Instance.selectedUnit.unitAttack;
                    elfeAttackRange.text = "Attack Range: " + GameManager.Instance.selectedUnit.unitAttackRange;
                    elfeSpeed.text = "Speed: " + GameManager.Instance.selectedUnit.unitSpeed;
                }
            }
            if (GameManager.Instance.selectedUnit.owner == Unit.Owner.Humans)
            {
                if (GameManager.Instance.selectedUnit == true)
                {
                    humanHealth.text = "Health: " + GameManager.Instance.selectedUnit.unitHealth;
                    humanTag.text = "Name: " + GameManager.Instance.selectedUnit.name;
                    humanArmor.text = "Armor: " + GameManager.Instance.selectedUnit.unitArmor;
                    humanAttack.text = "Attack: " + GameManager.Instance.selectedUnit.unitAttack;
                    humanAttackRange.text = "Attack Range: " + GameManager.Instance.selectedUnit.unitAttackRange;
                    humanSpeed.text = "Speed: " + GameManager.Instance.selectedUnit.unitSpeed;
                }
            }
        }
        else
        {
            elfeHealth.text = "Health: ";
            elfeTag.text = "Name: ";
            elfeArmor.text = "Armor: ";
            elfeAttack.text = "Attack: ";
            elfeAttackRange.text = "Attack Range: ";
            elfeSpeed.text = "Speed: ";
            humanHealth.text = "Health: ";
            humanTag.text = "Name: ";
            humanArmor.text = "Armor: ";
            humanAttack.text = "Attack: ";
            humanAttackRange.text = "Attack Range: ";
            humanSpeed.text = "Speed: ";
        }
    }

    private void ShowAttackedUnitStat()
    {
        if (GameManager.Instance.attackedUnit)
        {
            if (GameManager.Instance.attackedUnit.owner == Unit.Owner.Elfes)
            {
                if (GameManager.Instance.attackedUnit == true)
                {
                    elfeHealth.text = "Health: " + GameManager.Instance.selectedUnit.unitHealth;
                    elfeTag.text = "Name: " + GameManager.Instance.selectedUnit.name;
                    elfeArmor.text = "Armor: " + GameManager.Instance.selectedUnit.unitArmor;
                    elfeAttack.text = "Attack: " + GameManager.Instance.selectedUnit.unitAttack;
                    elfeAttackRange.text = "Attack Range: " + GameManager.Instance.selectedUnit.unitAttackRange;
                    elfeSpeed.text = "Speed: " + GameManager.Instance.selectedUnit.unitSpeed;
                }
            }
            if (GameManager.Instance.attackedUnit.owner == Unit.Owner.Humans)
            {
                if (GameManager.Instance.attackedUnit == true)
                {
                    humanHealth.text = "Health: " + GameManager.Instance.selectedUnit.unitHealth;
                    humanTag.text = "Name: " + GameManager.Instance.selectedUnit.name;
                    humanArmor.text = "Armor: " + GameManager.Instance.selectedUnit.unitArmor;
                    humanAttack.text = "Attack: " + GameManager.Instance.selectedUnit.unitAttack;
                    humanAttackRange.text = "Attack Range: " + GameManager.Instance.selectedUnit.unitAttackRange;
                    humanSpeed.text = "Speed: " + GameManager.Instance.selectedUnit.unitSpeed;
                }
            }
        }
        /*else
        {
            elfeHealth.text = "Health: ";
            elfeTag.text = "Name: ";
            elfeArmor.text = "Armor: ";
            elfeAttack.text = "Attack: ";
            elfeAttackRange.text = "Attack Range: ";
            elfeSpeed.text = "Speed: ";
            humanHealth.text = "Health: ";
            humanTag.text = "Name: ";
            humanArmor.text = "Armor: ";
            humanAttack.text = "Attack: ";
            humanAttackRange.text = "Attack Range: ";
            humanSpeed.text = "Speed: ";
        }*/
    }
}
