using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuRaycastingµ : MonoBehaviour
{
    public Transform raycastPoint;
    public LayerMask boardLayer;
    public float chargeTime;

    private LineRenderer lineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = raycastPoint.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

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


        // Draw ray in Scene view for debugging
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
            else if(hit.collider.gameObject.CompareTag("Start") && chargeTime >= 1)
            {
                StartCoroutine(StartGame());
            }
            if (hit.collider.gameObject.CompareTag("Tutorial") && chargeTime < 1)
            {
                chargeTime += Time.deltaTime;
            }
            else if (hit.collider.gameObject.CompareTag("Tutorial") && chargeTime >= 1)
            {
                StartCoroutine(Tutorial());
            }
            if (hit.collider.gameObject.CompareTag("Exit") && chargeTime < 1)
            {
                chargeTime += Time.deltaTime;
            }
            else if (hit.collider.gameObject.CompareTag("Exit") && chargeTime >= 1)
            {
                StartCoroutine(ExitGame());
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
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
    private IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
    private IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(1);
    }







}
