using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    public GameObject effectPrefab1;
    public void SpawnEffectAtLocation1(Vector2 spawnPosition)
    {
        Instantiate(effectPrefab1, spawnPosition, Quaternion.identity);
    }
}
