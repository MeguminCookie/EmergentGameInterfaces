using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuRaycastingµ : MonoBehaviour
{
    public Transform raycastPoint;
    public LayerMask boardLayer;
    public float chargeTime;
    public GameObject player;

    private LineRenderer lineRenderer;

    public RawImage fadingImage;
    public UnityEngine.UI.Image radialCircle;
    public Color fadedOutColor;
    public Color fadedInColor;
    private bool isFading;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = raycastPoint.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        if(isFading == false)
        {
            StartCoroutine(FadeOut());
        }

        // Basic style
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        

    }

    // Update is called once per frame
    void Update()
    {


        RaycastingLance();

        radialCircle.fillAmount = chargeTime;

    }

    private void RaycastingLance()
    {
        
        Debug.DrawRay(raycastPoint.transform.position, raycastPoint.transform.up * 20, Color.red);

        Ray ray = new Ray(raycastPoint.transform.position, raycastPoint.transform.up);
        RaycastHit hit;

        Vector3 endPoint = raycastPoint.transform.position + raycastPoint.transform.up * 20;


        if (Physics.Raycast(ray, out hit, 20, boardLayer))
        {
            endPoint = hit.point;
            if (hit.collider.gameObject.CompareTag("Start") && chargeTime < 1)
            {
                chargeTime += Time.deltaTime;
            }
            else if (hit.collider.gameObject.CompareTag("Start") && chargeTime >= 1)
            {
                chargeTime = 0;
                StartCoroutine(StartGame());
            }
            if (hit.collider.gameObject.CompareTag("Tutorial") && chargeTime < 1)
            {
                chargeTime += Time.deltaTime;
            }
            else if (hit.collider.gameObject.CompareTag("Tutorial") && chargeTime >= 1)
            {
                chargeTime = 0;
                StartCoroutine(Tutorial());
            }
            if (hit.collider.gameObject.CompareTag("Exit") && chargeTime < 1)
            {
                chargeTime += Time.deltaTime;
            }
            else if (hit.collider.gameObject.CompareTag("Exit") && chargeTime >= 1)
            {
                chargeTime = 0;
                StartCoroutine(ExitGame());
            }
            if(hit.collider.gameObject.CompareTag("Back") && chargeTime < 1)
            {
                chargeTime += Time.deltaTime;
            }
            else if (hit.collider.gameObject.CompareTag("Back") && chargeTime >= 1)
            {
                chargeTime = 0;
                StartCoroutine(Backl());
            }

        }
        else
        {
            chargeTime = 0;
        }
        lineRenderer.SetPosition(0, raycastPoint.transform.position);
        lineRenderer.SetPosition(1, endPoint);
    }


    private IEnumerator StartGame()
    {
        if (isFading == false)
        {
            StartCoroutine(FadeIn());
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
    private IEnumerator ExitGame()
    {
        if (isFading == false)
        {
            StartCoroutine(FadeIn());
        }
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
    private IEnumerator Tutorial()
    {
        player.transform.DORotate(new Vector3(110, 0, 0),1, RotateMode.LocalAxisAdd); 
        yield return new WaitForSeconds(1);
    }

    private IEnumerator Backl()
    {
        player.transform.DORotate(new Vector3(-110, 0, 0), 1, RotateMode.LocalAxisAdd);
        yield return new WaitForSeconds(1);
    }



    private IEnumerator FadeIn()
    {
        isFading = true;
        fadingImage.DOColor(fadedInColor, 1f);
        yield return null;
        yield return new WaitForSeconds(1);
        isFading = false;
    }
    private IEnumerator FadeOut()
    {
        isFading = true;
        fadingImage.DOColor(fadedOutColor, 1f);
        yield return new WaitForSeconds(1);
        isFading = false;
    }



}
