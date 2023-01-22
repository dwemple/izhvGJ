using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public Vector2 waspSpawnTimers;
    public Vector2 windSpawnTimers;
    public float rainSpawnTimer;
    public float pollenSpawnTimer;

    public GameObject waspObj;
    public GameObject windObj;
    public GameObject rainObj;
    public GameObject pollenObj;

    private WaspSpawner wasp;
    private WindSpawner wind;
    private RainSpawner rain;
    private PollenSpawner pollen;

    public bool pollenActive = false;
    public bool rainActive = false;
    public bool windActive = false;
    public bool waspActive = false;

    private void Awake()
    {
        wasp = waspObj.GetComponent<WaspSpawner>();
        wind = windObj.GetComponent<WindSpawner>();
        rain = rainObj.GetComponent<RainSpawner>();
        pollen = pollenObj.GetComponent<PollenSpawner>();
    }
    private void Start()
    {
        wasp.canSpawn = false;
        wind.canSpawn = false;
        pollen.canSpawn = false;
        rain.canSpawn = false;
    }
    public void DeActivateWaspSpawner(bool set)
    {
        wasp.canSpawn = set;
        waspActive = set;
    }
    public void DeActivateWindSpawner(bool set)
    {
        wind.canSpawn = set;
        windActive = set;
    }
    public void DeActivatePollenSpawner(bool set)
    {
        pollen.canSpawn = set;
        pollenActive = set;
    }
    public void DeActivateRainSpawner(bool set)
    {
        rain.canSpawn = set;
        rainActive = set;
    }
    public void RemoveAllObject()
    {
        wasp.RemoveDuringDeath();
        wind.RemoveDuringDeath();
        rain.RemoveDuringDeath();
        pollen.RemoveDuringDeath();
    }
}
