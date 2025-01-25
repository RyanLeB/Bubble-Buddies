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
    void Start()
    {
        gamePlayTracker = FindObjectOfType<GameplayTracker>();
        checkpointText.text = "";
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
            Instantiate(bubbleBuddy, transform.position, Quaternion.identity);
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
