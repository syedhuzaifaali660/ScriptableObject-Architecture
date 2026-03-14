using ScriptableObjectArchitecture;
using UnityEngine;

/// <summary>
/// Spawns enemies on a timer. Uses EnemyCollection to cap live enemy count.
/// Stops spawning when OnPlayerDied fires.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("SO References")]
    [SerializeField] private GameObjectCollection _enemyCollection;
    [SerializeField] private GameEvent _onPlayerDied;

    [Header("Spawn Settings")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private int _maxEnemies = 8;
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private float _minSpawnDistance = 4f;

    [Header("References")]
    [SerializeField] private Transform _player;

    private float _nextSpawnTime;
    private bool _stopped;

    private void OnEnable() => _onPlayerDied.AddListener(StopSpawning);
    private void OnDisable() => _onPlayerDied.RemoveListener(StopSpawning);

    private void Start()
    {
        if (_player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) _player = p.transform;
        }

        _nextSpawnTime = Time.time + 1f;
    }

    private void Update()
    {
        if (_stopped) return;
        if (Time.time < _nextSpawnTime) return;
        if (_enemyCollection != null && _enemyCollection.Count >= _maxEnemies) return;

        _nextSpawnTime = Time.time + _spawnInterval;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (_enemyPrefab == null) return;
        Instantiate(_enemyPrefab, GetSpawnPosition(), Quaternion.identity);
    }

    private Vector3 GetSpawnPosition()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 rand = Random.insideUnitCircle * _spawnRadius;
            Vector3 pos = new Vector3(rand.x, 0.5f, rand.y);

            if (_player == null) return pos;
            if (Vector3.Distance(pos, _player.position) >= _minSpawnDistance)
                return pos;
        }

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle) * _spawnRadius, 0.5f, Mathf.Sin(angle) * _spawnRadius);
    }

    private void StopSpawning() => _stopped = true;
}