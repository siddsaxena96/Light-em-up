using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPortal : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite = null;
    [SerializeField] private Sprite inactiveSprite = null;
    [SerializeField] private bool won = false;
    [SerializeField] private SpriteRenderer portalRenderer = null;
    [SerializeField] private bool isTutorialScene = false;

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
            if (isTutorialScene)
            {
                FindObjectOfType<TutorialSystem>().TutorialComplete();
            }
            else
            {
                GameObject.FindGameObjectWithTag("GameWonScreen").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.FindGameObjectWithTag("GameWonScreen").GetComponent<GameOver>().playm();
                Destroy(col.gameObject);
            }
        }
    }
}
