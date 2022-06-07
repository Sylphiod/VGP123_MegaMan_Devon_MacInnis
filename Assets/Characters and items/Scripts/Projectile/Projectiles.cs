using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damageValue;

    void Start()
    {
        if (lifetime <= 0)
            lifetime = 2.0f;

       GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            Destroy(gameObject);


        if (collision.gameObject.tag == "Enemy")
        {
            if (gameObject.tag == "PlayerProjectile")
            {
                Enemy e = collision.gameObject.GetComponent<Enemy>();

                if (e)
                    e.TakeDamage(damageValue);

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "EnemyProjectile")
        {
            GameManager.instance.lives--;
            Destroy(gameObject);
        }
    }
}
