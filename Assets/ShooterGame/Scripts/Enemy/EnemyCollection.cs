using ScriptableObjectArchitecture;
using UnityEngine;

/// <summary>
/// Typed Collection that tracks all live enemies in the scene.
/// Auto-populated via EnemyCollectionMember on each enemy prefab.
/// </summary>
[CreateAssetMenu(
    fileName = "EnemyCollection.asset",
    menuName = "Shooter/Enemy Collection")]
public class EnemyCollection : Collection<EnemyController> { }
