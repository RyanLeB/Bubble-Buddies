using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float inertia = 0.98f; 
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            Vector2 inputDirection = new Vector2(moveX, moveY).normalized;
            rb.velocity = inputDirection * moveSpeed;
        }
        else
        {
            rb.velocity *= inertia;
        }
    }
}