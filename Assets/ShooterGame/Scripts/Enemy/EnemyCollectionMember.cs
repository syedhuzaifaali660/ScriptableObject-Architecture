using ScriptableObjectArchitecture;
using UnityEngine;

/// <summary>
/// Attach to every Enemy prefab.
/// Auto-registers this enemy in EnemyCollection on spawn, removes on death.
/// No manual wiring needed.
/// </summary>
public class EnemyCollectionMember : MonoBehaviour
{
    [SerializeField] private GameObjectCollection _collection;

    private EnemyController _enemy;

private void OnEnable()
{
    if (_collection != null)
        _collection.Add(gameObject);
}

private void OnDisable()
{
    if (_collection != null)
        _collection.Remove(gameObject);
}
}
