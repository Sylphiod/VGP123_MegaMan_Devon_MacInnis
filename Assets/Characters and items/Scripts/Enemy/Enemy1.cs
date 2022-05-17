using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class Enemy1 : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public float speed;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask isplayerLayer;
    public Transform playerCheck;
    public float playerRadius;
    public bool nearPlayer;
    
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (!groundCheck)
        {
            groundCheck = GameObject.FindGameObjectWithTag("Ground Check").transform;
        }

        if (!playerCheck)
        {
            playerCheck = GameObject.FindGameObjectsWithTag("Player Check").transform;
        }

        if (speed <= 0)
        {
            speed = 4.0f;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
        }

        if (playerRadius <= 0)
        {
            playerRadius = 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        nearPlayer = Physics2D.OverlapCircle(playerCheck.position, playerRadius, isplayerLayer);

        Vector2 moveDirection = new Vector2(nearPlayer * speed, rb.velocity.y);
        rb.velocity = moveDirection;
    }
    }
}
