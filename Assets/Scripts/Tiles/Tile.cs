using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer renderer;
    public bool isWalkAble = false;
    private enum StartingTile { notStartingTile, leftPlayerStartingTile, rightPlayerStartingTile} // do użycia przy deployment State.
    [SerializeField]
    private StartingTile isStartingTile;


    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        CMEventBroker.EndCurrentState += UnHighLightMe;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.DeploymentLeft && isStartingTile == StartingTile.leftPlayerStartingTile)
        {
            HighLightMe(Color.green);
            isWalkAble = true;
        }
        if (GameManager.Instance.gameState == GameManager.GameState.DeploymentRight && isStartingTile == StartingTile.rightPlayerStartingTile)
        {
            HighLightMe(Color.green);
            isWalkAble = true;
        }
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
