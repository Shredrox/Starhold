using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject gameOverText;

    public GameObject winText;

    public TMP_Text waveText;

    public TMP_Text baseHealthText;

    private void Awake()
    {
        instance = this;
    }

    public void ShowGameOver()
    {
        gameOverText.SetActive(true);
    }

    public void ShowWin()
    {
        winText.SetActive(true);
    }

    public void UpdateWaveText(int totalWaves, int currentWave)
    {
        int wavesRemaining = Mathf.Max(0, totalWaves - currentWave);
        if (waveText != null)
        { 
            waveText.SetText($"Waves Remaining: {wavesRemaining}"); 
        }
    }

    public void UpdateBaseHealthText(float currentHealth, float maxHealth)
    {
        if (baseHealthText != null && currentHealth >= 0)
        {
            baseHealthText.SetText($"Base Health: {currentHealth}/{maxHealth}");
        }
    }
}
