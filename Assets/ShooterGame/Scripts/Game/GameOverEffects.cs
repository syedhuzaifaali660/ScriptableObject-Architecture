using UnityEngine;

/// <summary>
/// DEMONSTRATION OF ZERO-CODE EVENT LISTENER WIRING
///
/// Add a GameEventListener component on the same GameObject and wire:
///   - Game Event → OnPlayerDied
///   - Response   → GameOverEffects.LogMessage / HideObjects / PlaySound
///
/// This component reacts to the player dying without a single line of
/// connection code — the GameEventListener bridges it in the Inspector.
/// </summary>
public class GameOverEffects : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] private GameObject[] _objectsToHide;
    [SerializeField] private GameObject[] _objectsToShow;
    [SerializeField] private AudioSource _audioSource;

    // ── Wired via GameEventListener Response in the Inspector ────────────────

    public void HideObjects()
    {
        foreach (var obj in _objectsToHide)
            if (obj != null) obj.SetActive(false);
    }

    public void ShowObjects()
    {
        foreach (var obj in _objectsToShow)
            if (obj != null) obj.SetActive(true);
    }

    public void PlaySound()
    {
        if (_audioSource != null) _audioSource.Play();
    }

    public void StopSound()
    {
        if (_audioSource != null) _audioSource.Stop();
    }

    public void LogMessage(string message)
    {
        Debug.Log($"[GameOverEffects] {message} — fired via zero-code GameEventListener!");
    }
}