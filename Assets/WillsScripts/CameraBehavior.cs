using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Transform player;//player game object
    [SerializeField] private float offset;//offset of the camera
    [SerializeField] private LevelManager levelManager;//level manager
    void Start()
    {
        if(levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
    }

    void Update()
    {
        if(levelManager.scenename == "Game")
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            FollowPlayer();
        }
    }
    void FollowPlayer()
    {
        transform.position = new Vector3(player.position.x, player.position.y, offset);
    }
}
