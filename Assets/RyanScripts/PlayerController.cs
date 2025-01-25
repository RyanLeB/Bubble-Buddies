using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float floatSpeed = 5f;
    public float gravity = 2f;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; 
    }

    void Update()
    {
        
        Vector2 velocity = new Vector2(moveSpeed, rb.velocity.y);

        
        velocity += Vector2.down * gravity * Time.deltaTime;

        
        if (Input.GetKey(KeyCode.Space))
        {
            velocity = new Vector2(velocity.x, floatSpeed);
        }

        
        rb.velocity = velocity;

        
        Vector2 position = rb.position;
        
        position.y = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);
        rb.position = position;
    }
}