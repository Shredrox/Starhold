using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;

    public GameObject gameOverText;

    private void Awake()
    {
        instance = this;
    }

    public void ShowGameOver()
    {
        gameOverText.SetActive(true);
    }
}
