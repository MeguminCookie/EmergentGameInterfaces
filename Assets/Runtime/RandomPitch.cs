using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioSource;
    void Start()
    {
        audioSource.pitch = Random.Range(0.4f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
