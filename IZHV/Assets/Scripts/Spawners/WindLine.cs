using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindLine : MonoBehaviour
{
    private bool direction; // true = up, false = down
    private SpriteRenderer sprite;
    public GameObject arrow;
    private Collider2D col;
    private bool isBlowing;
    public int windForce;
    private Rigidbody2D playerRigidbody;
    private float timer;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        direction = Coinflip();
        InitializeWarning();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!isBlowing && timer > 1.5f)
        {
            ChangeColor();
            arrow.SetActive(false);
            StartBlowing();
        }
        if (timer > 6f) Destroy(gameObject);
    }

    private bool Coinflip()
    {
        return Random.Range(0f, 1f) >= 0.5f;
    }
    private void StartBlowing()
    {
        isBlowing = true;
    }
    private void ChangeColor()
    {
        sprite.color = new Color(255, 255, 255, 0.3f);
    }
    private void InitializeWarning()
    {
        transform.rotation = Quaternion.Euler(direction ? 180 : 0, 0, 0);
        arrow.transform.position -= new Vector3(0, 0, direction ? 2 : 0);
        sprite.color = new Color(255,0,0,0.5f);
        arrow.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (isBlowing && other.gameObject.CompareTag("Player"))
        {
            playerRigidbody.AddForce((direction ? Vector2.down : Vector2.up) * windForce * 100);
        }
    }
}
