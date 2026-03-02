using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController thePlayer = collision.gameObject.GetComponent<PlayerController>();
        if (thePlayer != null)
        {
            FindFirstObjectByType<LevelEnd>().ShowEndMenu();
        }
    }
}
