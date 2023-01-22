using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIHandler : MonoBehaviour
{
    public GameObject pauseScreen;

    public GameObject mainMenuButton;
    public GameObject mainMenuPauseButton;
    public GameObject restartButton;
    public GameObject pauseButton;
    public GameObject unpauseButton;

    private void Awake()
    {
        mainMenuButton.GetComponent<Button>().onClick.AddListener(GoToMainMenu);
        mainMenuPauseButton.GetComponent<Button>().onClick.AddListener(GoToMainMenu);
        restartButton.GetComponent<Button>().onClick.AddListener(RestartGame);
        pauseButton.GetComponent<Button>().onClick.AddListener(PauseGame);
        unpauseButton.GetComponent<Button>().onClick.AddListener(UnpauseGame);
    }
    private void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainGame");
    }
    private void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
    private void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    private void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
    }
}
