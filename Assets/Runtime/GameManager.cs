using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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
    public GameObject ragdolParent;
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
        endOfGameUI.SetActive(false);
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
        ragdolParent.gameObject.SetActive(true);
        ragdolParent.transform.position = Camera.main.transform.position;
        ragdolParent.transform.rotation = Camera.main.transform.rotation;
        Camera.main.transform.parent = ragdolParent.transform;
        ragdolParent.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward *40 * horseSpeed , ForceMode.Impulse);


        yield return new WaitForSeconds(1);
        endOfGameUI.SetActive(true);
        scoreText.gameObject.SetActive(false);
        speedText.gameObject.SetActive(false);
        while(endOfGameUI.gameObject.GetComponent<CanvasScaler>().scaleFactor <1)
        {
            endOfGameUI.gameObject.GetComponent<CanvasScaler>().scaleFactor += Time.deltaTime;
            yield return null;
        }
        
        yield return new WaitForSeconds(0.5f);
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
            time += Mathf.Ceil((totalTimeElapsed - time) * 0.2f);
            
            yield return new WaitForSeconds(0.05f);
            timeSurvivedText.text = "TIME SPENT: " + string.Format("{00:#.00}", time); ;

        }
       

        float endTimer = 0;
        int i = 0;
        while(endTimer < 10)
        {
            endTimer += Time.deltaTime;
            i=  10-(int)endTimer;
            timeTillMainMenuText.text = "HEADING TO MAIN MENU IN: " + i;
            yield return null;
        }
        if(i <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }


}

