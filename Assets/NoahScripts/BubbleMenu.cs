using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class BubbleSpawner : MonoBehaviour
{
    /* This script spawns bubbles at random positions within the camera's view, and allows the player to pop them by clicking on them.
    Main Menu interaction to use this script*/ 
    
    public List<GameObject> bubblePrefabs; // Prefab for the bubble
    public int maxBubbles = 10; // Maximum number of bubbles on screen
    public float bubbleSpeed = 5f; // Speed of the bubbles
    public float popInDuration = 0.5f; // Radius for popping bubbles
    public float scaleBubble = 0.5f; // Scale of the bubble

    private List<GameObject> bubbles = new List<GameObject>(); // List to store all bubbles
    private Camera mainCamera;
    
    public List<Sprite> faceSprites; // List of face sprites
    
    // Sound effects
    public AudioClip popSound; // Sound effect for popping bubbles
    
    // Particles
    public GameObject popParticles; // Particle effect for popping bubbles
    public GameObject spawnParticles; // Particle effect for spawning bubbles

    void Start()
    {
        mainCamera = Camera.main;
        
        // Spawn initial bubbles
        for (int i = 0; i < maxBubbles; i++)
        {
            StartCoroutine(SpawnBubble());
        }
    }

    void Update() 
    {
        // Check for bubble popping
        if (Input.GetMouseButtonDown(0)) // Left-click to pop bubbles
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            for (int i = bubbles.Count - 1; i >= 0; i--)
            {
                GameObject bubble = bubbles[i];
                if (Vector2.Distance(mousePos, bubble.transform.position) < 0.5f) // Adjust pop radius if needed
                {
                    DestroyBubble(bubble);
                }
            }
        }
    }

    private IEnumerator SpawnBubble() // Spawn a new bubble at a random position
    {
        Vector2 spawnPosition = GetRandomPositionWithinCamera();
        GameObject newBubble = Instantiate(bubblePrefabs[Random.Range(0, bubblePrefabs.Count)], spawnPosition, Quaternion.identity);

        // Set face sprite for the bubble making a child object
        GameObject face = new GameObject("Face");
        face.transform.SetParent(newBubble.transform);
        face.transform.localPosition = Vector3.zero;
        SpriteRenderer faceRenderer = face.AddComponent<SpriteRenderer>();
        if (faceRenderer != null && faceSprites.Count > 0)
        {
            faceRenderer.sprite = faceSprites[Random.Range(0, faceSprites.Count)];
            faceRenderer.sortingOrder = faceRenderer.sortingOrder + 1; // Ensure face is rendered above the bubble
        }

        // Scale the bubble to zero
        newBubble.transform.localScale = Vector3.zero;
        face.transform.localScale = Vector3.zero;

        // Scale in the bubble
        float timer = 0f;
        while (timer < popInDuration)
        {
            newBubble.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * scaleBubble, timer / popInDuration);
            face.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 1.5f, timer / popInDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        newBubble.transform.localScale = Vector3.one * scaleBubble;
        face.transform.localScale = Vector3.one * 1.5f;

        // Assign random direction and velocity
        Rigidbody2D rb = newBubble.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            rb.velocity = randomDirection * bubbleSpeed;
        }

        bubbles.Add(newBubble);
        spawnParticles.transform.position = spawnPosition;
    }

    void DestroyBubble(GameObject bubble) // Destroy a bubble and spawn a new one, using mouse click
    {
        Animator animator = bubble.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Pop"); // Assuming the animation has a trigger parameter named "Pop"
        }

        // Remove the bubble from the list and destroy it immediately
        bubbles.Remove(bubble);
        Destroy(bubble, 0.25f); // Small delay to ensure the animation starts

        if (popSound != null)
        {
            AudioSource.PlayClipAtPoint(popSound, Camera.main.transform.position);
        }

        // Spawn a new bubble
        StartCoroutine(SpawnBubble());
    }

    Vector2 GetRandomPositionWithinCamera() // Get a random position within the camera's view
    {
        float x = Random.Range(0f, 1f);
        float y = Random.Range(0f, 1f);
        Vector3 viewportPosition = new Vector3(x, y, 0);
        Vector3 worldPosition = mainCamera.ViewportToWorldPoint(viewportPosition);
        worldPosition.z = 0; // Keep bubbles in 2D space
        return worldPosition;
    }

    private void FixedUpdate()
    {
        // Check for bubbles bouncing off screen edges
        foreach (GameObject bubble in bubbles)
        {
            Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector3 position = bubble.transform.position;
                Vector3 viewportPosition = mainCamera.WorldToViewportPoint(position);
                
                // Sprite Flip for bubbles
                SpriteRenderer spriteRenderer = bubble.GetComponent<SpriteRenderer>();
                SpriteRenderer faceRenderer = bubble.transform.Find("Face")?.GetComponent<SpriteRenderer>();
                
                if (rb.velocity.x > 0)
                {
                    spriteRenderer.flipX = false;
                    faceRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                    faceRenderer.flipX = true;
                }

                // Bounce off screen edges and reposition if outside
                if (viewportPosition.x < 0f || viewportPosition.x > 1f)
                {
                    position.x = Mathf.Clamp(position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
                    rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
                }

                if (viewportPosition.y < 0f || viewportPosition.y > 1f)
                {
                    position.y = Mathf.Clamp(position.y, mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y);
                    rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
                }

                bubble.transform.position = position;
            }
        }
        
        // Enable bubble collisions
        for (int i = 0; i < bubbles.Count; i++)
        {
            for (int j = i + 1; j < bubbles.Count; j++)
            {
                GameObject bubbleA = bubbles[i]; // Bubble A that moves in to Bubble B 
                GameObject bubbleB = bubbles[j]; // Bubble B that moves in to Bubble A

                Rigidbody2D rbA = bubbleA.GetComponent<Rigidbody2D>();
                Rigidbody2D rbB = bubbleB.GetComponent<Rigidbody2D>();

                if (rbA != null && rbB != null)
                {
                    Vector2 direction = bubbleA.transform.position - bubbleB.transform.position;
                    float distance = direction.magnitude;
                    float radius = 0.5f; // Assuming bubbles have a radius of 0.5

                    if (distance < radius * 2f) // If bubbles overlap
                    {
                        direction = direction.normalized;
                        Vector2 relativeVelocity = rbA.velocity - rbB.velocity;
                        float velocityAlongDirection = Vector2.Dot(relativeVelocity, direction);

                        if (velocityAlongDirection < 0)
                        {
                            Vector2 impulse = direction * velocityAlongDirection * -1f; 
                            rbA.velocity += impulse;
                            rbB.velocity -= impulse;
                        }
                    }
                }
            }
        }
    }
}

