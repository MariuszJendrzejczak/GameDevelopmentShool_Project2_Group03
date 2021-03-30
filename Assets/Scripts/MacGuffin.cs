﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacGuffin : MonoBehaviour
{
    [SerializeField]
    private GameObject placeHolder, guffin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
        {
            collision.GetComponent<Unit>().GrabMacGuffin(this.gameObject);
            transform.SetParent(collision.transform);
        }

    }
}