using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isTorched;
    [SerializeField] private float proximity;
    [SerializeField] private Vector3 dir;
    [SerializeField] private AudioSource[] vampire_sounds;
    private Vector2 source = Vector2.zero;
    private Rigidbody2D rb = null;
    private Transform player = null;
    private bool playaudio = true;
    private AudioSource run, vamp;
    private bool wallHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        source = rb.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        vampire_sounds = GetComponents<AudioSource>();
        run = vampire_sounds[0];
        vamp = vampire_sounds[1];
        playaudio = true;
    }

    void Update()
    {
        if (player == null)
            return;
        float distance = Vector3.Distance(transform.position, player.position);
        if (!isTorched)
        {
            if (distance <= proximity)
            {
                if (playaudio)
                {
                    vampire_sounds[1].Play();
                    playaudio = false;
                }
                rb.velocity = (player.position - transform.position).normalized * speed;
            }
            else
            {
                playaudio = true;
                rb.velocity = dir * speed;
            }
        }
        else
        {
            rb.velocity = -(player.position - transform.position).normalized * Mathf.Abs(speed);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "map" && !isTorched && !wallHit)
        {
            speed *= -1;
            wallHit = true;
        }
        else if (col.tag == "map" && isTorched)
        {
            speed = 0;
        }
        if (col.tag == "PlayerLightArea")
        {
            vampire_sounds[0].Play();
            isTorched = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "PlayerLightArea")
        {
            playaudio = true;
            isTorched = false;
            speed = Mathf.Abs(speed);
        }
        wallHit = false;
    }

}
