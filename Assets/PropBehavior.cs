using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBehavior : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            Debug.Log("Bubble collided with prop");
            Destroy(other.gameObject);
        }
    }
}
