using UnityEngine;

public class HealthBarDisplay : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public SpriteRenderer healthBarSprite;
    public Sprite[] healthSprites;

    void Update()
    {
        if (healthSprites.Length > playerHealth.currentHealth && playerHealth.currentHealth >= 0)
        {
            healthBarSprite.sprite = healthSprites[playerHealth.currentHealth];
        }
    }
}