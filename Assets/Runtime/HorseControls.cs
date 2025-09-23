using JoyconLib;
using UnityEngine;

public class HorseControls : MonoBehaviour
{

    public JoyconDemo joyconLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorseSpeed();
    }

    private void HorseSpeed()
    {
        if (joyconLeft.joycon.GetAccelerationWorldWithoutGravity().y > 6.5f)
        {
            Debug.Log("Speed up");
            Debug.Log(joyconLeft.joycon.GetAccelerationWorldWithoutGravity().y);
        }
    }
}
