using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Enemy enemy;
    public Image healthFill;

    private void Update()
    {
        if (enemy == null) return;

        float fillAmount = enemy.currentHealth / enemy.maxHealth;
        Debug.Log("Health Fill Amount: " + fillAmount);
        healthFill.fillAmount = fillAmount;

        transform.rotation = Camera.main.transform.rotation;
    }
}
