using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerControl : MonoBehaviour
{


    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    ObjectSounds sfxManager;

    public AudioClip jumpSound;
    public AudioClip shotSound;
    public AudioMixerGroup soundFXGroup;


    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    bool coroutineRunning = false;

    private int _lives = 1;
    public int maxLives = 3;

    public int lives
    {
        get { return _lives; }
        set
        {
           

            _lives = value;

            if (_lives > maxLives)
                _lives = maxLives;

           

            Debug.Log("Lives Set To: " + lives.ToString());
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sfxManager = GetComponent<ObjectSounds>();

        if (!groundCheck)
        {
            groundCheck = GameObject.FindGameObjectWithTag("Ground Check").transform;
        }

        if (speed <= 0)
        {
            speed = 4.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 270;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
        }

        if (!sfxManager)
        {
            sfxManager = gameObject.AddComponent<ObjectSounds>();
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");


        if (Input.GetMouseButtonDown(0))
        {
            sfxManager.Play(shotSound, soundFXGroup);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            sfxManager.Play(jumpSound, soundFXGroup);
        }




        Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("shoot", Input.GetMouseButton(0));

       
        if (horizontalInput > 0.1f)
        {
            sr.flipX = true;
        }
        if (horizontalInput < -0.1f)
        {
            sr.flipX = false;
        }

    }

    public void StartJumpForceChange()
    {
        if (!coroutineRunning)
        {
            StartCoroutine("JumpForceChange");
        }
        else
        {
            StopCoroutine("JumpForceChange");
            jumpForce /= 2;
            StopCoroutine("JumpForceChange");
        }

    }

    IEnumerator JumpForceChange()
    {
        coroutineRunning = true;
        jumpForce *= 2;

        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        coroutineRunning = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Squish")
        {
            collision.gameObject.GetComponentInParent<EnemyWalker>().IsSquished();

            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
}