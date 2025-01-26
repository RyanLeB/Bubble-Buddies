using UnityEngine;
using TMPro;
using System.Collections;

public class ClickerScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI missesText;
    public TextMeshProUGUI finalScoreText; // Add this for the final score text
    public GameObject resultsScreen; // Add this for the results screen UI
    public AudioSource sfxSource;
    public AudioClip comboSound;
    private int score;
    private int comboMultiplier;
    private int misses; // Track the number of misses
    private Coroutine pulseCoroutine;
    private Coroutine rainbowCoroutine;
    private Coroutine flashCoroutine;
    public float maxScaleFactor = 1.5f; // Maximum scale factor for pulsing
    private LevelManager levelManager; // Reference to LevelManager

    void Start()
    {
        score = 0;
        comboMultiplier = 1;
        misses = 0; // Initialize misses
        UpdateScoreText();
        UpdateComboText();
        UpdateMissesText(); // Initialize misses text
        levelManager = FindObjectOfType<LevelManager>(); // Get LevelManager component
        resultsScreen.SetActive(false); // Ensure results screen is initially inactive
    }

    public void AddScore(int points)
    {
        score += points * comboMultiplier;
        comboMultiplier++;
        UpdateScoreText();
        UpdateComboText();
        if (comboMultiplier >= 3)
        {
            StartPulsing();
        }
        if (comboMultiplier >= 20)
        {
            StartRainbowEffect();
        }
        if (comboMultiplier % 10 == 0)
        {
            PlayComboSound();
        }
    }

    public void ResetCombo()
    {
        comboMultiplier = 1;
        UpdateComboText();
        StopPulsing();
        StopRainbowEffect();
        misses++; // Increment misses
        UpdateMissesText(); // Update misses text
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(FlashMissesText()); // Start flashing effect
        CheckMisses(); // Check if misses reached the limit
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void UpdateComboText()
    {
        comboText.text = "Combo: x" + comboMultiplier.ToString();
    }

    private void UpdateMissesText()
    {
        missesText.text = "Misses Remaining: " + (5 - misses).ToString();
    }

    private void StartPulsing()
    {
        if (pulseCoroutine != null)
        {
            StopCoroutine(pulseCoroutine);
        }
        pulseCoroutine = StartCoroutine(PulseComboText());
    }

    private void StopPulsing()
    {
        if (pulseCoroutine != null)
        {
            StopCoroutine(pulseCoroutine);
            pulseCoroutine = null;
            comboText.transform.localScale = Vector3.one;
        }
    }

    private IEnumerator PulseComboText()
    {
        Vector3 originalScale = Vector3.one;
        while (true)
        {
            float pulseDuration = 0.5f;
            float elapsedTime = 0f;
            Vector3 targetScale = originalScale * Mathf.Min(1.2f + (comboMultiplier - 3) * 0.1f, maxScaleFactor);

            while (elapsedTime < pulseDuration)
            {
                comboText.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / pulseDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < pulseDuration)
            {
                comboText.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / pulseDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    private void StartRainbowEffect()
    {
        if (rainbowCoroutine != null)
        {
            StopCoroutine(rainbowCoroutine);
        }
        rainbowCoroutine = StartCoroutine(RainbowComboText());
    }

    private void StopRainbowEffect()
    {
        if (rainbowCoroutine != null)
        {
            StopCoroutine(rainbowCoroutine);
            rainbowCoroutine = null;
            comboText.color = Color.white;
        }
    }

    private IEnumerator RainbowComboText()
    {
        while (true)
        {
            float duration = 1f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                comboText.color = Color.HSVToRGB(t, 1f, 1f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    private void PlayComboSound()
    {
        if (sfxSource != null && comboSound != null)
        {
            sfxSource.PlayOneShot(comboSound);
        }
    }

    private void CheckMisses()
    {
        if (misses >= 5)
        {
            if (levelManager != null)
            {
                StartCoroutine(ShowResultsAndLoadMainMenu()); // Start coroutine to show results and load main menu
            }
        }
    }

    private IEnumerator FlashMissesText()
    {
        Color originalColor = missesText.color;
        missesText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        missesText.color = originalColor;
    }

    private IEnumerator ShowResultsAndLoadMainMenu()
    {
        resultsScreen.SetActive(true); // Show results screen
        finalScoreText.text = "Final Score: " + score.ToString(); // Update final score text
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        resultsScreen.SetActive(false); // Hide results screen
        levelManager.LoadScene("MainMenuScene"); // Load the main menu
    }
}