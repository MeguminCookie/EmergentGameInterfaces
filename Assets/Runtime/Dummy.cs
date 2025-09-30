using DG.Tweening;
using JoyconLib;
using System;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int slowDownAmount;
    public JoyconDemo leftJoycon;
    public JoyconDemo rightJoycon;
    public static event Action<int> OnTargetHit;
    void Start()
    {
        leftJoycon = GameObject.Find("Joycon_Left").GetComponent<JoyconDemo>();
        rightJoycon = GameObject.Find("Joycon_Right").GetComponent<JoyconDemo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lance"))
        {
            Debug.Log("Hit dummy");
            rightJoycon.joycon.SetRumble(1000, 1000, 1000, 200);
            OnTargetHit?.Invoke(100);
            Camera.main.DOShakePosition(0.3f, 0.2f,20, 90, true);
            transform.DORotate(new Vector3(80,transform.rotation.y,transform.rotation.z), 0.15f, RotateMode.LocalAxisAdd);
            //Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Horse"))
        {
            Debug.Log("Collided with dummy");
            leftJoycon.joycon.SetRumble(1000, 1000, 1000, 200);
            Camera.main.DOShakePosition(0.3f, 0.8f, 20, 90, true);
            other.GetComponentInParent<HorseControls>().SlowDown(slowDownAmount);
            transform.DORotate(new Vector3(80, transform.rotation.y, transform.rotation.z), 0.15f, RotateMode.LocalAxisAdd);
            //Destroy(this.gameObject);
        }
    }
}
