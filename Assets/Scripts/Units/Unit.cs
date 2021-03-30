using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum Owner { Humans, Elfes }
    [SerializeField]
    public Owner owner;
    public enum Tag { Tag1, Tag2, Tag3, Tag4, Tag5, Tag6}
    public Tag myTag, weakOnTag;
    private int tagBonus;
    public float x, y;
    private SpriteRenderer renderer;
    [SerializeField]
    private Sprite developmentModeSprite, normalModeSprote;
    public bool isSelected = false, sanctuary = false;
    public bool passiveSkillPush = false;
    [SerializeField]
    public int baseUnitSpeed, baseUnitHealth, baseUnitArmor, baseUnitAttack, baseUnitAttackRange;
    public int unitSpeed, unitHealth, unitAttack, unitArmor, unitAttackRange;
    public bool canMove, canAtteck, canCounter, isAtteckable = false;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CMEventBroker.ChangeGameMode += ChangeMode;
        // poniższe do wrzucenia w stan, przed turą
        canMove = true;
        canAtteck = true;
        canCounter = true;

        SetBaseStatisticValues();
        unitHealth = baseUnitHealth;
    }
    private void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
    }
    private void OnMouseDown()
    {
        switch(GameManager.Instance.gameState)
        {
            case GameManager.GameState.UnitChoosing:
                CMEventBroker.CallUnitChoosed(this.gameObject);
                Debug.Log(this.name + "został wysłany (UnitScript)");
                break;
            case GameManager.GameState.LeftPlayerTurn:
                if (owner == Owner.Humans)
                {
                    if (GameManager.Instance.selectedUnit == this)
                    {
                        GameManager.Instance.UnitSelection(null, 0, 0, 0, 0, 0);
                        isSelected = false;
                    }
                    else if (GameManager.Instance.selectedUnit == null)
                    {
                        GameManager.Instance.UnitSelection(this, unitAttack, unitArmor, unitHealth, unitSpeed, unitAttackRange);
                        isSelected = true;
                    }
                }
                if (GameManager.Instance.selectedUnit != null && owner == Owner.Elfes)
                    {
                        if (isAtteckable)
                        {
                            if (sanctuary)
                            {
                                // informacja o sanktuarium
                                Debug.Log("Ten Unit ma na sobie czar sanctuarium");
                            }
                            else
                            {
                                TakeDamage(GameManager.Instance.selectedUnit.unitAttack);
                            }
                        }
                    }
                
                break;
            case GameManager.GameState.RightPlayerTurn:
                {
                    if (owner == Owner.Elfes)
                    {
                        if (GameManager.Instance.selectedUnit == this)
                        {
                            GameManager.Instance.UnitSelection(null, 0, 0, 0, 0, 0);
                            isSelected = false;
                        }
                        else if (GameManager.Instance.selectedUnit == null)
                        {
                            GameManager.Instance.UnitSelection(this, unitAttack, unitArmor, unitHealth, unitSpeed, unitAttackRange);
                            isSelected = true;
                        }
                    }
                    if (GameManager.Instance.selectedUnit != null && owner == Owner.Humans)
                        {
                            if (sanctuary)
                            {
                                // informacja o sanktuarium
                                Debug.Log("Ten Unit ma na sobie czar sanctuarium");
                            }
                            else
                            {
                                TakeDamage(GameManager.Instance.selectedUnit.unitAttack);
                            }
                        }
                    
                }
                break;
            case GameManager.GameState.SkillSwapUnits:
                CMEventBroker.CallSwapUnitsPeak(this.gameObject);
                break;
        }
    }

    private void OnMouseEnter()
    {
        switch (GameManager.Instance.gameState)
        {
            case GameManager.GameState.UnitChoosing:
                switch (owner)
                {
                    case Owner.Humans:
                        break;
                    case Owner.Elfes:
                        break;
                }
                transform.localScale += Vector3.one * 1.5f;
                break;
        }

        if (GameManager.Instance.selectedUnit != null && GameManager.Instance.selectedUnit != this)
        {
            // miejsce do wywołania eventu gdy mamy wybrany unit w grze, i najeżdżamy na inny unit, żęby przekazać tego unitu statystyki do UI (np. OnMauseEntarOnEnemy)
        }
    }

    private void OnMouseExit()
    {

        switch (GameManager.Instance.gameState)
        {
            case GameManager.GameState.UnitChoosing:
                transform.localScale -= Vector3.one * 1.5f;
                break;
        }
    }

    private void ChangeMode()
    {
        switch (GameManager.Instance.gameMode)
        {
            case GameManager.GameMode.DeveloperMode:
                if (developmentModeSprite)
                {
                    renderer.sprite = developmentModeSprite;
                }
                break;
            case GameManager.GameMode.NormalMode:
                if (normalModeSprote)
                {
                    renderer.sprite = normalModeSprote;
                }
                break;
        }
    }
    public void SetBaseStatisticValues()
    {
        unitSpeed = baseUnitSpeed;
        unitArmor = baseUnitArmor;
        unitAttack = baseUnitAttack;
        unitAttackRange = baseUnitAttackRange;
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
            if (GameManager.Instance.selectedUnit.myTag == weakOnTag)
            {
                if (value + GameManager.Instance.selectedUnit.tagBonus > unitArmor)
                {
                    unitHealth -= (value + GameManager.Instance.selectedUnit.tagBonus - unitArmor);
                    Debug.Log(this.name + " Taking Damage:" + (value - unitArmor));
                    Debug.Log(this.name + " Current HP:" + unitHealth + "/" + baseUnitHealth);
                }
            }
            else
            {
                if (value > unitArmor)
                {
                    unitHealth -= (value - unitArmor);
                    Debug.Log(this.name + " Taking Damage:" + (value - unitArmor));
                    Debug.Log(this.name + " Current HP:" + unitHealth + "/" + baseUnitHealth);
                }
                else
                {
                    Debug.Log("to weak attack for unit armor");
                }
            }
            
            if (unitHealth > 0)
            {
                if (GameManager.Instance.selectedUnit.passiveSkillPush == false)
                {
                    GameManager.Instance.GrabHaldeToAttacked(this, unitAttack, unitArmor, unitHealth, unitSpeed, unitAttackRange);
                    GameManager.Instance.CounterAttack();
                }
                else
                {
                    GameManager.Instance.GrabHaldeToAttacked(this, unitAttack, unitArmor, unitHealth, unitSpeed, unitAttackRange);
                }
                
            }
            else
            {
                Debug.Log(this.name + " is dead!");
                renderer.enabled = false;
            }
            if (GameManager.Instance.selectedUnit.passiveSkillPush == true)
            {
                TakePush(GameManager.Instance.selectedUnit);
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
                Debug.Log(this.name + " Current HP:" + unitHealth + "/" + baseUnitHealth);
            }
            if (unitHealth <= 0)
            {
                Debug.Log(this.name + " is dead!");
                renderer.enabled = false;
            }
        }
    }
    
    public void GetBonus(int attack, int armor, int speed, int attackRange)
    {
        unitAttack += attack;
        unitArmor += armor;
        unitSpeed += speed;
        unitAttackRange += attackRange;
    }

    public void NewTurnForUnit() // być może do wywalenie później
    {
        canAtteck = true;
        canCounter = true;
        canMove = true;
        sanctuary = false;
    }

    public void TakePush(Unit unit)
    {
        float realativeX = x - unit.x;
        float relativeY = y - unit.y;
        Debug.Log(realativeX + " " + relativeY);
        if (realativeX > 0)
        {
            transform.position += new Vector3(-1,0,0);
        }
        else if (realativeX < 0)
        {
            transform.position += new Vector3(1, 0, 0);
        }
        if (relativeY > 0)
        {
            transform.position += new Vector3(0, -1, 0);
        }
        else if (relativeY < 0)
        {
            transform.position += new Vector3(0, 1, 0);
        }
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
