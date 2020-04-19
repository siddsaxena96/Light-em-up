using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPortal : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private bool won = false;
    [SerializeField] private SpriteRenderer portalRenderer;

    public void OnGameWon()
    {
        won = true;
        portalRenderer.sprite = activeSprite;
    }

    public void ResetWinCondition()
    {
        won = false;
        portalRenderer.sprite = inactiveSprite;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (won && col.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameWonScreen").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.FindGameObjectWithTag("GameWonScreen").GetComponent<GameOver>().playm();
            Destroy(col.gameObject);
        }
    }
}
