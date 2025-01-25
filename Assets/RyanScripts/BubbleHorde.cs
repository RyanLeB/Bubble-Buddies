using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleHorde : MonoBehaviour
{
    public Transform player;
    public float attractionSpeed = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.isKinematic = false;
        rb.freezeRotation = true; 

        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            gameObject.AddComponent<CircleCollider2D>();
        }
    }

    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * attractionSpeed);
    }
}