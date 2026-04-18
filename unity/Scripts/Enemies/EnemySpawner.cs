using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform player;
    [SerializeField] private float initialSpawnInterval = 1.25f;
    [SerializeField] private float minSpawnInterval = 0.2f;
    [SerializeField] private float difficultyRampPerSecond = 0.01f;
    [SerializeField] private float spawnRadius = 10f;

    private float elapsed;
    private float nextSpawnTime;

    private void Update()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.CurrentState != RunState.Running)
        {
            return;
        }

        elapsed += Time.deltaTime;

        if (Time.time < nextSpawnTime || enemyPrefabs.Count == 0 || player == null)
        {
            return;
        }

        SpawnEnemy();

        var interval = Mathf.Max(minSpawnInterval, initialSpawnInterval - elapsed * difficultyRampPerSecond);
        nextSpawnTime = Time.time + interval;
    }

    private void SpawnEnemy()
    {
        var direction = Random.insideUnitCircle.normalized;
        if (direction == Vector2.zero)
        {
            direction = Vector2.up;
        }

        var spawnPosition = (Vector2)player.position + direction * spawnRadius;
        var enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
