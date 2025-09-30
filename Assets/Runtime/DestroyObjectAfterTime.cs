using System.Collections;
using UnityEngine;

public class DestroyObjectAfterTime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyAfterSeconds());
    }

    // Update is called once per frame
    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
