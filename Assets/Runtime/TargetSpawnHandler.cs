using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetSpawnHandler : MonoBehaviour
{
    [Header("Spawn Location Line")]
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    public float distanceBetweenSpawnsMin;
    public float distanceBetweenSpawnsMax;
    public float ySpawnRotation;

    [Header("Spawn Details High")]
    public float spawnDistanceHigh;

    [Header("Spawn Details Ground")]
    public float spawnDistanceGroundMin;
    public float spawnDistanceGroundMax;

    [Header("Spawning Speed Barriers")]
    public GameObject barrierPrefab;
    public float yBarrierRotation;
    public Transform spawnBarrierLocation;


    [Header("Prefabs")]
    [SerializeField] private GameObject[] spawnPrefabsHigh;
    [SerializeField] private GameObject[] spawnPrefabsGround;

    private List<GameObject> spawnedObjectsList = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnPrefabs();
        }
    }

    [ContextMenu("SpawnPrefabs")]
    void SpawnPrefabs()
    {
        if (endPos == null || startPos == null)
        {
            return;
        }

        while(spawnedObjectsList.Count > 0)
        {
            Destroy(spawnedObjectsList[0]);
            spawnedObjectsList.RemoveAt(0);
        }

        Vector3 direction = (startPos.position - endPos.position).normalized;
        Vector3 lastSpawnedLocation = Vector3.zero;
        float distanceFromStart = 0F;
        float totalDistance =Vector3.Distance(startPos.position,endPos.position);

        while (distanceFromStart <= totalDistance + distanceBetweenSpawnsMax)
        {

            int randomHighOrLow = Random.Range(1, 3);
            float randomDistance = Random.Range(distanceBetweenSpawnsMin, distanceBetweenSpawnsMax);
            if(lastSpawnedLocation == Vector3.zero)
            {
                Vector3 spawnLocation = startPos.position + direction * randomDistance;
                lastSpawnedLocation = spawnLocation;
            }
            else
            {
                Vector3 spawnLocation = lastSpawnedLocation + direction * randomDistance;
                lastSpawnedLocation = spawnLocation;
            }
            

            if(randomHighOrLow == 1)
            {
                //High Spawn
                int random = Random.Range(0, spawnPrefabsHigh.Length);
                GameObject targetObject = Instantiate(spawnPrefabsHigh[random], lastSpawnedLocation + new Vector3(spawnDistanceHigh,0,0),Quaternion.Euler(0,ySpawnRotation,0));
                spawnedObjectsList.Add(targetObject);
            }
            else
            {
                //Low Spawn
                GameObject dummyObject = Instantiate(spawnPrefabsGround[0], lastSpawnedLocation + new Vector3(Random.Range(spawnDistanceGroundMin, spawnDistanceGroundMax),-0.05f,0), Quaternion.Euler(0, ySpawnRotation, 0));
                spawnedObjectsList.Add(dummyObject);
            }

            distanceFromStart = Vector3.Distance(startPos.position, lastSpawnedLocation);

        }
        GameObject barrierObject = Instantiate(barrierPrefab, spawnBarrierLocation.position, Quaternion.Euler(0,yBarrierRotation,0));
        spawnedObjectsList.Add(barrierObject);  

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lance"))
        {
            SpawnPrefabs();
        }
    }

}
