using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HalfRotatingObstacle : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public float rotationAngle = 180f; 
    public string targetTag = "Player"; 

    private float currentRotation = 0f; 
    private bool rotatingForward = true; 

    void Start()
    {
        StartCoroutine(RotateRectangle());
    }

    IEnumerator RotateRectangle()
    {
        while (true)
        {
            if (rotatingForward)
            {
                float rotationStep = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.forward, rotationStep);
                currentRotation += rotationStep;

                if (currentRotation >= rotationAngle)
                {
                    currentRotation = rotationAngle;
                    rotatingForward = false;
                    yield return new WaitForSeconds(1f); 
                }
            }
            else
            {
                float rotationStep = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.forward, -rotationStep);
                currentRotation -= rotationStep;
                
                if (currentRotation <= 0f)
                {
                    currentRotation = 0f;
                    rotatingForward = true;
                    yield return new WaitForSeconds(1f);
                }
            }

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        if (other.CompareTag(targetTag) && playerMovement != null && !playerMovement.isJumping)
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Debug.Log("DEAD");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


