using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    private SpawnerManager sm;

    private float timer = 0;
    private float spawnTimer;

    public GameObject windPrefab;

    private List<GameObject> winds = new List<GameObject>();

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
        if (timer > spawnTimer)
        {
            SpawnWindStream();
            RerollSpawnTimer();
            timer = 0;
        }
    }
    private void RerollSpawnTimer()
    {
        spawnTimer = Random.Range(sm.windSpawnTimers.x, sm.windSpawnTimers.y);
    }

    private void SpawnWindStream()
    {
        var wind = Instantiate(windPrefab, transform);
        wind.transform.localPosition = Vector3.right * Random.Range(1, 8);
        winds.Add(wind);
    }
    public void RemoveDuringDeath()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
