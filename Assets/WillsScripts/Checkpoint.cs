using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject bubbleBuddy;//bubble buddy prefab
    [SerializeField] private GameplayTracker gamePlayTracker;//game play tracker
    [SerializeField] private int bubbleBuddyBonus;//bonus bubble buddies, VALUE FOUND IN INSPECTOR
    [SerializeField] private TextMeshProUGUI checkpointText;//checkpoint text
    [SerializeField] private Obstacle[] obstacles;//obstacles
    [SerializeField] private float spawnRadius = 1.0f; // radius to check for obstacles

    void Start()
    {
        gamePlayTracker = FindObjectOfType<GameplayTracker>();
        checkpointText.text = "";
        FindObstacles();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            BubbleBuddyBonus();
            StartCoroutine(CheckpointText());
            gamePlayTracker.CheckpointReached();
        }
    }

    /// <summary>
    /// Spawn bubble buddy after checkpoint is reached
    /// </summary>
    void BubbleBuddyBonus()
    {
        for(int i = 0; i < bubbleBuddyBonus; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            Instantiate(bubbleBuddy, spawnPosition, Quaternion.identity);
        }
    }

    /// <summary>
    /// Get a valid spawn position that is not colliding with obstacles
    /// </summary>
    Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPosition;
        bool validPosition = false;

        do
        {
            spawnPosition = transform.position + (Vector3)(Random.insideUnitCircle * spawnRadius);
            validPosition = true;

            foreach (var obstacle in obstacles)
            {
                if (Vector3.Distance(spawnPosition, obstacle.transform.position) < obstacle.GetComponent<Collider2D>().bounds.extents.magnitude)
                {
                    validPosition = false;
                    break;
                }
            }
        } while (!validPosition);

        return spawnPosition;
    }

    /// <summary>
    /// Find the obstacles
    /// </summary>
    void FindObstacles()
    {
        if(obstacles.Length == 0)
        {
            obstacles = FindObjectsOfType<Obstacle>();
        }
    }

    /// <summary>
    /// Checkpoint text
    /// </summary>
    IEnumerator CheckpointText()
    {
        checkpointText.text = "Checkpoint Reached!";
        yield return new WaitForSeconds(2);
        checkpointText.text = "";
    }   
}
