using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    public float proximityRadius;
    public int health = 100;
    public static bool rotateMirror = false;
    public float rotationSpeed;
    public Rigidbody2D nearMirror;
    private Rigidbody2D rb;
    private float vertical, horizontal;
    private Vector2 vel = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        vel = new Vector2(horizontal, vertical);
        rb.velocity = vel * speed;
    }
}
