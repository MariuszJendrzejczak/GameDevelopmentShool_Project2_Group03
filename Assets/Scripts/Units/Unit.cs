using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
   
    private int HealthPoints { get; set; }
    private int AttackValue { get; set; }
    private int ArmorValue { get; set; }
    private int SpeedValue { get; set; }


    private SpriteRenderer renderer;
    private bool isSelected = false;
    [SerializeField]
    private int unitSpeed;
    public bool hasMoved, hasAttecked, isAtteckable = false;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        if (isSelected)
        {
            GameManager.Instance.UnitSelection(null, 0);
            isSelected = false;
        }
        else
        {
            GameManager.Instance.UnitSelection(this, unitSpeed);
            isSelected = true;
        }
    }

    public void StartMovement(Vector2 value, float moveStep)
    {
        StartCoroutine(UnitMovement(value, moveStep));
    }

    public void HighLightMe(Color color)
    {
        Debug.Log("Light");
        renderer.color = color;
    }
    public void UnHighLightMe()
    {
        renderer.color = Color.white;
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
