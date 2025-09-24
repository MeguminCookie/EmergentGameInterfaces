using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("UI Stuff")]
    [SerializeField] private TextMeshProUGUI speedText;

    [Header("Horse Details")]
    public float horseSpeed = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = string.Format("{0:#.00} kph",horseSpeed);
    }
}
