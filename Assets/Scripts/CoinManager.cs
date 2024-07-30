using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int totalCoins;
    private int collectedCoins = 0;
    public GameObject successImage; 

    void Start()
    {
        successImage.SetActive(false);
    }

    public void CollectCoin()
    {
        collectedCoins++;
        if (collectedCoins >= totalCoins)
        {
            successImage.SetActive(true);
        }
    }
}
