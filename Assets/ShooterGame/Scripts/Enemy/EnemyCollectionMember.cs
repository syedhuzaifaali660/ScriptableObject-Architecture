using ScriptableObjectArchitecture;
using UnityEngine;

/// <summary>
/// Attach to every Enemy prefab.
/// Auto-registers this enemy in EnemyCollection on spawn, removes on death.
/// No manual wiring needed.
/// </summary>
public class EnemyCollectionMember : MonoBehaviour
{
    [SerializeField] private EnemyCollection _collection;

    private EnemyController _enemy;

    private void OnEnable()
    {
        _enemy = GetComponent<EnemyController>();
        if (_collection != null && _enemy != null)
            _collection.Add(_enemy);
    }

    private void OnDisable()
    {
        if (_collection != null && _enemy != null)
            _collection.Remove(_enemy);
    }
}
