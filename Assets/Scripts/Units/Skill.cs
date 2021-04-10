using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private Sprite activationButtonSprite, inactiveButtonSprite;
    protected Unit unit;
    protected bool isActive, canBeActivated;

    void Start()
    {
        unit = GetComponent<Unit>();
    }

    void Update()
    {
        if (isActive)
        {
            ChangeButtonSprite();
        }
    }

    private void ChangeButtonSprite()
    {
        if (unit.canAtteck && unit.canMove)
        {
            canBeActivated = true;
            // UI miejsce na spritea aktywującego skill 
            //= activationButtonSprite;
        }
        else
        {
            canBeActivated = false;
            // UI miejsce na spritea aktywującego skill 
            //= inactiveButtonSprite;
        }
    }
}
