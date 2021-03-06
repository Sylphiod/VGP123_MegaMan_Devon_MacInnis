using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Turret : enemy
{
    [SerializeField] float projectileForce;
    [SerializeField] float projectileFireRate;
    [SerializeField] float turretFireDistance;

    float timeSinceLastFire;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public Projectiles projectilePrefab;

    ObjectSounds sfxManager;



    public AudioClip FireSound;

    public AudioMixerGroup soundFXGroup;



    public override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2.0f;

        if (projectileForce <= 0)
            projectileForce = 7.0f;

        if (turretFireDistance <= 0)
            turretFireDistance = 5.0f;
    }

    public override void Death()
    {
        base.Death();

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Fire"))
        {
            if (Time.time >= timeSinceLastFire + projectileFireRate)
            {
                anim.SetBool("Fire", true);
            }
        }


        if (pc.gameObject.transform.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    public void Fire()
    {
        timeSinceLastFire = Time.time;

        if (pc.gameObject.transform.position.x < transform.position.x)
        {
            Projectiles temp = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            temp.speed = -projectileForce;
        }
        else
        {
            Projectiles temp = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            temp.speed = projectileForce;
            sfxManager.Play(FireSound, soundFXGroup);
        }
    }

    public void ReturnToIdle()
    {
        anim.SetBool("Fire", false);
    }
}