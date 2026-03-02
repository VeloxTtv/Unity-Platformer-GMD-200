using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public GameObject endPanel;
    public GameObject controlMenu1, controlMenu2;

    public void ShowEndMenu()
    {
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NextLevel()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResumeLevel()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(4);
    }

    public void FirstLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void SecondLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void ThirdLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }


    public void ShowControls()
    {
        controlMenu1.SetActive(true);
        controlMenu2.SetActive(true);
    }

    public void CloseWindow1()
    {
        controlMenu1.SetActive(false);
    }

    public void CloseWindow2()
    {
        controlMenu2.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}