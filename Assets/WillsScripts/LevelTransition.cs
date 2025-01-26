using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [Header("Level Generation Settings")]
    [SerializeField] private List<GameObject> LevelPrefab; // level prefab
    [SerializeField] private int levelIndex; // level index
    [SerializeField] private Transform playerTransform; // reference to the player's transform
    [SerializeField] private float distanceInFrontOfPlayer = 10f; // distance in front of the player to spawn the level
    [SerializeField] private int maxLevelInScene = 1; // maximum number of levels in the scene
    [SerializeField] private int spawnedLevelCount = 0; // number of levels spawned in the scene

    private GameObject currentLevelSegment;

    void Start()
    {
        Debug.Log("Starting level transition");
        ReloadLevel();
    }

    public void SpawnNextLevel()
    {
        if (spawnedLevelCount >= maxLevelInScene)
        {
            Debug.Log("Maximum level count reached. Deleting previous level segment.");
            DeletePreviousLevelSegment();
        }

        Debug.Log("Spawning next level");
        levelIndex++;
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * distanceInFrontOfPlayer;
        currentLevelSegment = Instantiate(LevelPrefab[levelIndex], spawnPosition, Quaternion.identity);
        spawnedLevelCount++;

        // Add a new level prefab to the list
        AddNewLevelPrefab();
    }

    void DeletePreviousLevelSegment()
    {
        if (currentLevelSegment != null)
        {
            Destroy(currentLevelSegment);
            spawnedLevelCount--;
        }
    }

    void AddNewLevelPrefab()
    {
        // Assuming you have a method to get a new level prefab
        GameObject newLevelPrefab = GetNewLevelPrefab();
        if (newLevelPrefab != null)
        {
            LevelPrefab.Add(newLevelPrefab);
            Debug.Log("New level prefab added to the list.");
        }
    }

    GameObject GetNewLevelPrefab()
    {
        LevelPrefab.Add(LevelPrefab[levelIndex]);
        return LevelPrefab[levelIndex];
    }
    public void ReloadLevel()
    {
        Debug.Log("Reloading level");
        levelIndex = 0;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * distanceInFrontOfPlayer;
        currentLevelSegment = Instantiate(LevelPrefab[levelIndex], spawnPosition, Quaternion.identity);
        spawnedLevelCount++;
    }
}
