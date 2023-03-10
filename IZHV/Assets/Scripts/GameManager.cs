using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject bubbleObject;
    public GameObject endScreen;

    public GameObject spawners;
    private SpawnerManager sm;

    public PlayerStatistics playerStats;

    public GameObject pollenTextObject;
    private Text pollenText;

    public List<GameObject> lives;
    private int livesShowing = 3;
    private float timer;

    public float startPollen;
    public float startRain;
    public float startWind;
    public float startWasp;

    private bool updateTimer = true;

    public float endTimer;

    private void Awake()
    {
        pollenText = pollenTextObject.GetComponent<Text>();
        sm = spawners.GetComponent<SpawnerManager>();
        playerStats = playerObject.GetComponent<PlayerStatistics>();
        UpdatePollen();
    }

    private void Start()
    {
        timer = 0;
        playerObject.SetActive(true);
    }
    private void Update()
    {
        if (updateTimer) timer += Time.deltaTime;
        if (!sm.pollenActive && timer > startPollen)
        {
            sm.DeActivatePollenSpawner(true);
        }
        if (!sm.rainActive && timer > startRain)
        {
            sm.DeActivateRainSpawner(true);
        }
        if (!sm.windActive && timer > startWind)
        {
            sm.DeActivateWindSpawner(true);
        }
        if (!sm.waspActive && timer > startWasp)
        {
            sm.DeActivateWaspSpawner(true);
        }
        if (timer > endTimer) WonGame();
    }
    public void HideSpawners()
    {
        sm.DisableAllSpawners();
        sm.RemoveAllObject();
        spawners.SetActive(false);
    }
    public void ActivateSpawners()
    {
        spawners.SetActive(true);
        sm.ActivateAllSpawners();
    }
    public void ShowBubble()
    {
        bubbleObject.SetActive(true);
    }
    public void HideBubble()
    {
        bubbleObject.SetActive(false);
    }
    public void WonGame()
    {
        updateTimer = false;
        timer = 0;
        HideSpawners();
        GetComponent<EndingManager>().HideFog();
    }
    public void LostGame()
    {
        Time.timeScale = 0;
        playerObject.SetActive(false);
        endScreen.SetActive(true);
    }

    /// <summary>
    /// GUI UPDATES
    /// </summary>
    public void UpdatePollen()
    {
        pollenText.text = playerStats.pollen.ToString();
    }
    public void UpdateHealth(int count)
    {
        if (livesShowing < count) lives[livesShowing++].SetActive(true);
        else lives[--livesShowing].SetActive(false);
    }
}
