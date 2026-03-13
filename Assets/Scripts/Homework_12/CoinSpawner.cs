using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab;
    public int CoinCount = 3;
    public float SpawnRadius = 10f;
    public float SpawnHeight = 1f;

    private void Start()
    {
        for (int i = 0; i < CoinCount; i++)
            SpawnCoin();
    }

    public void SpawnCoin()
    {
        if (CoinPrefab == null) return;

        Vector3 randomPos = new Vector3(
            Random.Range(-SpawnRadius, SpawnRadius),
            SpawnHeight,
            Random.Range(-SpawnRadius, SpawnRadius)
        );

        Instantiate(CoinPrefab, randomPos, Quaternion.identity);
    }
}