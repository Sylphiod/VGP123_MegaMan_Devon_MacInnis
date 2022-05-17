using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
     
    SpriteRenderer sr;
    Animator anim;


    public Transform projectileSpawnPointLeft;
    public Transform projectileSpawnPointRight;
    public float projectileSpeed;
    public Projectiles projectilePrefab;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();


        if (projectileSpeed <= 0)
        { projectileSpeed = 0.9f; }
        if (!projectileSpawnPointLeft || !projectileSpawnPointRight || !projectilePrefab)
            Debug.LogWarning("Issue with inspector Values");

    }

    // Update is called once per frame
    void Update()
    {
    
        
    }
    void Fire()
    {
        if (!sr.flipX)
        {
            Projectiles curProjectile = Instantiate(projectilePrefab, projectileSpawnPointLeft.position, projectileSpawnPointLeft.rotation);
            curProjectile.speed = -projectileSpeed;
        }
        else
        {
            Projectiles curProjectile = Instantiate(projectilePrefab, projectileSpawnPointRight.position, projectileSpawnPointRight.rotation);
            curProjectile.speed = projectileSpeed;
        }
    }
}
