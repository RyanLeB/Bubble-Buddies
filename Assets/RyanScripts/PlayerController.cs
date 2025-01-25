using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float floatSpeed = 5f; 
    public float gravity = 2f; 
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        
        rb.velocity += Vector2.down * gravity * Time.deltaTime;

        
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, floatSpeed);
        }
    }
}