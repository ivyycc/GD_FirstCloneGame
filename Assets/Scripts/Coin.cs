using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinManager coinManager;

    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coinManager.CollectCoin();
            Destroy(gameObject);
        }
    }
}
