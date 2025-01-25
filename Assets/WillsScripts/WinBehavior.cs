using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBehavior : MonoBehaviour
{
    [SerializeField] private GameObject player;//player game object
    [SerializeField] private LevelTransition levelTransition;//level transition
    void Start()
    {
        levelTransition = FindObjectOfType<LevelTransition>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            levelTransition.SpawnNextLevel();
        }
    }
}
