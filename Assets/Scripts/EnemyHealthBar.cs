using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Enemy enemy;

    private void Update()
    {
        slider.value = enemy != null ? enemy.currentHealth : 0;
    }
}
