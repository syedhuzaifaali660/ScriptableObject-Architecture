using UnityEngine;

/// <summary>
/// Moves forward at set speed. On collision:
///   - Player bullet hits Enemy → calls TakeHit() on EnemyController
///   - Enemy bullet hits Player → EnemyBulletDamageRaiser raises OnPlayerDamaged
///   - Any bullet hits Ground   → destroyed
/// </summary>
public class BulletController : MonoBehaviour
{
    [SerializeField] private float _lifetime = 4f;

    private float _speed;
    private string _ownerTag;

    public void Initialize(float speed, string ownerTag)
    {
        _speed = speed;
        _ownerTag = ownerTag;
        Destroy(gameObject, _lifetime);

        // Remove Rigidbody if present — we move manually
        var rb = GetComponent<Rigidbody>();
        if (rb != null) Destroy(rb);
    }

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player bullet hits enemy
        if (_ownerTag == "Player" && other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<EnemyController>(out var enemy))
                enemy.TakeHit();
            Destroy(gameObject);
            return;
        }

        // Enemy bullet hits player
        if (_ownerTag == "Enemy" && other.CompareTag("Player"))
        {
            var damageRaiser = GetComponent<EnemyBulletDamageRaiser>();
            damageRaiser?.RaiseDamage();
            Destroy(gameObject);
            return;
        }

        // Hit ground or walls
        if (other.CompareTag("Ground"))
            Destroy(gameObject);
    }
}