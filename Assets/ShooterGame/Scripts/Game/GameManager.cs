using ScriptableObjectArchitecture;
using UnityEngine;
using TMPro;

/// <summary>
/// Listens to SO Events to manage game state and HUD.
/// No direct references to Player, Enemy, or UI — only SO assets.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("SO Events — listen")]
    [SerializeField] private GameEvent _onPlayerDied;
    [SerializeField] private GameEvent _onEnemyDied;
    [SerializeField] private IntGameEvent _onScoreChanged;

    [Header("SO Variables — read")]
    [SerializeField] private FloatVariable _playerHealth;
    [SerializeField] private IntVariable _currentScore;

    [Header("UI")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private TextMeshProUGUI _enemiesKilledText;

    private int _enemiesKilled;

    // ── Lifecycle ─────────────────────────────────────────────────────────────

    private void OnEnable()
    {
        _onPlayerDied.AddListener(OnPlayerDied);
        _onEnemyDied.AddListener(OnEnemyDied);
        _onScoreChanged.AddListener(OnScoreChanged);

        // FloatVariable IS a GameEventBase — AddListener hooks directly into it
        if (_playerHealth != null) _playerHealth.AddListener(OnHealthChanged);
    }

    private void OnDisable()
    {
        _onPlayerDied.RemoveListener(OnPlayerDied);
        _onEnemyDied.RemoveListener(OnEnemyDied);
        _onScoreChanged.RemoveListener(OnScoreChanged);

        if (_playerHealth != null) _playerHealth.RemoveListener(OnHealthChanged);
    }

    private void Start()
    {
        if (_gameOverPanel != null) _gameOverPanel.SetActive(false);
        RefreshHUD();
    }

    // ── Event Handlers ────────────────────────────────────────────────────────

    private void OnPlayerDied()
    {
        Debug.Log("[GameManager] Player died");

        if (_gameOverPanel != null) _gameOverPanel.SetActive(true);
        if (_finalScoreText != null) _finalScoreText.text = $"Score: {_currentScore.Value}";
        if (_enemiesKilledText != null) _enemiesKilledText.text = $"Enemies Killed: {_enemiesKilled}";

        Time.timeScale = 0f;
    }

    private void OnEnemyDied()
    {
        _enemiesKilled++;
    }

    private void OnScoreChanged(int newScore)
    {
        if (_scoreText != null) _scoreText.text = $"Score: {newScore}";
    }

    private void OnHealthChanged(float newHealth)
    {
        if (_healthText != null) _healthText.text = $"HP: {newHealth:0}";
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private void RefreshHUD()
    {
        if (_scoreText != null && _currentScore != null)
            _scoreText.text = $"Score: {_currentScore.Value}";
        if (_healthText != null && _playerHealth != null)
            _healthText.text = $"HP: {_playerHealth.Value:0}";
    }

    // ── Restart ───────────────────────────────────────────────────────────────

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}