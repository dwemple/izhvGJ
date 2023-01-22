using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspSpawner : MonoBehaviour
{
    private SpawnerManager sm;

    private float timer;
    private float spawnTimer;

    private GameObject leftWasp;
    private GameObject rightWasp;

    private int waspCount;

    public GameObject waspPrefabLeft;
    public GameObject waspPrefabRight;

    public GameObject player;
    public bool canSpawn = true;

    private void Awake()
    {
        sm = transform.parent.gameObject.GetComponent<SpawnerManager>();
    }
    private void Start()
    {
        RerollSpawnTimer();
    }
    private void Update()
    {
        if (!canSpawn) return;
        timer += Time.deltaTime;
        if(timer > spawnTimer)
        {
            SpawnWasp();
            timer = 0;
        }
    }
    private void SpawnWasp()
    {
        if (waspCount == 2) return;
        if (leftWasp == null && (Coinflip() || rightWasp != null))
        {
            leftWasp = Instantiate(waspPrefabLeft, transform);
        } else
        {
            rightWasp = Instantiate(waspPrefabRight, transform);
        }
        waspCount++;
        RerollSpawnTimer();
    }
    public void DeleteWasp(GameObject wasp)
    {
        if (wasp == leftWasp)
        {
            leftWasp = null;
        }
        else
        {
            rightWasp = null;
        }
        Destroy(wasp);
        waspCount--;
    }
    private void RerollSpawnTimer()
    {
        spawnTimer = Random.Range(sm.waspSpawnTimers.x, sm.waspSpawnTimers.y);
    }
    private bool Coinflip()
    {
        return Random.Range(0f, 1f) <= 0.5f;
    }
    public void RemoveDuringDeath()
    {
        if (leftWasp)
        {
            leftWasp.GetComponent<Wasp>().isFiring = false;
            Destroy(leftWasp);
            leftWasp=null;
        }
        if (rightWasp)
        {
            rightWasp.GetComponent<Wasp>().isFiring = false;
            Destroy(rightWasp);
            rightWasp = null;
        }
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        waspCount = 0;
    }
}
