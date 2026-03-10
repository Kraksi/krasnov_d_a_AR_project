using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Столкновение с: " + other.gameObject.name);
        
        if (other.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Монетка собрана!");
            Destroy(gameObject);
        }
    }
}