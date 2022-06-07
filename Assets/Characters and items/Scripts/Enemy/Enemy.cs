using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;
    protected PlayerControl pc;

    protected int _health;
    [SerializeField] protected int maxHealth;

    public int health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health > maxHealth)
            {
                _health = maxHealth;
            }

            if (_health <= 0)
                Death();
        }
    }


    public virtual void Death()
    {
        
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }

    
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pc = FindObjectOfType<PlayerControl>();

        if (maxHealth <= 0)
            maxHealth = 10;

        health = maxHealth;
    }
}
