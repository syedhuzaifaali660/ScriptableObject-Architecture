using ScriptableObjectArchitecture;
using UnityEngine;

/// <summary>
/// Handles player movement (WASD) and shooting (Left Click).
/// All tuning values come from ScriptableObject Variables.
/// Listens to OnPlayerDamaged FloatGameEvent via AddListener.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("SO Variables")]
    [SerializeField] private FloatVariable _moveSpeed;
    [SerializeField] private FloatVariable _health;
    [SerializeField] private BoolVariable _isGameOver;

    [Header("SO Events")]
    [SerializeField] private GameEvent _onPlayerDied;
    [SerializeField] private FloatGameEvent _onPlayerDamaged;

    [Header("Shooting")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private FloatVariable _bulletSpeed;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 0.3f;

    private float _nextFireTime;
    private bool _isDead;
    private Camera _cam;
    private Rigidbody _rb;
    private Vector3 _moveInput;

    private void Awake()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _onPlayerDamaged.AddListener(TakeDamage);
    }

    private void OnDisable()
    {
        _onPlayerDamaged.RemoveListener(TakeDamage);
    }

    private void Update()
    {
        if (_isDead) return;

        float h = 0f, v = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  h = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) h =  1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))  v = -1f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))    v =  1f;

        _moveInput = new Vector3(h, 0f, v).normalized;

        HandleAiming();
        HandleShooting();
    }

    private void FixedUpdate()
    {
        if (_isDead) return;
        HandleMovement();
    }

    // ── Movement ──────────────────────────────────────────────────────────────

    private void HandleMovement()
    {
        Vector3 newPos = _rb.position + _moveInput * _moveSpeed.Value * Time.fixedDeltaTime;
        _rb.MovePosition(newPos);
    }

    // ── Aiming ────────────────────────────────────────────────────────────────

    private void HandleAiming()
    {
        if (_cam == null) return;

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 lookPoint = ray.GetPoint(distance);
            lookPoint.y = transform.position.y;
            transform.LookAt(lookPoint);
        }
    }

    // ── Shooting ──────────────────────────────────────────────────────────────

    private void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + _fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_bulletPrefab == null || _firePoint == null) return;

        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        if (bullet.TryGetComponent<BulletController>(out var bc))
            bc.Initialize(_bulletSpeed.Value, gameObject.tag);
    }

    // ── Damage ────────────────────────────────────────────────────────────────

    private void TakeDamage(float amount)
    {
        if (_isDead) return;

        _health.Value -= amount;
        Debug.Log($"[Player] Took {amount} damage. Health: {_health.Value}");

        if (_health.Value <= 0f)
            Die();
    }

    private void Die()
    {
        _isDead = true;
        Debug.Log("[Player] Died!");

        if (_isGameOver != null) _isGameOver.Value = true;

        _onPlayerDied.Raise();
        gameObject.SetActive(false);
    }
}