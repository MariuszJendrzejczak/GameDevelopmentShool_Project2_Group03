using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Pathfinding : MonoBehaviour
{
    [SerializeField]
    private PathNode startNode, endNode;
    [SerializeField]
    private int xDistance, yDistance;
    [SerializeField]
    private int xBuffor = 0, yBuffor = 0;
    private bool brakeBool = false, wasObstacle = false;
    public List<PathNode> pathNodeList;
    [SerializeField]
    private List<PathNode> nodeList;

    public void GetNodeList()
    {
        foreach (GameObject tile in GameManager.Instance.tilesList)
        {
            nodeList.Add(tile.GetComponent<PathNode>());
        }
    }
    public void ClearPathList()
    {
        pathNodeList.Clear();
    }

    private void CalculateDistance()
    {
        xDistance = endNode.x - startNode.x;
        yDistance = endNode.y - startNode.y;
    }

    public void FindAPath(PathNode start, PathNode end)
    {
        startNode = start;
        endNode = end;
        CalculateDistance();
        int counter = 0;
        while (xBuffor != Mathf.Abs(xDistance) || yBuffor != yDistance)
        {
            counter++;
            if (counter >= 10)
            {
                break;
            }
            if (xBuffor != xDistance +1)
            {
                CheckHozizontalPath();
            }
            if (yBuffor != yDistance)
            {
                CheckVerticalPath();
            }
        }  
    }
    private void CheckHozizontalPath()
    {
        Debug.Log("CheckHorizontal");
        if (xDistance > 0)
        {
            for (int i = xBuffor; i < xDistance; i++)
            {
                if (brakeBool)
                {
                    brakeBool = false;
                    break;
                }
                foreach (PathNode node in nodeList)
                {
                    if (node.x == startNode.x + (i + 1) && node.y == startNode.y + yBuffor)
                    {
                        if (node.walkAble)
                        {
                            pathNodeList.Add(node);
                            Debug.Log("+");
                            xBuffor = i;
                            break;
                        }
                        else
                        {
                            xBuffor = i - 1;
                            brakeBool = true;
                            wasObstacle = true;
                            break;
                        }
                    }
                }
            }
        }
        if (xDistance < 0)
        {
            for (int i = xBuffor; i > xDistance; i--)
            {
                if (brakeBool)
                {
                    brakeBool = false;
                    break;
                }
                foreach (PathNode node in nodeList)
                {
                    if (node.x == startNode.x + (i - 1) && node.y == startNode.y + yBuffor)
                    {
                        if (node.walkAble)
                        {
                            pathNodeList.Add(node);
                            Debug.Log("Horizontal - Node" + node.x + " " + node.y);
                            xBuffor = i;
                            break;
                        }
                        else
                        {
                            xBuffor = i + 1;
                            brakeBool = true;
                            wasObstacle = true;
                            break;
                        }
                    }
                }
            }
        }  
    }
          
    private void CheckVerticalPath()
    {
        Debug.Log("CheckVertical");
        foreach (PathNode node in nodeList)
        {
            if(xDistance > 0)
            {
                if (yDistance > 0)
                {
                    if (node.x == startNode.x + xBuffor + 1 && node.y == startNode.y + yBuffor + 1)
                    {
                        if (node.walkAble)
                        {
                            pathNodeList.Add(node);
                            Debug.Log("Vertical + Node" + node.x + " " + node.y);
                            yBuffor++;
                           /* if (wasObstacle)
                            {
                                xBuffor--;
                                wasObstacle = false;
                            }*/
                            break;
                        }
                    }
                }
                else if (yDistance < 0)
                {
                    if (node.x == startNode.x + xBuffor + 1 && node.y == startNode.y + yBuffor - 1)
                    {
                        if (node.walkAble)
                        {
                            pathNodeList.Add(node);
                            Debug.Log("Vertical + Node" + node.x + " " + node.y);
                            yBuffor--;
                           /* if (wasObstacle)
                            {
                                xBuffor--;
                                wasObstacle = false;
                            }*/
                            break;
                        }
                    }
                }    
            }
            else if (xDistance < 0)
            {
                if (yDistance > 0)
                {
                    if (node.x == startNode.x + xBuffor - 1 && node.y == startNode.y + yBuffor + 1)
                    {
                        if (node.walkAble)
                        {
                            pathNodeList.Add(node);
                            Debug.Log(xBuffor);
                            Debug.Log("Vertical - Node" + node.x + " " + node.y);
                            yBuffor++;
                           /* if (wasObstacle)
                            {
                                xBuffor++;
                                wasObstacle = false;
                            }*/
                            break;
                        }
                    }
                }
                else if (yDistance < 0)
                {
                    if (node.x == startNode.x + xBuffor - 1 && node.y == startNode.y + yBuffor - 1)
                    {
                        if (node.walkAble)
                        {
                            pathNodeList.Add(node);
                            Debug.Log("Vertical - Node" + node.x + " " + node.y);
                            yBuffor--;
                           /* if (wasObstacle)
                            {
                                xBuffor++;
                                wasObstacle = false;
                            }*/
                            break;
                        }
                    }
                }
            }
        }
    }
}