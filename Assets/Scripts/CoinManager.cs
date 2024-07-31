using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this line for scene management
using System.Collections.Generic;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public int totalCoins;
    private int collectedCoins = 0;
    public List<GameObject> successImages; // List to hold multiple UI images
    public string nextSceneName; // Name of the next scene to load
    public float delayBeforeLoading = 3f; // Delay in seconds before loading the next scene

    void Start()
    {
        // Deactivate all success images at the start
        foreach (GameObject successImage in successImages)
        {
            successImage.SetActive(false);
        }
    }

    public void CollectCoin()
    {
        if (collectedCoins < totalCoins && collectedCoins < successImages.Count)
        {
            successImages[collectedCoins].SetActive(true); // Activate the corresponding success image
            collectedCoins++;
        }

        // Load the next scene when all coins are collected
        if (collectedCoins >= totalCoins)
        {
            Debug.Log("All coins collected!");
            StartCoroutine(LoadNextSceneWithDelay()); // Start coroutine to load the next scene with a delay
        }
    }

    private IEnumerator LoadNextSceneWithDelay()
    {
        // Wait for the specified delay before loading the next scene
        yield return new WaitForSeconds(delayBeforeLoading);

        // Load the scene specified by the nextSceneName variable
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set.");
        }
    }
}
