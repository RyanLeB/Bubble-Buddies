using UnityEngine;
using TMPro;
using System.Collections;

public class ClickerScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public AudioSource sfxSource;
    public AudioClip comboSound;
    private int score;
    private int comboMultiplier;
    private Coroutine pulseCoroutine;
    private Coroutine rainbowCoroutine;
    public float maxScaleFactor = 1.5f; // Maximum scale factor for pulsing

    void Start()
    {
        score = 0;
        comboMultiplier = 1;
        UpdateScoreText();
        UpdateComboText();
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
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void UpdateComboText()
    {
        comboText.text = "x" + comboMultiplier.ToString();
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
}