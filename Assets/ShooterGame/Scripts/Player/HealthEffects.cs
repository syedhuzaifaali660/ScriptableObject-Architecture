using ScriptableObjectArchitecture;
using UnityEngine;

/// <summary>
/// DEMONSTRATION OF ZERO-CODE VARIABLE LISTENER WIRING
///
/// Attach alongside a FloatGameEventListener component.
/// In the listener Inspector:
///   - Game Event → PlayerHealth (FloatVariable acts as event)
///   - Response   → HealthEffects.OnHealthChanged
///
/// No direct reference to PlayerHealth in code — the listener bridges it.
/// </summary>
public class HealthEffects : MonoBehaviour
{
    [Header("Visual Feedback")]
    [SerializeField] private Renderer _playerRenderer;
    [SerializeField] private float _fullHealth = 100f;

    // ── Wired via FloatGameEventListener in the Inspector ────────────────────

    /// <summary>Changes player color green → red as health drops.</summary>
    public void OnHealthChanged(float currentHealth)
    {
        if (_playerRenderer == null) return;

        float t = Mathf.Clamp01(currentHealth / _fullHealth);
        _playerRenderer.material.color = Color.Lerp(Color.red, Color.green, t);

        Debug.Log($"[HealthEffects] Health: {currentHealth} — color updated via zero-code wiring!");
    }

    /// <summary>Logs a warning when health is critical.</summary>
    public void WarnIfLowHealth(float currentHealth)
    {
        if (currentHealth <= 30f)
            Debug.LogWarning($"[HealthEffects] Critical health! {currentHealth} remaining!");
    }
}