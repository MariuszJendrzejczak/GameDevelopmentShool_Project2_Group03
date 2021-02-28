using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer renderer;
    public bool isWalkAble = false;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HighLightMe(Color color)
    {
        renderer.color = color;
        isWalkAble = true;
    }

    public void UnHighLightMe()
    {
        renderer.color = Color.white;
        isWalkAble = false;
    }

    private void OnMouseDown()
    {
        if (isWalkAble)
        {
            GameManager.Instance.MoveUnit(this.transform.position);
            GameManager.Instance.UnitSelection(null);
        }
        else
        {
            Debug.Log("Out of range");
        }
    }
}
