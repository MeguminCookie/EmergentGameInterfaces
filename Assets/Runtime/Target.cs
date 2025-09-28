using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform[] SpawnLocations;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject wrongTarget;
    private List<GameObject> targetObjects = new List<GameObject>();

    private int random;
    public static event Action<int> OnTargetHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        random = UnityEngine.Random.Range(0,SpawnLocations.Length);
        for(int i = 0; i < SpawnLocations.Length; i++)
        {
            if(random == i)
            {
                GameObject target = Instantiate(targetObject, SpawnLocations[i].position, SpawnLocations[i].transform.rotation);
                targetObjects.Add(target);
            }
            else
            {
                GameObject notTarget = Instantiate(wrongTarget, SpawnLocations[i].position, SpawnLocations[i].transform.rotation);
                targetObjects.Add(notTarget);
            }
        }
        
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
            Destroy(targetObject);
        }
    }

    private void OnDestroy()
    {
        for(int i = 0;i < targetObjects.Count;i++)
        {
            Destroy(targetObjects[i]);
        }
    }
}


