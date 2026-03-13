using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public AudioClip PickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerClick>() != null)
        {
            GameManager.Instance.AddScore(1);

            if (PickupSound != null)
                AudioSource.PlayClipAtPoint(PickupSound, transform.position);

            CoinSpawner spawner = FindObjectOfType<CoinSpawner>();
            if (spawner != null)
                spawner.SpawnCoin();

            Destroy(gameObject);
        }
    }
}