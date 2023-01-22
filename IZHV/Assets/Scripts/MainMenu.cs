using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject exit;
    public GameObject play;

    private void Awake()
    {
        exit.GetComponent<Button>().onClick.AddListener(ExitGame);
        play.GetComponent<Button>().onClick.AddListener(StartGame);
    }
    private void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
