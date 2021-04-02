using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer renderer;
    public bool isWalkAble = false;
    public enum StartingTile { notStartingTile, leftPlayerStartingTile, rightPlayerStartingTile} // do użycia przy deployment State.
    [SerializeField]
    public StartingTile isStartingTile;
    private enum TileState { WalkAble, Ocupated, Obstacle}
    private TileState tileState;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        CMEventBroker.EndCurrentState += UnHighLightMe;
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
        switch (GameManager.Instance.gameState)
        {
            case GameManager.GameState.LeftPlayerTurn:
            case GameManager.GameState.RightPlayerTurn:
                if (isWalkAble)
                {
                    GameManager.Instance.MoveUnit(this.GetComponent<PathNode>());
                    GameManager.Instance.UnitSelection(null, 0, 0, 0, 0, 0, null);
                }
                else
                {
                    Debug.Log("Out of range");
                }
                break;
            case GameManager.GameState.DeploymentLeft:
            case GameManager.GameState.DeploymentRight:
                if (isWalkAble)
                {
                    if(GameManager.Instance.selectedUnit != null)
                    {
                        GameManager.Instance.selectedUnit.transform.position = this.transform.position;
                    }

                }
                break;
        }      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(tileState != TileState.Obstacle)
        {
            if (collision.tag == "Unit")
            {
                tileState = TileState.Ocupated;
            }
        }     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tileState != TileState.Obstacle)
        {
            if (collision.tag == "Unit")
            {
                tileState = TileState.Ocupated;
            }
        }
    }
}
