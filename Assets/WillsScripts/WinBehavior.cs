using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBehavior : MonoBehaviour
{
    [SerializeField] private GameObject player;//player game object
    [SerializeField] private UIManager uIManager;//UI manager
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            uIManager.WinGame();
            player.SetActive(false);
        }
    }
}
