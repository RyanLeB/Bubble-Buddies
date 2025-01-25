using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject bubbleBuddy;//bubble buddy prefab
    [SerializeField] private GameplayTracker gamePlayTracker;//game play tracker
    void Start()
    {
        gamePlayTracker = FindObjectOfType<GameplayTracker>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(bubbleBuddy, transform.position, Quaternion.identity);
            gamePlayTracker.CheckpointReached();
        }
    }
}
