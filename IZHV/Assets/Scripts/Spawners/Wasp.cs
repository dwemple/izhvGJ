using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    private float timer;
    private Animator animator;
    public bool isFiring = false;
    private float bulletTimer;

    public float waspDespawn;
    public float bulletSpawn;
    public int bulletSpeed;
    private GameObject player;
    public float waspStart;

    public GameObject bulletPrefab;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = transform.parent.GetComponent<WaspSpawner>().player;
        timer = 0;
        animator.Play("MovementIn");
    }
    void Update()
    {
        IterateTimers();
        if (!isFiring && timer > waspStart) isFiring = true;
        if (timer > waspDespawn)
        {
            isFiring = false;
            animator.Play("MovementOut");
        }
        if (timer > waspDespawn + 1.5f)
        {
            transform.parent.GetComponent<WaspSpawner>().DeleteWasp(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (isFiring && bulletTimer >= bulletSpawn)
        {
            FireBullet();
            bulletTimer = 0;
        }
    }
    private void FireBullet()
    {
        var newBullet = Instantiate(bulletPrefab, transform);
        newBullet.GetComponent<WaspBullet>().parentRef = this;
        newBullet.transform.parent = transform.parent;
        var bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = (player.transform.position - newBullet.transform.position).normalized * bulletSpeed;
        newBullet.transform.up = -(player.transform.position - newBullet.transform.position);
    }
    public void RemoveBulletOnHit(GameObject bullet)
    {
        Destroy(bullet);
    }
    private void IterateTimers()
    {
        timer += Time.deltaTime;
        bulletTimer += Time.deltaTime;
    }
}
