using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public int x, y;
    public bool walkAble = true;

    public PathNode cameFromeNode;

    private void Start()
    {
        x = (int)transform.position.x;
        y = (int)transform.position.y;
    }
}
