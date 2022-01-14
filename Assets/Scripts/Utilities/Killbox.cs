using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    private PlayerMovement pm;
    [SerializeField] private float deathLevel; 
    
    private void Awake()
    {
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (transform.position.y < deathLevel)
        {
            pm.Respawn();
        }
    }
}
