using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Sprite basicSprite = null;
    [SerializeField] private Sprite hitSprite = null;
    [SerializeField] private Material unlit = null;
    [SerializeField] private Material lit = null;
    [SerializeField] private SpriteRenderer[] sprites = null;

    public void ChangeMaterial(bool status)
    {
        for (int i = 0; i < sprites.Length; i++)
            sprites[i].material = status ? lit : unlit;
    }

    public void ChangeSprite(bool status)
    {
        spriteRenderer.sprite = status ? hitSprite : basicSprite;
    }
}
