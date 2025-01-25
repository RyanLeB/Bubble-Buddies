using UnityEngine;

public class CameraScrolling : MonoBehaviour
{
    public Transform player;
    public float offset = 5f; 

    void Update()
    {
        if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.x += offset; 
            newPosition.y = transform.position.y; 
            newPosition.z = transform.position.z; 
            transform.position = newPosition;
        }
    }
}