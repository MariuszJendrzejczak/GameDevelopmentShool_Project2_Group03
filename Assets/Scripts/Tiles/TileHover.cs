using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHover : MonoBehaviour
{
    /* [SerializeField][Tooltip("Using this varable You can channge Hover effect when mause enters and exits a tile")]
     private float hoverAmount;


     [SerializeField]
     private Texture2D cursorArrow;
     [SerializeField]
     private Texture2D cursorChange;


     private void Start()
     {
         Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
     }

     private void OnMouseEnter()
     {
        *//* transform.localScale += Vector3.one * hoverAmount;*//*
         Cursor.SetCursor(cursorChange, Vector2.zero, CursorMode.ForceSoftware);
     }

     private void OnMouseExit()
     {
         *//* transform.localScale -= Vector3.one * hoverAmount;*//*
         Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
     }*/


    private Color basicColor = Color.white;
    private Color hoverColor = new Color(0.7735f, 0.2138951f, 0.2079922f, 1f);
    private Renderer renderer;
    [SerializeField]
    private Texture2D cursorArrow;
    [SerializeField]
    private Texture2D cursorChange;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = basicColor;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseEnter()
    {
        renderer.material.color = hoverColor;
        Cursor.SetCursor(cursorChange, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseExit()
    {
        renderer.material.color = basicColor;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
}
