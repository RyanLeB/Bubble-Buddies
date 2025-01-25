using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Camera mainCamera;
    [SerializeField] private float destroyOffset = 0.1f;
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
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
