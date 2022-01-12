using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    public float deathLevel;
    private Vector2 respawnPoint;

    void Start()
    {
        GameObject respawnPointGO = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnPointGO == null) 
            respawnPoint = new Vector2(0, 1f);
        else 
            respawnPoint = respawnPointGO.transform.position;
    }

    void Update()
    {
        if (transform.position.y < deathLevel)
            transform.position = respawnPoint;
    }
}
