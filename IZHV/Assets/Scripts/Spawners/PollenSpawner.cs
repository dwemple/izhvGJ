using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenSpawner : MonoBehaviour
{
    private SpawnerManager sm;
    private List<GameObject> pollenList = new List<GameObject>();
    public GameObject pollenPrefab;
    public int speed;

    public GameObject player;
    private PlayerStatistics playerStats;
    private float timer;
    public bool canSpawn = true;

    private void Awake()
    {
        sm = transform.parent.gameObject.GetComponent<SpawnerManager>();
        playerStats = player.GetComponent<PlayerStatistics>();
    }
    void Update()
    {
        if (!canSpawn) return;
        timer += Time.deltaTime;
        if (timer > sm.pollenSpawnTimer)
        {
            SpawnPollen();
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        foreach (var pollen in pollenList)
        {
            pollen.transform.position += Vector3.down * speed * Time.fixedDeltaTime;
        }
    }
    private void SpawnPollen()
    {
        var pollen = Instantiate(pollenPrefab, transform);
        pollen.transform.localPosition = Vector3.right * Random.Range(3.0f, 11.5f);
        pollenList.Add(pollen);
    }
    public void PlayerHitBridge(GameObject pollen)
    {
        playerStats.AddPollen();
        DespawnPollen(pollen);
    }

    public void DespawnPollen(GameObject pollen)
    {
        pollenList.Remove(pollen);
        Destroy(pollen);
    }

    public void RemoveDuringDeath()
    {
        for (int i = 0; i < pollenList.Count; i++)
        {
            var toDelete = pollenList[0];
            pollenList.RemoveAt(0);
            Destroy(toDelete);
        }
    }
}
