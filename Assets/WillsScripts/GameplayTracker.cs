using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayTracker : MonoBehaviour
{
    [Header("Gameplay Tracker")]
    [Header("Gameplay values")]
    public float currentScore;//current score
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
    private float pointsPerSecond = 0.1f; // Points added per second

    private void Start()
    {
        FindCheckpoints();
        if(startPoint == null)
        {
            startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        }
        if(endPoint == null)
        {
            endPoint = GameObject.FindGameObjectWithTag("WinTrig");
        }
        if (startPoint != null && endPoint != null)
        {
            totalDistance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        }
    }

    private void Update()
    {
        FindBubbleBuddies();
        AddScorePerSecond();
        //BubbleTracker();
    }
    /// <summary>
    /// Track the player distance
    /// </summary>
    public void TrackPlayerDistance()
    {
        if (player != null && startPoint != null)
        {
            float distance = Vector3.Distance(player.transform.position, startPoint.transform.position);

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
    public void AddScore(float score)
    {
        currentScore += score;
        scoreText.text = "Score: " + currentScore.ToString("F2");
    }
    /// <summary>
    /// Add score per second
    /// </summary>
    private void AddScorePerSecond()
    {
        currentScore += pointsPerSecond;
        scoreText.text = "Score: " + currentScore.ToString("F2");
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
