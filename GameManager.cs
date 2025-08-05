using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // THIS IS THE TOP OF THE CLASS
    public static GameManager Instance;

    // 🧠 General game states
    public bool isGamePaused = false;

    // ✅ 1. Add the first variable (Game Over flag)
    public bool isGameOver;

    // 💚 Player variables
    // ✅ 2. Add the second variable (Health)
    public int playerHealth = 100;
    public int maxHealth = 100;

    // 🎛️ UI Elements (drag in Inspector)
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;

    // ✅ 3. Add the third variable (Start menu UI)
    public GameObject startMenuUI;

    public Slider healthBar;

    // 🕹️ Optional Audio
    public AudioSource bgMusic;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 0f; // Game paused at start
        ShowStartMenu();
        UpdateHealthUI(); // Initial health setup
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (isGamePaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // ✅ Start Game Logic
    public void StartGame()
    {
        startMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        if (bgMusic != null) bgMusic.Play();
    }

    // ✅ Game Over Logic
    public void EndGame()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        Debug.Log("Game Over!");
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    // Health System
    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            EndGame();
        }
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        playerHealth += amount;
        if (playerHealth > maxHealth)
            playerHealth = maxHealth;
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.value = (float)playerHealth / maxHealth;
    }

    // ✅ Restart
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Optional - shows start menu
    public void ShowStartMenu()
    {
        if (startMenuUI != null)
            startMenuUI.SetActive(true);
    }

    // 🧠 TODO: AddScore(), CompleteLevel(), QuitGame() if needed later
}