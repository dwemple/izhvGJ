using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Animator animator;

    public int health;
    public int pollen;
    public int wetness;
    public PlayerStatus playerStatus;
    private float wetnessTimer = 0f;
    public float wetnessDespawn;

    private Vector2 respawnPos;
    public GameObject animatorObj;

    private void Awake()
    {
        gm = gameManageObject.GetComponent<GameManager>();
        animator = animatorObj.GetComponent<Animator>();
        respawnPos = new Vector2(2.36f, -3.23f);
    }
    private void Start()
    {
        StartCoroutine(WaitForAnimationFinish());
    }
    private void Update()
    {
        if (wetnessTimer >= wetnessDespawn && wetness > 0)
        {
            wetness--;
            gm.UpdateWetness();
            wetnessTimer = 0f;
        }
        wetnessTimer += Time.deltaTime;
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
        gm.UpdateWetness();
    }
    public void AddWetness()
    {
        StartCoroutine(WaitForWaterAnimation());
        wetness++;
        gm.UpdateWetness();
        if (wetness >= 5)
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
        gm.UpdateWetness();
        RemoveHealth();

        playerStatus = PlayerStatus.IMMUNE;
        gm.HideSpawners();
        StartCoroutine(WaitForAnimationFinish());
    }

    private IEnumerator WaitForAnimationFinish()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = respawnPos;
        yield return new WaitForSeconds(1.2f);
        animator.ResetTrigger("Hit");
        playerStatus = PlayerStatus.ALIVE;
        yield return new WaitForSeconds(1f);
        gm.ActivateSpawners();
    }
    private IEnumerator WaitForWaterAnimation()
    {
        animator.SetTrigger("Water");
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("Water");
    }
}
