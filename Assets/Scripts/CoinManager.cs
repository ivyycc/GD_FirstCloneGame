using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using System.Collections.Generic;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public int totalCoins;
    private int collectedCoins = 0;
    public List<GameObject> successImages; 
    public string nextSceneName; 
    public float delayBeforeLoading = 3f; 

    void Start()
    {
        
        foreach (GameObject successImage in successImages)
        {
            successImage.SetActive(false);
        }
    }

    public void CollectCoin()
    {
        if (collectedCoins < totalCoins && collectedCoins < successImages.Count)
        {
            successImages[collectedCoins].SetActive(true); 
            collectedCoins++;
        }

        if (collectedCoins >= totalCoins)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.levelUp, this.transform.position);
            Debug.Log("All coins collected!");
            StartCoroutine(LoadNextSceneWithDelay()); 
        }
    }

    private IEnumerator LoadNextSceneWithDelay()
    {
        
        yield return new WaitForSeconds(delayBeforeLoading);

        
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
