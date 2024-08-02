using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FullRotatingObstacle : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public string targetTag = "Player";

    void Update()
    {
        RotateRectangle();
    }

    void RotateRectangle()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        if (other.CompareTag(targetTag) && playerMovement != null && !playerMovement.isJumping)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerHit, this.transform.position);
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Debug.Log("DEAD");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}


