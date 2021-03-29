using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private Sprite activationButtonSprite, inactiveButtonSprite;
    private Unit unit;
    protected bool isActive, canBeActivated;
    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Unit>();
    }

    // Update is called once per frame
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
