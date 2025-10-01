using JoyconLib;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class LanceController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public JoyconDemo joyconLeft;
    public JoyconDemo joyconRight;

    public Transform hitParticleSpawnLocation;
    public GameObject hitParticles;

    public Quaternion joyconRotation = quaternion.identity;
    public GameObject lanceObject;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        joyconLeft.GetComponent<JoyconDemo>().joycon.Recenter();
        joyconRight.GetComponent<JoyconDemo>().joycon.Recenter();
    }

    // Update is called once per frame
    void Update()
    {
        LanceRotationR();
    }

    private void LanceRotationR()
    {
        joyconRotation= joyconRight.joycon.GetOrientation();
        lanceObject.transform.localRotation = joyconRotation;
    }

    public void SpawnParticles()
    {
       GameObject particle = Instantiate(hitParticles, hitParticleSpawnLocation.transform.position, hitParticleSpawnLocation.transform.rotation);
        particle.SetActive(true);
    }

    
}
