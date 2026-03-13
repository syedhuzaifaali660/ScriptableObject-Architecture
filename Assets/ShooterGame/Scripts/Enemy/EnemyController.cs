using ScriptableObjectArchitecture;
using UnityEngine;

/// <summary>
/// Enemy behaviour:
///   - Faces and shoots at the player on a timer
///   - Dies after N hits (IntVariable)
///   - Reads IsGameOver BoolVariable — stops when true
///   - Raises OnEnemyDied and OnScoreChanged on death
///   - Auto-registers in EnemyCollection via EnemyCollectionMember
/// </summary>
[RequireComponent(typeof(EnemyCollectionMember))]
public class EnemyController : MonoBehaviour
{
    [Header("SO Variables")]
    [SerializeField] private IntVariable _hitsToKill;
    [SerializeField] private FloatVariable _bulletSpeed;
    [SerializeField] private IntVariable _scoreValue;
    [SerializeField] private IntVariable _currentScore;
    [SerializeField] private BoolVariable _isGameOver;

    [Header("SO Events")]
    [SerializeField] private GameEvent _onEnemyDied;
    [SerializeField] private IntGameEvent _onScoreChanged;

    [Header("Shooting")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate = 2f;

    [Header("References")]
    [SerializeField] private Transform _player;

    private int _hitsReceived;
    private float _nextFireTime;
    private bool _isDead;

    private void Start()
    {
        if (_player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) _player = p.transform;
        }

        _nextFireTime = Time.time + Random.Range(0.5f, _fireRate);
    }

    private void Update()
    {
        if (_isDead || _player == null) return;
        if (_isGameOver != null && _isGameOver.Value) return;

        FacePlayer();
        HandleShooting();
    }

    // ── Face player ───────────────────────────────────────────────────────────

    private void FacePlayer()
    {
        Vector3 dir = (_player.position - transform.position).normalized;
        dir.y = 0f;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    // ── Shoot ─────────────────────────────────────────────────────────────────

    private void HandleShooting()
    {
        if (Time.time < _nextFireTime) return;
        _nextFireTime = Time.time + _fireRate;

        if (_bulletPrefab == null) return;

        Vector3 dirToPlayer = (_player.position - transform.position);
        dirToPlayer.y = 0f;
        dirToPlayer.Normalize();

        Quaternion aimRotation = Quaternion.LookRotation(dirToPlayer);
        Vector3 spawnPos = transform.position + dirToPlayer * 1.2f;
        spawnPos.y = transform.position.y;

        GameObject bullet = Instantiate(_bulletPrefab, spawnPos, aimRotation);

        if (bullet.TryGetComponent<BulletController>(out var bc))
            bc.Initialize(_bulletSpeed.Value, "Enemy");
    }

    // ── Take hit ──────────────────────────────────────────────────────────────

    public void TakeHit()
    {
        if (_isDead) return;

        _hitsReceived++;
        Debug.Log($"[Enemy] Hit {_hitsReceived}/{_hitsToKill.Value}");

        StartCoroutine(HitFlash());

        if (_hitsReceived >= _hitsToKill.Value)
            Die();
    }

    private System.Collections.IEnumerator HitFlash()
    {
        var rend = GetComponent<Renderer>();
        if (rend == null) yield break;

        Color original = rend.material.color;
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        // Guard against destroy during flash
        if (rend != null)
            rend.material.color = original;
    }

    // ── Die ───────────────────────────────────────────────────────────────────

    private void Die()
    {
        _isDead = true;

        // Value assignment triggers the variable's built-in Raise() automatically
        _currentScore.Value += _scoreValue.Value;
        _onScoreChanged.Raise(_currentScore.Value);
        _onEnemyDied.Raise();

        Debug.Log($"[Enemy] Died! Score: {_currentScore.Value}");

        Destroy(gameObject);
    }
}