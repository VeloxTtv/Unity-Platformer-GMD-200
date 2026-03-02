using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealtht", menuName = "Game/Player Data")]


public class PlayerHealth : ScriptableObject
{
    public int currentHealth;
    public int maxHealth = 4;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
}
