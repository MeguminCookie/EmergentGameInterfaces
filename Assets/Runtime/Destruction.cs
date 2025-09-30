using UnityEngine;

public class Destruction : MonoBehaviour
{
    public GameObject destructedObject;
    public Transform spawnLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void DestoryedObject()
    {
        GameObject destructible = Instantiate(destructedObject, spawnLocation.position, spawnLocation.rotation);
        foreach (Rigidbody rb in destructible.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = transform.forward * 4 + (new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f)));
            rb.AddForce(force * 100);
        }
    }
}
