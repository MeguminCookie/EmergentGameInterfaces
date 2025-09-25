using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedBarrier : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI speedText;
    private GameManager gameManager;
    private float requiredSpeed;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        requiredSpeed = gameManager.speedRequirement;
        speedText.SetText(requiredSpeed + "kph");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Horse"))
        {
            if(other.GetComponentInParent<HorseControls>().currentSpeed >= requiredSpeed)
            {
                Destroy(this.gameObject);
            }
            else
            {
                SceneManager.LoadScene(0);
                //Game Over
            }
        }
    }
}
