using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{
    public AudioSource audioSource;
    public static AudioScript instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == "TabMenu" && SceneManager.GetActiveScene().buildIndex != 0)
        {
            audioSource.UnPause();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu Level" || scene.name == "TabMenu")
        {
            UnityEngine.Debug.Log("unpause");
            audioSource.Pause();
        }
        else if (!audioSource.isPlaying)
        {
            UnityEngine.Debug.Log("unpause");
            audioSource.Play();
        }
    }
}
