using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpriteRandomizer : MonoBehaviour
{
    private SpriteRenderer renderer;
    [SerializeField]
    private Sprite[] tileSprites;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = tileSprites[Random.Range(0, tileSprites.Length)];
    }
}
