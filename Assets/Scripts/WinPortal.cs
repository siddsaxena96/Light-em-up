using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPortal : MonoBehaviour {

	// Use this for initialization
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public bool won=false;
    public SpriteRenderer portalRenderer;
    	
    public void wincondition()
    {
        won = true;
        portalRenderer.sprite = activeSprite;
    }

    void OnTriggerEnter2D(Collider2D col) {
        
        if (won&&col.tag=="Player") {
            GameObject.FindGameObjectWithTag ("GameWonScreen").GetComponent<SpriteRenderer> ().enabled = true;
            GameObject.FindGameObjectWithTag ("GameWonScreen").GetComponent<GameOver> ().playm ();
            Destroy (col.gameObject);
        }
    }
}
