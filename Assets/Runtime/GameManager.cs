using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [Header("Ramping Difficulty Numbers")]
    public int speedRequirement;
    public float speedRequirmentMuliplier;
    public float timeTillDifficultyRamp;

    [Header("Score stuff")]
    public int score;
    public int displayedScore;
    public TextMeshProUGUI scoreText;

    [Header("UI Stuff")]
    [SerializeField] private TextMeshProUGUI speedText;

    private float timeSpanned;
    public float horseSpeed = 2;

    private void OnEnable()
    {
        RightTarget.OnTargetHit += AddScore;
        Dummy.OnTargetHit += AddScore;
    }

    private void OnDisable()
    {
        RightTarget.OnTargetHit -= AddScore;
        Dummy.OnTargetHit -= AddScore;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = string.Format("{0:#.00} kph",horseSpeed);

        timeSpanned += Time.deltaTime;
        if(timeSpanned > timeTillDifficultyRamp)
        {
            timeSpanned = 0;
            speedRequirement = (int)(speedRequirement * speedRequirmentMuliplier);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        
        if(horseSpeed > 5)
        {
            score += (int)(scoreToAdd * (horseSpeed / 3));
        }
        else
        {
            score += scoreToAdd;
        }
            StopAllCoroutines(); // stop any previous animation
        StartCoroutine(AnimateScore());
    }

    private IEnumerator AnimateScore()
    {
        while (displayedScore < score)
        {
            displayedScore += Mathf.CeilToInt((score - displayedScore) * 0.2f);
            if (displayedScore > score)
                displayedScore = score;

            UpdateScoreText();
            yield return new WaitForSeconds(0.05f); // controls speed
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + displayedScore.ToString();
    }
}

