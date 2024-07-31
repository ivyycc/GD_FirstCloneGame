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
            AudioManager.instance.PlayOneShot(FMODEvents.instance.collectFloatie, this.transform.position);
            coinManager.CollectCoin();
            Destroy(gameObject);
        }
    }
}
