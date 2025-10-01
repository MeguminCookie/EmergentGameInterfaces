using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JoyconLib;

public class RightTarget : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameManager GameManager;
    public JoyconDemo rightJoycon;
    public bool isHit = false;
    public static event Action<int> OnTargetHit;
    
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rightJoycon = GameObject.Find("Joycon_Right").GetComponent<JoyconDemo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lance") && isHit ==false)
        {
            Debug.Log("Hit Target");
            OnTargetHit?.Invoke(100);
            Destruction destruct = GetComponentInParent<Destruction>();
            if(GameManager.horseSpeed > 10)
            {
                //Destroy actual object
                destruct.DestoryedObject();
                Camera.main.DOShakePosition(0.2f, 0.5f, 20, 90, true);
                rightJoycon.joycon.SetRumble(2, 4, 10, 1);
                Destroy(this.gameObject);
            }
            else
            {
                //Play animation
                isHit = true;
                Vector3 rotation = new Vector3(90, transform.rotation.y, transform.rotation.z);
                rightJoycon.joycon.SetRumble(1000, 1000, 1000, 200);
                Camera.main.DOShakePosition(0.2f, 0.2f, 20, 90, true);
                transform.DORotate(rotation, 0.15f, RotateMode.LocalAxisAdd);
               
            }

        }
    }
}
