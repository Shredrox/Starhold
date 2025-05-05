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
        { 
            healthBar.maxValue = maxHealth; 
        }
        GameUI.instance.UpdateBaseHealthText(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        GameUI.instance.UpdateBaseHealthText(currentHealth, maxHealth);

        if (healthBar != null)
        { 
            healthBar.value = currentHealth; 
        }

        if (currentHealth <= 0)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        GameUI.instance.ShowGameOver();
    }
}
