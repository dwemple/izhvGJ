using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    private GameManager gm;

    public GameObject beehive;
    public GameObject fog;
    public GameObject congratzMsg;

    public GameObject pollenTextObj;
    public void Awake()
    {
        gm = GetComponent<GameManager>();
    }

    public void HideFog()
    {
        StartCoroutine(WaitForFogToHideAndSpawnBeehive());
    }
    public void ShowBeehive()
    {
        beehive.SetActive(true);
        StartCoroutine(WaitForBeehiveAndPopupCongratulationMessage());
    }
    private IEnumerator WaitForBeehiveAndPopupCongratulationMessage()
    {
        yield return new WaitForSeconds(5f);
        UpdatePollenOnEndingScreen();
        congratzMsg.SetActive(true);
        gm.playerObject.SetActive(false);
    }
    private void UpdatePollenOnEndingScreen()
    {
        pollenTextObj.GetComponent<Text>().text = gm.playerStats.pollen.ToString();
    }
    private IEnumerator WaitForFogToHideAndSpawnBeehive()
    {
        fog.GetComponent<Animator>().SetTrigger("Hide");
        yield return new WaitForSeconds(3f);
        fog.SetActive(false);
        ShowBeehive();
    }
}
