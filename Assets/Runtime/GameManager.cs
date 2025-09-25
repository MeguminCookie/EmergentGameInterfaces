using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Ramping Difficulty Numbers")]
    public int speedRequirement;
    public float speedRequirmentMuliplier;
    public float timeTillDifficultyRamp;

    [Header("UI Stuff")]
    [SerializeField] private TextMeshProUGUI speedText;

    private float timeSpanned;
    public float horseSpeed = 2;
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
}
