using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float MaxHealth = 100f;
    public float CurrentHealth;

    [Header("UI")]
    public Slider HealthBar;
    public TextMeshProUGUI HealthText;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        UpdateHealthUI();

        if (CurrentHealth <= 0)
            Debug.Log("Игрок умер!");
    }

    private void UpdateHealthUI()
    {
        if (HealthBar != null)
            HealthBar.value = CurrentHealth;

        if (HealthText != null)
            HealthText.text = $"{CurrentHealth} HP";
    }
}