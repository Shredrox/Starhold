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
        UIManager.instance.UpdateBaseHealthText(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UIManager.instance.UpdateBaseHealthText(currentHealth, maxHealth);

        if (healthBar != null)
        { 
            healthBar.value = currentHealth; 
        }

        if (currentHealth <= 0)
        {
            GameManager.instance.LoseGame();
        }
    }
}
