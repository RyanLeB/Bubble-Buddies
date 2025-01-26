using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPropGenerator : MonoBehaviour
{
    [Header("Prop Generation")]
    [SerializeField] private GameObject[] propsPrefabs; // array of props prefabs
    [SerializeField] private Transform spawnPoint; // spawn point
    [SerializeField] private float timeBetweenProps; // time between props
    [SerializeField] private bool isSpawned; // is the prop spawned

    /// <summary>
    /// Start the coroutine to generate props
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartPropGeneration()
    {
        yield return new WaitForSeconds(timeBetweenProps);
        GenerateProp();
        yield return new WaitForSeconds(timeBetweenProps);
    }

    /// <summary>
    /// Generate the props
    /// </summary>
    void GenerateProp()
    {
        int randomProp = Random.Range(0, propsPrefabs.Length);
        GameObject propPrefab = propsPrefabs[randomProp];

        // Check the tag of the prop prefab and set a specific y position
        Vector3 spawnPosition = spawnPoint.position;
        if (propPrefab.CompareTag("Coral"))
        {
            spawnPosition.y = -23; 
            spawnPosition.z = 0;
        }
        else if (propPrefab.CompareTag("Debris"))
        {
            spawnPosition.y = -26;
        }

        GameObject prop = Instantiate(propPrefab, spawnPosition, Quaternion.identity);
        prop.AddComponent<DestroyAfterTime>().SetLifetime(60f); // Add the component to destroy after a set time
        isSpawned = false;
    }

    void Update()
    {
        if (!isSpawned)
        {
            isSpawned = true;
            StartCoroutine(StartPropGeneration());
        }
    }
}

public class DestroyAfterTime : MonoBehaviour
{
    private float lifetime;

    public void SetLifetime(float time)
    {
        lifetime = time;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
