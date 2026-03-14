using UnityEngine;

public class FPCoinPickup : MonoBehaviour
{
    public AudioClip PickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FPPlayerController>() != null)
        {
            if (PickupSound != null)
                AudioSource.PlayClipAtPoint(PickupSound, transform.position);

            if (FPGameManager.Instance != null)
                FPGameManager.Instance.AddScore(1);
            else
                Debug.LogError("FPGameManager не найден!");

            FPCoinSpawner spawner = FindObjectOfType<FPCoinSpawner>();
            if (spawner != null)
                spawner.SpawnCoin();

            Destroy(gameObject);
        }
    }
}