using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayTracker : MonoBehaviour
{
    [Header("Gameplay Tracker")]
    [Header("Gameplay values")]
    [SerializeField] private int currentScore;//current score
    [SerializeField] private GameObject startPoint;//start point of the level
    [SerializeField] private GameObject endPoint;//end point of the level
    [SerializeField] private GameObject player;//player object
    [SerializeField] private TextMeshProUGUI scoreText;//score text
    [SerializeField] private GameObject[] bubbleBuddy;//bubble buddy prefab
    public int bubblesSaved;//bubbles saved
    [Header("Obstacle values")]
    [SerializeField] private float obstacleSpeed;//speed of the obstacles
    [SerializeField] private Obstacle[] obstacles;//obstacles
    [Header("Checkpoint values")]
    [SerializeField] private int checkpointCount;//number of checkpoints

    private float totalDistance;

    private void Start()
    {
        FindCheckpoints();

        if (startPoint != null && endPoint != null)
        {
            totalDistance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        }
    }

    private void Update()
    {
        TrackPlayerDistance();
        FindBubbleBuddies();
        //BubbleTracker();
    }
    /// <summary>
    /// Track the player distance
    /// </summary>
    public void TrackPlayerDistance()
    {
        if (player != null && startPoint != null)
        {
            float distanceTraveled = Vector3.Distance(startPoint.transform.position, player.transform.position);
            float progress = distanceTraveled / totalDistance;
            currentScore = Mathf.RoundToInt(progress * 100);//Convert to percentage
            // Add points based on the number of bubble buddies
            int bubbleBuddyBonus = bubbleBuddy.Length * 5; // each bubble buddy gives 5 points
            currentScore += bubbleBuddyBonus;

            AddScore(currentScore);
        }
    }
    /// <summary>
    /// Find the Bubble Buddies
    /// </summary>
    public void FindBubbleBuddies()
    {
        bubbleBuddy = GameObject.FindGameObjectsWithTag("Bubble");
    }
    /// <summary>
    /// Add score
    /// </summary>
    public void AddScore(int score)
    {
        // currentScore += score;
        scoreText.text = "Score: " + currentScore;
    }
    /// <summary>
    /// Find the Checkpoints
    /// </summary>
    public void FindCheckpoints()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpointCount = checkpoints.Length;
    }
    /// <summary>
    /// Update the checkpoint count and add bonus score
    /// </summary>
    public void CheckpointReached()
    {
        checkpointCount--;
        FindObstacles();
        AddScore(10);
    }
    /// <summary>
    /// Bubble Tracker for final score
    /// </summary>
    public void BubbleTracker()
    {
        foreach (GameObject bubble in bubbleBuddy)
        {
            if (bubble != null)
            {
                bubblesSaved++;
            }
        }
    }
    /// <summary>
    /// Find all the obstacles in the scene
    /// </summary>
    public void FindObstacles()
    {
        obstacles = FindObjectsOfType<Obstacle>();
        foreach (Obstacle obstacle in obstacles)
        {
            if (obstacle != null)
            {  
                obstacle.speed += obstacleSpeed;
            }
        }
    }
}
