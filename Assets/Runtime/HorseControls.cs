using JoyconLib;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class HorseControls : MonoBehaviour
{
    [Header("Horse Movement Variables")]
    public JoyconDemo joyconLeft;
    public float baseSpeed;
    public float currentSpeed;
    public float speedUpAmount;
    public float timeTillSpeedDown;
    public float accelerationRate;
    private float timeSinceLastSpedUp;


    private SplineAnimate splineAnimate;
    private GameManager gameManager;

    private bool isAccelerating;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //Speed horse up till base speed
        currentSpeed = 0;
        splineAnimate.MaxSpeed = 0;
        StartCoroutine(StartOfGame());


    }

    // Update is called once per frame
    void Update()
    {
        HorseSpeed();
        gameManager.horseSpeed = currentSpeed;
        timeSinceLastSpedUp += Time.deltaTime;
        if(timeSinceLastSpedUp > timeTillSpeedDown && currentSpeed > baseSpeed)
        {
            Debug.Log("Speeding down");
            float prevProgress = splineAnimate.NormalizedTime;
            currentSpeed -= (accelerationRate *0.25f) * Time.deltaTime;
            splineAnimate.MaxSpeed = currentSpeed;
            splineAnimate.NormalizedTime = prevProgress;
        }
    }

    private void HorseSpeed()
    {
        if (joyconLeft.joycon.GetAccelerationWorldWithoutGravity().y > 6.5f && isAccelerating == false)
        {
            isAccelerating = true;
            Debug.Log("Speed up");
            timeSinceLastSpedUp = 0;
            Debug.Log(joyconLeft.joycon.GetAccelerationWorldWithoutGravity().y);
            StartCoroutine(Accelaration());
        }
    }
    
    private IEnumerator Accelaration()
    {
       
        float speedGoal = currentSpeed + speedUpAmount;
        while (currentSpeed < speedGoal)
        {
            //Normalize the speed change with the current position of player ( if we don't do this, the player will teleport )
            float prevProgress = splineAnimate.NormalizedTime;
            currentSpeed += accelerationRate * Time.deltaTime;
            splineAnimate.MaxSpeed = currentSpeed;
            splineAnimate.NormalizedTime = prevProgress;
            yield return null;
        }
        Debug.Log("Current Speed: " + currentSpeed);
        isAccelerating = false;
        yield return null;
    }

    private IEnumerator StartOfGame()
    {
        while (splineAnimate.MaxSpeed < baseSpeed)
        {
            float prevProgress = splineAnimate.NormalizedTime;
            currentSpeed += accelerationRate * Time.deltaTime;
            splineAnimate.MaxSpeed = currentSpeed;
            splineAnimate.NormalizedTime = prevProgress;
            yield return null;
        }
    }

    public void SlowDown(float speed)
    {
        StartCoroutine(SlowingDown(speed));
    }

    private IEnumerator SlowingDown(float speed)
    {
        float speedGoal = currentSpeed - speed;
        while (currentSpeed > speedGoal && isAccelerating == false)
        {
            //Normalize the speed change with the current position of player ( if we don't do this, the player will teleport )
            float prevProgress = splineAnimate.NormalizedTime;
            currentSpeed -= accelerationRate * 2 * Time.deltaTime;
            splineAnimate.MaxSpeed = currentSpeed;
            splineAnimate.NormalizedTime = prevProgress;
            yield return null;
        }
        Debug.Log("Current Speed: " + currentSpeed);
        yield return null;
    }
   

}
