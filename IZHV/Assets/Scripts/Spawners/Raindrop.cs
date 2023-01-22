using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raindrop : MonoBehaviour
{
    private RainSpawner parentScript;
    private void Awake()
    {
        parentScript = transform.parent.gameObject.GetComponent<RainSpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Despawner"))
        {
            parentScript.DespawnRaindrop(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            parentScript.PlayerHitBridge(gameObject);
        }
    }

}
