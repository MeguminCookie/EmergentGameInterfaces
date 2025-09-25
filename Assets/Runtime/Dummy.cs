using UnityEngine;

public class Dummy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int SlowDownAmount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lance"))
        {
            Debug.Log("Hit dummy");
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Horse"))
        {
            Debug.Log("Collided with dummy");
            other.GetComponentInParent<HorseControls>().SlowDown(SlowDownAmount);
            Destroy(this.gameObject);
        }
    }
}
