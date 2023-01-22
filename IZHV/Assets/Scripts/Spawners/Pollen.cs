using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{
    private PollenSpawner parentScript;
    private void Awake()
    {
        parentScript = transform.parent.gameObject.GetComponent<PollenSpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Despawner"))
        {
            parentScript.DespawnPollen(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            parentScript.PlayerHitBridge(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collector"))
        {
            parentScript.PlayerHitBridge(gameObject);
        }
    }
}
