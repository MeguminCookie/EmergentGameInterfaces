using System;
using System.Collections.Generic;
using UnityEngine;

public class RightTarget : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static event Action<int> OnTargetHit;
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
            OnTargetHit?.Invoke(100);
            Destroy(this.gameObject);
        }
    }
}
