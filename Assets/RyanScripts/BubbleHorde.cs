using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleHorde : MonoBehaviour
{
    public Transform player;
    public float attractionSpeed = 2f;
    public float squishFactor = 0.9f;
    public float squishDuration = 0.5f;
    public float squishCooldown = 1f;
    public float recoilFactor = 1.1f;
    public float maxSpeed = 10f;

    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool canSquish = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player object not found. Ensure the player has the 'Player' tag.");
            enabled = false; 
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.isKinematic = false;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.angularDrag = 5f;
        rb.drag = 2f;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<CircleCollider2D>();
        }

        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = 0.1f;
        material.friction = 0.1f;
        collider.sharedMaterial = material;

        originalScale = transform.localScale;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * attractionSpeed);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canSquish)
        {
            StartCoroutine(Squish());
        }
    }

    IEnumerator Squish()
    {
        canSquish = false;
        float elapsedTime = 0f;

        while (elapsedTime < squishDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * squishFactor, elapsedTime / squishDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale * squishFactor;

        elapsedTime = 0f;
        while (elapsedTime < squishDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale * squishFactor, originalScale * recoilFactor, elapsedTime / squishDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale * recoilFactor;

        elapsedTime = 0f;
        while (elapsedTime < squishDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale * recoilFactor, originalScale, elapsedTime / squishDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
        yield return new WaitForSeconds(squishCooldown);
        canSquish = true;
    }
}