using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationairyObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;//obstacles to spawn
    [SerializeField] private Rect spawnArea;//area to spawn obstacles
    [SerializeField] private float spawnRate;//rate to spawn obstacles
    [SerializeField] private bool canSpawn;//can spawn obstacles
    [SerializeField] private int maxNumObstacles;//max number of obstacles

    void Start()
    {
        StartCoroutine(SpawnObstacles());
        canSpawn = true;
    }
    IEnumerator SpawnObstacles()
    {
        while(canSpawn)
        {
            if(transform.childCount < maxNumObstacles)
            {
                GameObject obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(Random.Range(spawnArea.x, spawnArea.x + spawnArea.width), Random.Range(spawnArea.y, spawnArea.y + spawnArea.height), 0), Quaternion.identity);
                obstacle.transform.parent = transform;
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(spawnArea.x + spawnArea.width / 2, spawnArea.y + spawnArea.height / 2, 0), new Vector3(spawnArea.width, spawnArea.height, 0));
    }
}
