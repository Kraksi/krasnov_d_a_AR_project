using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class GameStarter : MonoBehaviour
{
    [Header("Player")]
    public GameObject PlayerPrefab;  
    public Transform[] PlayerSpawnPoints;

    [Header("Timer")]
    public float CountdownTime = 3f;
    public TextMeshProUGUI CountdownText;
    public GameObject CountdownPanel;

    private GameObject _spawnedPlayer;

    private void Start()
    {
        SpawnPlayer();

        if (CountdownPanel != null)
            CountdownPanel.SetActive(true);

        StartCoroutine(StartCountdown());
    }

    private void SpawnPlayer()
    {
        Debug.Log("SpawnPlayer вызван");
        
        if (PlayerPrefab == null)
        {
            Debug.LogError("PlayerPrefab не назначен!");
            return;
        }
        
        if (PlayerSpawnPoints == null || PlayerSpawnPoints.Length == 0)
        {
            Debug.LogError("Нет точек спавна!");
            return;
        }

        Transform spawnPoint = PlayerSpawnPoints[
            Random.Range(0, PlayerSpawnPoints.Length)];

        Debug.Log($"Спавним на точке: {spawnPoint.name} позиция: {spawnPoint.position}");

        _spawnedPlayer = Instantiate(PlayerPrefab, 
                                    spawnPoint.position, 
                                    spawnPoint.rotation);

        Debug.Log($"Игрок создан: {_spawnedPlayer.name}");
    }

    private System.Collections.IEnumerator StartCountdown()
    {
        FPEnemyAI[] enemies = FindObjectsOfType<FPEnemyAI>();
        foreach (FPEnemyAI enemy in enemies)
            enemy.enabled = false;

        float timeLeft = CountdownTime;
        while (timeLeft > 0)
        {
            if (CountdownText != null)
            {
                if (timeLeft > 1f)
                    CountdownText.text = Mathf.CeilToInt(timeLeft).ToString();
                else
                    CountdownText.text = "GO!";
            }

            timeLeft -= Time.deltaTime;
            yield return null;
        }

        StartGame();
    }

    private void StartGame()
    {
        if (CountdownPanel != null)
            CountdownPanel.SetActive(false);

        if (_spawnedPlayer != null)
        {
            FPPlayerController controller = 
                _spawnedPlayer.GetComponent<FPPlayerController>();
            if (controller != null)
                controller.enabled = true;
        }

        FPEnemyAI[] enemies = FindObjectsOfType<FPEnemyAI>();
        foreach (FPEnemyAI enemy in enemies)
        {
            enemy.enabled = true;
            if (_spawnedPlayer != null)
                enemy.Player = _spawnedPlayer.transform;
        }

        Debug.Log("Игра началась!");
    }
}