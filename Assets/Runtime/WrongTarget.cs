using System.Collections;
using UnityEngine;
using DG.Tweening;

public class WrongTarget : MonoBehaviour
{
    public bool moving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lance"))
        {
            if (moving == false)
            {
                Debug.Log("Hit Target");
                other.GetComponentInParent<HorseControls>().SlowDown(3);
                other.GetComponentInParent<LanceController>().SpawnParticles();
                StartCoroutine(Hit());
                moving = true;
            }

        }
    }

    private IEnumerator Hit()
    {
        transform.DORotate(new Vector3(-40,transform.rotation.y,transform.rotation.z),0.25f, RotateMode.LocalAxisAdd);
        yield return new WaitForSeconds(0.28f);
        transform.DORotate(new Vector3(65, transform.rotation.y, transform.rotation.z), 0.25f, RotateMode.LocalAxisAdd);
        yield return new WaitForSeconds(0.28f);
        transform.DORotate(new Vector3(-25, transform.rotation.y, transform.rotation.z), 0.25f, RotateMode.LocalAxisAdd);


    }
}
