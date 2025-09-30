using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RightTarget : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameManager GameManager;
    public static event Action<int> OnTargetHit;
    
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
            Destruction destruct = GetComponentInParent<Destruction>();
            if(GameManager.horseSpeed > 10)
            {
                //Destroy actual object
                destruct.DestoryedObject();
                Destroy(this.gameObject);
            }
            else
            {
                //Play animation
                Vector3 rotation = new Vector3(-90, transform.rotation.y, transform.rotation.z);
                transform.DORotate(rotation, 0.15f, RotateMode.LocalAxisAdd);
               
            }

        }
    }
}
