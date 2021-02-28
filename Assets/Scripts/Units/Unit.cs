using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{


    private SpriteRenderer renderer;
    private bool isSelected = false;
    [SerializeField]
    public int unitSpeed, unitHealth, unitAttack, unitArmor, unitAttackRange;
    public bool canMove, canAtteck, canCounter, isAtteckable = false;
    private int startingHP;

    private void Start()
    {
        canMove = true;
        canAtteck = true;
        canCounter = true;
        renderer = GetComponent<SpriteRenderer>();
        startingHP = unitHealth;
    }
    private void OnMouseDown()
    {
        if (GameManager.Instance.selectedUnit == this)
        {
            GameManager.Instance.UnitSelection(null);
            isSelected = false;
        }
        else if (GameManager.Instance.selectedUnit == null)
        {
            GameManager.Instance.UnitSelection(this);
            isSelected = true;
        }
        else if (GameManager.Instance.selectedUnit != null)
        {
            if (isAtteckable)
            {
                TakeDamage(GameManager.Instance.selectedUnit.unitAttack);
            }
        }
        
    }

    public void StartMovement(Vector2 value, float moveStep)
    {
        StartCoroutine(UnitMovement(value, moveStep));
        canMove = false;
    }

    public void HighLightMe(Color color)
    {
        renderer.color = color;
        isAtteckable = true;
    }
    public void UnHighLightMe()
    {
        renderer.color = Color.white;
        isAtteckable = false;
    }
    public void TakeDamage(int value)
    {
        if (GameManager.Instance.selectedUnit.canAtteck)
        {
            if (value > unitArmor)
            {
                unitHealth -= (value - unitArmor);
                Debug.Log(this.name + " Taking Damage:" + (value - unitArmor));
                Debug.Log(this.name + " Current HP:" + unitHealth + "/" + startingHP);
            }
            else
            {
                Debug.Log("to weak attack for unit armor");
            }
            if (unitHealth > 0)
            {
                GameManager.Instance.GrabHaldeToAttacked(this);
                GameManager.Instance.CounterAttack();
            }
            else
            {
                Debug.Log(this.name + " is dead!");
                renderer.enabled = false;
            }
            GameManager.Instance.AfterAttack();
        }
    }
    public void TakeCounterDamage(int value)
    {
        if (GameManager.Instance.attackedUnit.canCounter)
        {
            if (value > unitArmor)
            {
                unitHealth -= (value - unitArmor);
                Debug.Log(this.name + " Taking CouterDamage:" + (value - unitArmor));
                Debug.Log(this.name + " Current HP:" + unitHealth + "/" + startingHP);
            }
            if (unitHealth <= 0)
            {
                Debug.Log(this.name + " is dead!");
                renderer.enabled = false;
            }
        }
    }
    public void NewTurnForUnit()
    {
        canAtteck = true;
        canCounter = true;
        canMove = true;
    }

    IEnumerator UnitMovement(Vector2 tilePosition, float moveStep)
    {
        while (transform.position.x != tilePosition.x || transform.position.y != tilePosition.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, tilePosition, moveStep * Time.deltaTime);
            yield return null;
        }
        /*while (transform.position.y != tilePosition.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePosition.y), moveStep * Time.deltaTime);
            yield return null;
        }*/
    }
}
