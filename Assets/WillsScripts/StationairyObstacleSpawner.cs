using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationairyObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclesPrefabs; // array of obstacles prefabs
    [SerializeField] private Rect spawnArea; // area to spawn the obstacles
    [SerializeField] private bool isSpawned; // is the obstacle spawned
    [SerializeField] private int maxObstacles = 3; // maximum number of obstacles
    private int currentObstacleCount = 0; // current number of spawned obstacles

    void Update()
    {
        if (!isSpawned && currentObstacleCount < maxObstacles)
        {
            isSpawned = true;
            StartCoroutine(StartObstacleGeneration());
        }
    }

    /// <summary>
    /// Start the coroutine to generate obstacles
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartObstacleGeneration()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        GenerateObstacle();
        yield return new WaitForSeconds(Random.Range(1, 3));
        isSpawned = false;
    }

    /// <summary>
    /// Generate the obstacles
    /// </summary>
    void GenerateObstacle()
    {
        int randomObstacle = Random.Range(0, obstaclesPrefabs.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(spawnArea.x, spawnArea.x + spawnArea.width), Random.Range(spawnArea.y, spawnArea.y + spawnArea.height), 0);
        GameObject obstacle = Instantiate(obstaclesPrefabs[randomObstacle], spawnPosition, Quaternion.identity);
        currentObstacleCount++;
        Debug.Log("Obstacle spawned at " + spawnPosition);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(spawnArea.x + spawnArea.width / 2, spawnArea.y + spawnArea.height / 2, 0), new Vector3(spawnArea.width, spawnArea.height, 0));
    }
}
