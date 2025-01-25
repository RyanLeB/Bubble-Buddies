using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [Header("Level Generation Settings")]
    [SerializeField] private GameObject[] levelSegments; // array of level segments
    [SerializeField] private Transform spawnPoint; // spawn point
    [SerializeField] private int maxActiveSegments; // max active segments
    [SerializeField] private Transform player; // reference to the player
    [SerializeField] private float triggerDistance = 10f; // distance to trigger segment generation

    private Queue<GameObject> activeSegments = new Queue<GameObject>(); // queue of active segments

    void Start()
    {
        for (int i = 0; i < maxActiveSegments; i++)
        {
            GenerateSegment();
        }
    }

    void Update()
    {
        if (Vector3.Distance(player.position, spawnPoint.position) < triggerDistance)
        {
            GenerateSegment();
        }
    }

    /// <summary>
    /// Generate a level segment
    /// </summary>
    public void GenerateSegment()
    {
        int randomSegment = Random.Range(0, levelSegments.Length);
        GameObject segment = Instantiate(levelSegments[randomSegment], spawnPoint.position, Quaternion.identity);
        activeSegments.Enqueue(segment);

        UpdateSpawnPoint(segment);

        if (activeSegments.Count > maxActiveSegments)
        {
            GameObject oldSegment = activeSegments.Dequeue();
            Destroy(oldSegment);
        }
    }

    /// <summary>
    /// Update the spawn point
    /// </summary>
    void UpdateSpawnPoint(GameObject seg)
    {
        Transform exitPoint = seg.transform.Find("ExitPoint");
        if (exitPoint != null)
        {
            spawnPoint.position = exitPoint.position;
        }
    }
}
