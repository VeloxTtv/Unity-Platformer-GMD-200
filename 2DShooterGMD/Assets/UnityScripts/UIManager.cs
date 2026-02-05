using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class UIManager : MonoBehaviour
{
    public GameObject deathScreenUI;

    public void ToggleDeathScreen()
    {
        deathScreenUI.SetActive(true);
        Time.timeScale = 0f; //pauses
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; //resumes
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}