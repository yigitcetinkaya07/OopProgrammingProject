using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MainUIHandler : MonoBehaviour
{
    public bool gameIsPaused = false;
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private int m_Points;
    [field:SerializeField]
    public bool gameOver { get; private set; } = false;
    // private HighScoreTable highScoreTable;
    private static DataManager dataManagerInstance = DataManager.Instance;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    public void AddPoint(int point)
    {
        if (!gameOver)
        {
            m_Points += point;
            scoreText.text = $"Score : {m_Points}";
        }

    }
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseScreen.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.gameObject.SetActive(false);
        }
    }
    public void GameOver()
    {
        if (dataManagerInstance.CheckScoreCount())
        {
            HighScoreEntry highScoreEntry = dataManagerInstance.GetLastScore();
            if (m_Points >= highScoreEntry.score)
            {
                dataManagerInstance.RemoveScore();
                dataManagerInstance.AddHighScoreEntry(m_Points, dataManagerInstance.inputName);
            }
        }
        else
        {
            dataManagerInstance.AddHighScoreEntry(m_Points, dataManagerInstance.inputName);
        }
        gameOver = true;
        gameOverScreen.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
             Application.Quit(); // original code to quit Unity player
        #endif
    }
}
