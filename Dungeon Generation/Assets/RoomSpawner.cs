﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour{

    public int openingDirection;
    // 1 need bottom door
    // 2 need a top door
    // 3 need a left door
    // 4 need a right door

    private RoomTemplate templates;
    private int rand;
    private bool spawned = false;


    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        Invoke("Spawn", 0.1f);
    }
    private void Spawn()
    {
        if(spawned == false)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }

            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("spawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && !spawned)
            {
                // spawn walls blocking off to any openings
                Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
                //Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
