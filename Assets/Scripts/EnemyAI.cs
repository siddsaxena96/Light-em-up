using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public bool isTorched;
    public int health=100;
    public float proximity;
    public Vector3 dir;
    public AudioSource[] vampire_sounds;
    private Vector2 source;   
    private Rigidbody2D rb;
    private Light playerLight;
    private Transform player;
    private bool playaudio=true;
    AudioSource run,vamp;
    void Start() {
        rb = GetComponent<Rigidbody2D> ();
        source = rb.position;
        playerLight = GameObject.FindGameObjectWithTag ("PlayerLight").GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        vampire_sounds = GetComponents<AudioSource> ();
        run = vampire_sounds [0];
        vamp = vampire_sounds [1];
        playaudio=true;
    }

    void Update() {
        float distance = Vector3.Distance (transform.position, player.position);
        if (!isTorched) {           
            if (distance <= proximity) {
              //  Debug.Log ("Prox Less");
                if (playaudio) {
                    vampire_sounds[1].Play();
                    playaudio = false;                   
                }
                rb.velocity = (player.position - transform.position).normalized * speed;
            } else {
               // Debug.Log ("Prox more");
                playaudio=true;
                rb.velocity = dir * speed;
            }
        } else {
//            Debug.Log ("Torched");

            rb.velocity = -(player.position - transform.position).normalized * Mathf.Abs(speed);
        }

    }
        
    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "map" && !isTorched) {
      //      Debug.Log ("HERE");
            speed *= -1;
        } else if (col.tag == "map" && isTorched) {
            speed = 0;
        }
        if (col.tag == "PlayerLightArea") {
            vampire_sounds[0].Play();
            isTorched = true;
        } 
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.tag == "PlayerLightArea") {
            playaudio = true;
            isTorched = false;
            speed = 3.0f;
        }
    }

}
