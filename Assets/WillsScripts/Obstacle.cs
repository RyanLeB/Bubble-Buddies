using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 5f;//speed of the obstacle
    [SerializeField] Camera mainCamera;//main camera
    [SerializeField] private float destroyOffset = 0.1f;//offset to destroy the obstacle
    void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        MoveObstacle();
        DestroyWhenOffScreen();
    }
    /// <summary>
    /// Move the obstacle
    /// </summary>
    void MoveObstacle()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    /// <summary>
    /// Destroy the obstacle when it leaves the camera view
    /// </summary>
    void DestroyWhenOffScreen()
    {
        if(mainCamera == null)
        {
            return;
        }
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        if(screenPoint.x < destroyOffset)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player collided with obstacle");
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Bubble"))
        {
            Debug.Log("Bubble collided with obstacle");
            Destroy(other.gameObject);
        }
    }
}
