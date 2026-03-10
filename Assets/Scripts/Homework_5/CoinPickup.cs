using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CoinCounter counter = other.GetComponent<CoinCounter>();
        
        if (counter != null)
        {
            counter.AddCoin();
            Destroy(gameObject);
        }
    }
}