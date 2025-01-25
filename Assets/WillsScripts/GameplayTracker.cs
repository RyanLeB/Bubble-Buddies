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
    [Header("Obstacle values")]
    [SerializeField] private float obstacleSpeed;//speed of the obstacles
    [Header("Checkpoint values")]
    [SerializeField] private int checkpointCount;//number of checkpoints

    private float totalDistance;

    private void Start()
    {
        if (startPoint != null && endPoint != null)
        {
            totalDistance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        }
    }

    private void Update()
    {
        TrackPlayerDistance();
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
            AddScore(1);
        }
    }
    /// <summary>
    /// Add score
    /// </summary>
    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score: " + currentScore;
    }
}
