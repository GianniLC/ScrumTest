using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    [Header("Player Values")]
    public float health;
    public float attackDamage;

    public float movementSpeed;

    [Header("ProjectileDamageTaken")]
    public float projectileDamage;

    [Header("Bullet options")]
    public GameObject bulletSprite; // what sprite to use as a bullet
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    [Header("hallo dit is mijn header")]
    public float randomnummer;

    private void FixedUpdate()
    {
        Death();
        // horizontal player movement
        Vector3 movementX = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        transform.position += movementX * Time.deltaTime * movementSpeed;
        // vertical player movement
        Vector3 movementY = new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
        transform.position += movementY * Time.deltaTime * movementSpeed;
        // horizontal bullet movement
        float bulletMovementX = Input.GetAxisRaw("ShootHorizontal");
        // vertical bullet movement
        float bulletMovementY = Input.GetAxisRaw("ShootVertical");

        if ((bulletMovementX != 0 || bulletMovementY != 0 ) && Time.time > lastFire + fireDelay)
        {
            // shoot 
            shoot(bulletMovementX, bulletMovementY);
            lastFire = Time.time;
        }
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("projectile"))
        {
            health -= projectileDamage;
        }
    }

    public void Death()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletSprite, transform.position, transform.rotation);
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed, 0);
            
    }
}
