using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyController : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.gameObject.CompareTag("Player"))
        {
            hasTriggered = true;
            playerHealth.currentHealth -= 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
