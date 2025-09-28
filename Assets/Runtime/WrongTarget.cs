using UnityEngine;

public class WrongTarget : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            Debug.Log("Hit Target");
            other.GetComponentInParent<HorseControls>().SlowDown(3);
            Destroy(this.gameObject);
        }
    }
}
