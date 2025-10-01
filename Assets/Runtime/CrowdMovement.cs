using UnityEngine;
using UnityEngine.UIElements;

public class CrowdMovement : MonoBehaviour
{
    [SerializeField] private float minAmplitude = 0.2f;
    [SerializeField] private float maxAmplitude = 0.6f;

    [SerializeField] private float minFrequency = 0.5f;
    [SerializeField] private float maxFrequency = 1.5f;

    private Vector3 startPos;
    private float amplitude;
    private float frequency;
    private float offset;

    void Start()
    {
        startPos = transform.position;

        // Give each object a random amplitude and frequency
        amplitude = Random.Range(minAmplitude, maxAmplitude);
        frequency = Random.Range(minFrequency, maxFrequency);

        // Add a random phase offset so they don’t sync
        offset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency + offset) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
