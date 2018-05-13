using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// firearm should have only one child: object where to instatiate bullets on fire and this script
/// </summary>

public class Firearm : MonoBehaviour {
    
    public float shootingRate = 2f;
    public GameObject bulletPrefab;
    public float damage;
    public float bulletSpeed;
    public float bulletDestroyAfter;
    public Animator animator;

    Transform projectileParent;
    Transform shootingFrom;
    private float timeTillNextShoot = 0;

    // Use this for initialization
    void Start()
    {
        shootingFrom = transform.GetChild(0);
        projectileParent = GameObject.Find("Projectiles").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeTillNextShoot > 0)
            timeTillNextShoot -= Time.deltaTime;
    }

    public void Shoot()
    {
        if(timeTillNextShoot <= 0)
        {
            if(animator != null)
                animator.SetTrigger("Fire");
            timeTillNextShoot = shootingRate;
            GameObject newBullet = Instantiate(bulletPrefab, shootingFrom.position, transform.rotation, projectileParent);
            newBullet.GetComponent<Bullet>().Instantiate(bulletDestroyAfter, bulletSpeed, damage);
        }
    }

	
}
