using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public DifficultySettings[] difficultyLevels;
    public DifficultySettings currentDifficulty;

    public int currentScore;
    public int highScore;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverHighScoreText;
    public GameObject newRecordText;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        int diffIndex = PlayerPrefs.GetInt("SelectedDifficulty", 0);
        if (difficultyLevels != null && difficultyLevels.Length > diffIndex)
        {
            currentDifficulty = difficultyLevels[diffIndex];
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        currentScore = 0;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        LoadHighScore();
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        currentScore += amount;
        UpdateUI();
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        CheckHighScore();

        if (gameOverScoreText != null) gameOverScoreText.text = "SCORE: " + currentScore.ToString();
        if (gameOverHighScoreText != null) gameOverHighScoreText.text = "BEST: " + highScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void CheckHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            if (newRecordText != null) newRecordText.SetActive(true);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null) scoreText.text = currentScore.ToString();
        if (highScoreText != null) highScoreText.text = "BEST: " + highScore.ToString();
    }
}