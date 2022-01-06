using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    public float deathLevel;
    private Vector2 respawnPoint;

    void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }

    void Update()
    {
        if (transform.position.y < deathLevel)
            transform.position = respawnPoint;
    }
}
