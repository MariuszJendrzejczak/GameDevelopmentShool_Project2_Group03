using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private bool isSelected = false;
    [SerializeField]
    private int unitSpeed;
 

    private void OnMouseDown()
    {
        if (isSelected)
        {
            GamaManager.Instance.UnitSelection(null, 0);
            isSelected = false;
        }
        else
        {
            GamaManager.Instance.UnitSelection(this, unitSpeed);
            isSelected = true;
        }
    }

    public void StartMovement(Vector2 value, float moveStep)
    {
        StartCoroutine(UnitMovement(value, moveStep));
    }

    IEnumerator UnitMovement(Vector2 tilePosition, float moveStep)
    {
        while (transform.position.x != tilePosition.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePosition.x, transform.position.y), moveStep * Time.deltaTime);
            yield return null;
        }
        while (transform.position.y != tilePosition.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePosition.y), moveStep * Time.deltaTime);
            yield return null;
        }

        
    }
}
