using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enemyController enemyPrefab;
    public Transform[] spawnPoints; 
    public float spawnInterval = 20f;

    private Coroutine _spawnEnemiesRoutine;

    //Modified code from class

    // Start is called before the first frame update
    void Start()
    {
        _spawnEnemiesRoutine = StartCoroutine(Co_SpawnEnemies());
    }

   IEnumerator Co_SpawnEnemies()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            // Pick a random spawn point
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform randomSpawnPoint = spawnPoints[randomIndex];

            // Instantiate the enemy at the selected point's position and rotation
            Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            // Wait for the specified interval before the next spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}