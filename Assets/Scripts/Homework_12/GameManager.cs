using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI RecordText;
    public GameObject GameOverPanel;
    public TextMeshProUGUI GameOverScoreText;
    public TextMeshProUGUI GameOverRecordText;

    private int _score = 0;
    private int _record = 0;

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