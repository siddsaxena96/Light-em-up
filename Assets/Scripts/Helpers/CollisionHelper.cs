using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionEvent : UnityEvent<string>
{

}

public class CollisionHelper : MonoBehaviour
{
    [SerializeField]
    public CollisionEvent onCollision;

    private void OnCollisionEnter2D(Collision2D other)
    {
        onCollision?.Invoke(other.gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        onCollision?.Invoke(other.gameObject.tag);
    }
}
