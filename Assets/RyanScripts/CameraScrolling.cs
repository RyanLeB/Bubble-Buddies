using UnityEngine;

public class CameraScrolling : MonoBehaviour
{
    public Transform player;
    public float offset = 5f; 
    [SerializeField] private LevelManager levelManager;


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
        Vector3 newPosition = player.position;
        newPosition.x += offset; 
        newPosition.y = transform.position.y; 
        newPosition.z = transform.position.z; 
        transform.position = newPosition;
    }
}