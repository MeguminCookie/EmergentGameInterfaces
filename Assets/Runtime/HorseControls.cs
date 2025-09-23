using JoyconLib;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class HorseControls : MonoBehaviour
{

    public JoyconDemo joyconLeft;
    public float baseSpeed;
    private float currentSpeed;
    public float speedUpAmount;
    public float timeTillSpeedDown;



    private bool isAccelerating;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HorseSpeed();
        this.gameObject.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z + currentSpeed * Time.deltaTime);
    }

    private void HorseSpeed()
    {
        if (joyconLeft.joycon.GetAccelerationWorldWithoutGravity().y > 6.5f && isAccelerating == false)
        {
            isAccelerating = true;
            Debug.Log("Speed up");
            Debug.Log(joyconLeft.joycon.GetAccelerationWorldWithoutGravity().y);
            StartCoroutine(Accelaration());
        }
    }
    
    private IEnumerator Accelaration()
    {
        
        float speedGoal = currentSpeed + speedUpAmount;
        while (currentSpeed > speedGoal)
        {
            currentSpeed += Time.deltaTime / 0.05f;
        }
        yield return new WaitForSeconds(0.5f);
        isAccelerating = false;
    }


}
