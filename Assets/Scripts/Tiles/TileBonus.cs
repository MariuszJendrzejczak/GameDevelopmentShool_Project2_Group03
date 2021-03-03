using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBonus : MonoBehaviour
{
    [SerializeField]
    private int attackBonus, attackRangeBonus, speedBonus, armorBonus;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
        {
            collision.GetComponent<Unit>().GetBonus(attackBonus, armorBonus, speedBonus, attackRangeBonus); ;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
        {
            collision.GetComponent<Unit>().SetBaseStatisticValues();
        }
    }
}
