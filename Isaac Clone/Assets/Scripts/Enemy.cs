using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour{

    [Header("Enemy movement value")]
    public float enemyMovementSpeed;
    public float stoppingDistance;
    public float retreatDistance;

    [Header("Enemy Healthvalues")]
    public float health;

    [Header("Enemy shooting values")]
    public GameObject projectTile;

    private float timeBetweenShots;
    public float startTimeBetweenShot;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweenShot;
    }

    // Update is called once per frame
    void Update()
    {
        // check to see if our enemy distance relative to the player is greater than our stopping distance
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            // move the enemy position towards the player position
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyMovementSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && (Vector2.Distance(transform.position, player.position) > retreatDistance))
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -enemyMovementSpeed * Time.deltaTime);
        }

        if(timeBetweenShots <= 0)
        {
            // Instantiate means to spawn a item with the values at what position and what rotation
            Instantiate(projectTile, transform.position, quaternion.identity);
            timeBetweenShots = startTimeBetweenShot; // remove this and he shoots every single frame
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

        EnemyDeath();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerProjectile"))
        {
            health = health - 1;
        }
    }

    public void EnemyDeath()
    {
        if(health == 0)
        {
            Destroy(gameObject);
        }
    }
}
