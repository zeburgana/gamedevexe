using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenade : Explosive
{

    public float delay = 2f;
    float countdown;

    public GameObject explosionEffect;

    bool exploded = false;
    public float radius = 3f;
    public float force = 500f;

    // Use this for initialization
    void Start()
    {
        countdown = delay;
        //GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 500f)); //transform.forward.z * speed - 10, transform.forward.z * speed + 60
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !exploded)
        {
            Explode();
            exploded = true;
        }
    }

    public override void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D obj in nearbyObjects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log("rigidbody found");
                AddExplosionForce(rb, force, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }

}