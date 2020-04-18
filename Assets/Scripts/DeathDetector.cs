using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDetector : MonoBehaviour {
    private AudioSource death;   
    public GameObject player;       
    void OnTriggerEnter2D(Collider2D col){
        
        if (col.tag == "Enemy") {
            GameObject.FindGameObjectWithTag ("GameOverScreen").GetComponent<SpriteRenderer> ().enabled = true;           
            GameObject.FindGameObjectWithTag ("GameOverScreen").GetComponent<GameOver> ().playm ();      
            Destroy (player);

        }
    }
}
