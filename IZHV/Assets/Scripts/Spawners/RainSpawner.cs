using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    private List<GameObject> raindrops = new List<GameObject>();
    public GameObject raindropPrefab;
    public int speed;
    public GameObject player;
    private PlayerStatistics playerStats;
    private SpawnerManager sm;

    private float timer;
    public bool canSpawn = true;

    private void Awake()
    {
        playerStats = player.GetComponent<PlayerStatistics>();
        sm = transform.parent.gameObject.GetComponent<SpawnerManager>();
    }
    private void Update()
    {
        if (!canSpawn) return;
        if (timer >= sm.rainSpawnTimer)
        {
            SpawnRaindrop();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        foreach (var raindrop in raindrops)
        {
            raindrop.transform.position += Vector3.down * speed * Time.fixedDeltaTime;
        }
    }
    private void SpawnRaindrop()
    {
        var raindrop = Instantiate(raindropPrefab, transform);
        raindrop.transform.localPosition = Vector3.right * Random.Range(3.0f, 11.8f);
        raindrop.transform.localScale = Vector3.one * Random.Range(1,3);
        raindrops.Add(raindrop);
    }
    public void PlayerHitBridge(GameObject raindrop)
    {
        playerStats.AddWetness();
        DespawnRaindrop(raindrop);
    }

    public void DespawnRaindrop(GameObject raindrop)
    {
        raindrops.Remove(raindrop);
        Destroy(raindrop);
    }

    public void RemoveDuringDeath()
    {
        for(int i = 0; i < raindrops.Count; i++)
        {
            var toDelete = raindrops[0];
            raindrops.RemoveAt(0);
            Destroy(toDelete);
        }
    }
}
