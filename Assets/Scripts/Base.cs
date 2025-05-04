using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private Slider healthBar;

    private void Start()
    {
        healthBar = GameObject.Find("BaseHealthBar").GetComponent<Slider>();
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.maxValue = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Base Health: " + currentHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        Debug.Log("Game Over! Base Destroyed.");
        GameOverUI.instance.ShowGameOver();
    }
}
