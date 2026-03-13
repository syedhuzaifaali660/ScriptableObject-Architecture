using ScriptableObjectArchitecture;
using UnityEngine;

/// <summary>
/// Attach to the Enemy bullet prefab alongside BulletController.
/// Raises OnPlayerDamaged FloatGameEvent when the bullet hits the player.
/// Fully decoupled — PlayerController reacts via its own listener.
/// </summary>
public class EnemyBulletDamageRaiser : MonoBehaviour
{
    [SerializeField] private FloatGameEvent _onPlayerDamaged;
    [SerializeField] private float _damageAmount = 10f;

    public void RaiseDamage()
    {
        _onPlayerDamaged?.Raise(_damageAmount);
    }
}