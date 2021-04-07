using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackedUnitStat : MonoBehaviour
{
    [SerializeField] TextMeshPro healthText, attackText, attackRangeText, tagText, speedText, armorText, attackSimText;

    Unit unit;

    private void Start()
    {
        unit = GetComponentInParent<Unit>();
    }

    private void Update()
    {
        CurrentAttackedUnitStat();
    }

    private void CurrentAttackedUnitStat()
    {
            healthText.text = "Health: " + unit.unitHealth;
            attackText.text = "Attack: " + unit.unitAttack;
            attackRangeText.text = "Attack Range: " + unit.unitAttackRange;
            tagText.text = "Tag: " + unit.myTag;
            speedText.text = "Speed: " + unit.unitSpeed;
            armorText.text = "Armor: " + unit.unitArmor;
            if(GameManager.Instance.selectedUnit != null)
            {
                attackSimText.text = "Give Damage: -" + GameManager.Instance.selectedUnit.unitAttack;
            }
            
    }
}
