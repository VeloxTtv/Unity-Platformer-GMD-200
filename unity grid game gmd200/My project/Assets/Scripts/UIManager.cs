using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject winScreenUI;
    public GameObject deathScreenUI;

    public void ToggleWinScreen()
    {
        winScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ToggleDeathScreen()
    {
        deathScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0);
    }
}