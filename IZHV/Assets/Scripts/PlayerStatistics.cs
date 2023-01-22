using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerStatus
{
    ALIVE,
    BUBBLED,
    IMMUNE,
    DEAD
}

public class PlayerStatistics : MonoBehaviour
{
    public GameObject gameManageObject;
    private GameManager gm;
    public GameObject wetBar;
    public GameObject wetProgressBar;
    private Image wetProgress;
    private Animator animator;

    public float maxWetness;

    public int health;
    public int pollen;
    public float wetness;
    public PlayerStatus playerStatus;

    private Vector2 respawnPos;
    public GameObject animatorObj;

    private void Awake()
    {
        gm = gameManageObject.GetComponent<GameManager>();
        animator = animatorObj.GetComponent<Animator>();
        respawnPos = new Vector2(2.36f, -3.23f);
        wetProgress = wetProgressBar.GetComponent<Image>();
    }
    private void Start()
    {
        StartCoroutine(WaitForAnimationFinishStart());
    }
    private void Update()
    {
        if (wetness > 0)
        {
            wetness -= Time.deltaTime * 0.5f;
        }
    }
    private void FixedUpdate()
    {
        UpdateWetnessBar();
        UpdateWetBarPosition();
    }
    private void UpdateWetBarPosition()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        wetBar.transform.position = screenPos - new Vector3(0,75,0);
    }
    public void RemoveWetnessWhenInBubble()
    {
        if (wetness <= 0)
        {
            wetness = 0;
            gm.HideBubble();
            playerStatus = PlayerStatus.ALIVE;
            return;
        }
        wetness--;
    }
    public void AddWetness()
    {
        StartCoroutine(WaitForWaterAnimation());
        wetness++;
        if (wetness >= maxWetness)
        {
            playerStatus = PlayerStatus.BUBBLED;
            gm.ShowBubble();
        }
    }
    public void AddPollen()
    {
        pollen++;
        if (pollen % 10 == 0) AddHealth();
        gm.UpdatePollen();
    }
    private void AddHealth()
    {
        if (health < 5) gm.UpdateHealth(++health);
    }
    private void RemoveHealth()
    {
        gm.UpdateHealth(--health);
        if (health == 0) gm.LostGame();
    }
    public void GetHit()
    {
        animator.SetTrigger("Hit");
        if (playerStatus == PlayerStatus.BUBBLED) gm.HideBubble();
        wetness = 0;
        wetBar.SetActive(false);
        RemoveHealth();

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        playerStatus = PlayerStatus.IMMUNE;
        gm.HideSpawners();
        StartCoroutine(WaitForAnimationFinish());
    }

    private IEnumerator WaitForAnimationFinishStart()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = respawnPos;
        yield return new WaitForSeconds(1.2f);
        animator.ResetTrigger("Hit");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        playerStatus = PlayerStatus.ALIVE;
        wetBar.SetActive(true);
        UpdateWetnessBar();
        yield return new WaitForSeconds(1f);
        gm.spawners.SetActive(true);

    }
    private IEnumerator WaitForAnimationFinish()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = respawnPos;
        yield return new WaitForSeconds(1.2f);
        animator.ResetTrigger("Hit");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        playerStatus = PlayerStatus.ALIVE;
        wetBar.SetActive(true);
        UpdateWetnessBar();
        yield return new WaitForSeconds(1f);
        gm.ActivateSpawners();
    }
    private IEnumerator WaitForWaterAnimation()
    {
        animator.SetTrigger("Water");
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("Water");
    }
    private void UpdateWetnessBar()
    {
        if(wetness < 5 && wetness > 0) wetProgress.fillAmount = wetness / maxWetness;
    }
}
