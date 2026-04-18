using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> normalEnemyPrefabs;
    [SerializeField] private List<GameObject> eliteEnemyPrefabs;
    [SerializeField] private GameObject bossEnemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float initialSpawnInterval = 1.25f;
    [SerializeField] private float minSpawnInterval = 0.2f;
    [SerializeField] private float difficultyRampPerSecond = 0.01f;
    [SerializeField] private float eliteStartTime = 180f;
    [SerializeField] private float eliteSpawnInterval = 25f;
    [SerializeField] private float bossSpawnTime = 600f;
    [SerializeField] private float spawnRadius = 10f;

    private float elapsed;
    private float nextSpawnTime;
    private float nextEliteTime;
    private bool bossSpawned;

    private void Update()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.CurrentState != RunState.Running)
        {
            return;
        }

        elapsed += Time.deltaTime;

        if (elapsed >= bossSpawnTime && !bossSpawned && bossEnemyPrefab != null)
        {
            SpawnSpecificEnemy(bossEnemyPrefab);
            bossSpawned = true;
        }

        if (elapsed >= eliteStartTime && elapsed >= nextEliteTime && eliteEnemyPrefabs.Count > 0)
        {
            SpawnSpecificEnemy(eliteEnemyPrefabs[Random.Range(0, eliteEnemyPrefabs.Count)]);
            nextEliteTime = elapsed + eliteSpawnInterval;
        }

        if (Time.time < nextSpawnTime || normalEnemyPrefabs.Count == 0 || player == null)
        {
            return;
        }

        SpawnSpecificEnemy(normalEnemyPrefabs[Random.Range(0, normalEnemyPrefabs.Count)]);

        var interval = Mathf.Max(minSpawnInterval, initialSpawnInterval - elapsed * difficultyRampPerSecond);
        nextSpawnTime = Time.time + interval;
    }

    private void SpawnSpecificEnemy(GameObject enemyPrefab)
    {
        var direction = Random.insideUnitCircle.normalized;
        if (direction == Vector2.zero)
        {
            direction = Vector2.up;
        }

        var spawnPosition = (Vector2)player.position + direction * spawnRadius;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
