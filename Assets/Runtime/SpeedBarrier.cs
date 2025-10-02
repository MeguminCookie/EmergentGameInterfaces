using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedBarrier : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI speedText;
    public float launchForce;
    private GameManager gameManager;
    private float requiredSpeed;
    public AudioSource hitSound;
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
                hitSound.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
                hitSound.Play();
                StartCoroutine(LaunchAndDestroy(other));
            }
            else
            {
                StartCoroutine(gameManager.GameOver());
                //Game Over
            }
        }
    }


    private IEnumerator LaunchAndDestroy(Collider collision)
    {
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Unfreeze all constraints before launching
                rb.constraints = RigidbodyConstraints.None;

                // Calculate direction away from the player
                Vector3 direction = (child.position - collision.transform.position + new Vector3(0,4,0)).normalized;

                // Apply impulse force
                if(collision.gameObject.GetComponentInParent<HorseControls>().currentSpeed < 15)
                {
                    rb.AddForce(direction * launchForce * collision.gameObject.GetComponentInParent<HorseControls>().currentSpeed, ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(direction * launchForce * 15, ForceMode.Impulse);

                }

                Vector3 randomTorque = new Vector3(
                       Random.Range(-1f, 1f),
                       Random.Range(-1f, 1f),
                       Random.Range(-1f, 1f)
                   ).normalized * Random.Range(0.5f,2f);

                rb.AddTorque(randomTorque, ForceMode.Impulse);
            }
        }
        Camera.main.DOComplete();
        Camera.main.DOShakePosition(0.3f, 0.8f, 20, 90, true);
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
