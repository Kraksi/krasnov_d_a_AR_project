using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public AudioClip PickupSound;
    public float Volume = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null || 
            other.GetComponent<PlayerClick>() != null)
        {

            if (PickupSound != null)
                AudioSource.PlayClipAtPoint(PickupSound, transform.position, Volume);

            Debug.Log("Монетка собрана!");
            Destroy(gameObject);
        }
    }
}