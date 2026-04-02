using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject difficultyPanel;
    public TextMeshProUGUI highScoreText;

    private void Start()
    {
        if (difficultyPanel != null)
        {
            difficultyPanel.SetActive(false);
        }

        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
        {
            highScoreText.text = "BEST SCORE: " + bestScore.ToString();
        }
    }

    public void OnPlayButtonClicked()
    {
        if (difficultyPanel != null)
        {
            difficultyPanel.SetActive(true);
        }
    }

    public void StartEasyMode()
    {
        PlayerPrefs.SetInt("SelectedDifficulty", 0);
        SceneManager.LoadScene("Game");
    }

    public void StartNormalMode()
    {
        PlayerPrefs.SetInt("SelectedDifficulty", 1);
        SceneManager.LoadScene("Game");
    }

    public void StartHardcoreMode()
    {
        PlayerPrefs.SetInt("SelectedDifficulty", 2);
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}