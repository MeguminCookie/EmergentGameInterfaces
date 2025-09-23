using JoyconLib;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class LanceController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public JoyconDemo joyconLeft;
    public JoyconDemo joyconRight;

    public Quaternion joyconRotation = quaternion.identity;
    public GameObject lanceObject;
    void Start()
    {
        
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

    
}
