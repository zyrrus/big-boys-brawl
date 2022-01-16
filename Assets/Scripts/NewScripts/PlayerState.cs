using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoysBrawl
{    
    public class PlayerState : MonoBehaviour
    {
        // TODO: Get rid of unshared variables

        [Header("Movement")]
        public float moveSpeed;
        public float acceleration;
        public float deceleration;
        public float airAccel;
        public float airDecel;
        public float velPower;
        public float frictionAmount;
        public bool canMove = false;

        [Header("Jump")]
        [Range(0, 1)] 
        public float jumpCutMultiplier;
        public float jumpForce;
        public float fallGravityMultiplier;

        [Header("Checks")]
        public Transform groundCheckPoint;
        public Vector2 groundCheckSize;
        public float jumpCoyoteTime;
        public float jumpBufferTime;

        [Header("Layers & Tags")]
        public LayerMask groundLayer;
    }
}