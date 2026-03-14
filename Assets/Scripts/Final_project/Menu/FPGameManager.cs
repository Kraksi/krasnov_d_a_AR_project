using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FPGameManager : MonoBehaviour
{
    public static FPGameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI RecordText;
    public GameObject GameOverPanel;
    public TextMeshProUGUI GameOverScoreText;
    public TextMeshProUGUI GameOverRecordText;

    [Header("Difficulty")]
    public GameObject EnemyPrefab;
    public Transform[] EnemySpawnPoints;
    public float EnemySpeedIncrement = 0.5f;
    public int CoinsPerSpeedUp = 5;
    public int CoinsPerNewEnemy = 20;
    public Transform Player;

    [Header("Initial Enemies")]
    public int InitialEnemyCount = 3;

    private int _score = 0;
    private int _record = 0;
    private float _currentEnemySpeed = 3.5f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _record = PlayerPrefs.GetInt("Record", 0);
        UpdateUI();

        if (GameOverPanel != null)
            GameOverPanel.SetActive(false);

        SpawnInitialEnemies();
    }

    public void AddScore(int amount)
    {
        _score += amount;

        if (_score > _record)
        {
            _record = _score;
            PlayerPrefs.SetInt("Record", _record);
            PlayerPrefs.Save();
        }

        UpdateUI();
        CheckDifficulty();
    }

    private void CheckDifficulty()
    {
        if (_score % CoinsPerSpeedUp == 0)
        {
            _currentEnemySpeed += EnemySpeedIncrement;
            UpdateEnemySpeed();
            Debug.Log($"Враги ускорились! Скорость: {_currentEnemySpeed}");
        }

        if (_score % CoinsPerNewEnemy == 0)
        {
            SpawnNewEnemy();
            Debug.Log($"Новый враг появился!");
        }
    }

    private void UpdateEnemySpeed()
    {
        FPEnemyAI[] enemies = FindObjectsOfType<FPEnemyAI>();
        foreach (FPEnemyAI enemy in enemies)
            enemy.SetSpeed(_currentEnemySpeed);
    }

    private void SpawnInitialEnemies()
    {
        if (EnemyPrefab == null) return;

        if (EnemySpawnPoints != null && EnemySpawnPoints.Length >= InitialEnemyCount)
        {
            Transform[] shuffled = (Transform[])EnemySpawnPoints.Clone();
            for (int i = shuffled.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                Transform temp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = temp;
            }

            for (int i = 0; i < InitialEnemyCount; i++)
                SpawnEnemyAtPosition(shuffled[i].position);
        }
        else
        {
            List<Vector3> usedPositions = new List<Vector3>();

            for (int i = 0; i < InitialEnemyCount; i++)
            {
                Vector3 spawnPos = GetUniqueSpawnPosition(usedPositions);
                usedPositions.Add(spawnPos);
                SpawnEnemyAtPosition(spawnPos);
            }
        }
    }

    private void SpawnNewEnemy()
    {
        if (EnemyPrefab == null) return;

        Vector3 spawnPos = Vector3.zero;

        if (EnemySpawnPoints != null && EnemySpawnPoints.Length > 0)
        {
            spawnPos = EnemySpawnPoints[
                Random.Range(0, EnemySpawnPoints.Length)].position;
        }
        else
        {
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(
                Random.insideUnitSphere * 10f, out hit, 10f,
                UnityEngine.AI.NavMesh.AllAreas))
                spawnPos = hit.position;
        }

        SpawnEnemyAtPosition(spawnPos);
    }

    private Vector3 GetUniqueSpawnPosition(List<Vector3> usedPositions)
    {
        for (int attempt = 0; attempt < 30; attempt++)
        {
            Vector3 randomPos = Random.insideUnitSphere * 10f;
            UnityEngine.AI.NavMeshHit hit;

            if (UnityEngine.AI.NavMesh.SamplePosition(randomPos, out hit, 10f,
                UnityEngine.AI.NavMesh.AllAreas))
            {
                bool tooClose = false;
                foreach (Vector3 used in usedPositions)
                {
                    if (Vector3.Distance(hit.position, used) < 3f)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                    return hit.position;
            }
        }

        Debug.LogWarning("Не удалось найти уникальную точку спавна!");
        return Vector3.zero;
    }

    private void SpawnEnemyAtPosition(Vector3 position)
    {
        GameObject newEnemy = Instantiate(EnemyPrefab, position, Quaternion.identity);
        FPEnemyAI ai = newEnemy.GetComponent<FPEnemyAI>();
        if (ai != null)
        {
            ai.Player = Player;
            ai.SetSpeed(_currentEnemySpeed);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);

            if (GameOverScoreText != null)
                GameOverScoreText.text = $"Счёт: {_score}";

            if (GameOverRecordText != null)
                GameOverRecordText.text = $"Рекорд: {_record}";
        }

        Debug.Log($"Game Over! Счёт: {_score}, Рекорд: {_record}");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        _score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateUI()
    {
        if (ScoreText != null)
            ScoreText.text = $"Очки: {_score}";

        if (RecordText != null)
            RecordText.text = $"Рекорд: {_record}";
    }
}