using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class FPCoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab;
    public int CoinCount = 3;
    public float SpawnRadius = 30f;
    public float SpawnHeight = 1f;
    public float MinDistanceToWall = 0.5f;      
    public float MinDistanceBetweenCoins = 2f;  
    public Vector3 MapCenter = Vector3.zero;    

    private List<Vector3> _spawnedPositions = new List<Vector3>();

    public Transform Map;

    private void Start()
    {
        if (Map != null)
        {
            Renderer[] renderers = Map.GetComponentsInChildren<Renderer>();
            if (renderers.Length > 0)
            {
                Bounds bounds = renderers[0].bounds;
                foreach (Renderer r in renderers)
                    bounds.Encapsulate(r.bounds);

                MapCenter = bounds.center;
                SpawnRadius = Mathf.Min(bounds.extents.x, bounds.extents.z);
                Debug.Log($"Карта: центр={MapCenter}, радиус={SpawnRadius}");
            }
        }

        for (int i = 0; i < CoinCount; i++)
            SpawnCoin();
    }

    public void SpawnCoin()
    {
        if (CoinPrefab == null) return;

        Vector3 spawnPos = GetRandomNavMeshPosition();
        if (spawnPos != Vector3.zero)
        {
            Instantiate(CoinPrefab, spawnPos, Quaternion.identity);
            _spawnedPositions.Add(spawnPos);
        }
        else
        {
            
            NavMeshHit hit;
            Vector3 randomPos = MapCenter + Random.insideUnitSphere * SpawnRadius;
            if (NavMesh.SamplePosition(randomPos, out hit, SpawnRadius, NavMesh.AllAreas))
            {
                Instantiate(CoinPrefab, hit.position + Vector3.up * SpawnHeight, 
                            Quaternion.identity);
                _spawnedPositions.Add(hit.position);
            }
        }
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 randomPos = MapCenter + new Vector3(
                Random.Range(-SpawnRadius, SpawnRadius),
                0f,
                Random.Range(-SpawnRadius, SpawnRadius)
            );

            NavMeshHit hit;
            if (!NavMesh.SamplePosition(randomPos, out hit, 5f, NavMesh.AllAreas))
                continue;

            NavMeshHit wallHit;
            if (NavMesh.FindClosestEdge(hit.position, out wallHit, NavMesh.AllAreas)
                && wallHit.distance < MinDistanceToWall)
                continue;

            bool tooClose = false;
            foreach (Vector3 pos in _spawnedPositions)
            {
                if (Vector3.Distance(hit.position, pos) < MinDistanceBetweenCoins)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
                return hit.position + Vector3.up * SpawnHeight;
        }

        Debug.LogWarning("Не удалось найти точку — используем fallback!");
        return Vector3.zero;
    }

    public void RemoveCoinPosition(Vector3 pos)
    {
        _spawnedPositions.Remove(pos);
    }
}