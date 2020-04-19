using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Sprite basicSprite;
    [SerializeField] private Sprite hitSprite;
    [SerializeField] private Material unlit;
    [SerializeField] private Material lit;
    [SerializeField] private SpriteRenderer[] sprites;

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
