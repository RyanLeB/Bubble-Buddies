using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("Obstacle Generation")]
    [SerializeField] private GameObject[] obstaclesPrefabs;//array of obstacles prefabs
    [SerializeField] private Transform spawnPoint;//array of spawn points
    [SerializeField] private float timeBetweenObstacles;//time between obstacles
    [SerializeField] private bool isSpawned;//is the obstacle spawned

    /// <summary>
    /// Start the coroutine to generate obstacles
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartObstacleGeneration()
    {
        yield return new WaitForSeconds(timeBetweenObstacles);
        GenerateObstacle();
        yield return new WaitForSeconds(timeBetweenObstacles);
    }
    /// <summary>
    /// Generate the obstacles
    /// </summary>
    void GenerateObstacle()
    {
        int randomObstacle = Random.Range(0, obstaclesPrefabs.Length);
        GameObject obstacle = Instantiate(obstaclesPrefabs[randomObstacle], spawnPoint.position, Quaternion.identity);
        isSpawned = false;
        //obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(-scrollSpeed, 0);
    }
    void Update()
    {
        if(!isSpawned)
        {
            isSpawned = true;
            StartCoroutine(StartObstacleGeneration());
        }
    }
}
