using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
    public ParticleSystem speedLines;

    [Header("Start Management")]
    public float timeTillGameStarts;
    public GameObject horseControls;
    public GameObject lanceController;
    public GameObject[] tutorialTextObjects;

    [Header("End Management")]
    private float endTimer;
    public GameObject endOfGameUI;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI timeSurvivedText;
    public TextMeshProUGUI timeTillMainMenuText;

    private bool hasStarted;

    private float totalTimeElapsed;
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
        StartCoroutine(StartOfGame());
        scoreText.gameObject.SetActive(false); 
        speedText.gameObject.SetActive(false);
        horseSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = string.Format("{0:#.00} kph", horseSpeed);

        if (horseSpeed < 15)
        {
            Camera.main.fieldOfView = 60 + horseSpeed * 1.5f;
            speedLines.emissionRate = horseSpeed;
        }
        else
        {
            speedLines.emissionRate = horseSpeed * 2;
            Camera.main.fieldOfView = 60 + 15 * 1.5f;
        }

        totalTimeElapsed += Time.deltaTime;
        timeSpanned += Time.deltaTime;
        if (timeSpanned > timeTillDifficultyRamp)
        {
            timeSpanned = 0;
            speedRequirement = (int)(speedRequirement * speedRequirmentMuliplier);
        }

        
    }

    public void AddScore(int scoreToAdd)
    {

        if (horseSpeed > 5)
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

    private IEnumerator StartOfGame()
    {
        tutorialTextObjects[0].transform.DOMoveY(540, 0.5f);
        yield return new WaitForSeconds(5);
        tutorialTextObjects[0].transform.DOMoveY(-600, 0.5f);
        tutorialTextObjects[1].transform.DOMoveY(540, 0.5f);
        yield return new WaitForSeconds(1);
        tutorialTextObjects[1].transform.DOMoveY(-600, 0.5f);
        tutorialTextObjects[2].transform.DOMoveY(540, 0.5f);
        yield return new WaitForSeconds(1);
        tutorialTextObjects[2].transform.DOMoveY(-600, 0.5f);
        tutorialTextObjects[3].transform.DOMoveY(540, 0.5f);
        yield return new WaitForSeconds(1);
        tutorialTextObjects[3].transform.DOMoveY(-600, 0.5f);
        lanceController.GetComponent<LanceController>().enabled = true;
        horseControls.GetComponent<HorseControls>().enabled = true;
        scoreText.gameObject.SetActive(true);
        speedText.gameObject.SetActive(true);

    }

    public IEnumerator GameOver()
    {
        endOfGameUI.SetActive(true);
        scoreText.gameObject.SetActive(false);
        speedText.gameObject.SetActive(false);
        int scoring = 0;
        while(scoring < score)
        {
            scoring += Mathf.CeilToInt((score - scoring) * 0.2f);
            yield return new WaitForSeconds(0.02f);
            finalScoreText.text = "FINAL SCORE: " + scoring;

        }
        float time = 0;
        while (time < totalTimeElapsed)
        {
            scoring += Mathf.CeilToInt((totalTimeElapsed - time) * 0.2f);
            yield return new WaitForSeconds(0.02f);
            timeSurvivedText.text = "TIME SPENT: " + string.Format("{00:#.00}", time); ;

        }
    }
}

