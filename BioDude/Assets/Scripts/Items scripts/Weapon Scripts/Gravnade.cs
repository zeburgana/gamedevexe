﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravnade : Explosive
{

    public float delay = 2f;
    float countdown;

    public GameObject blackHoleEffect;

    bool exploded = false;
    public float radius = 3f;
    public float force = 500f;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        countdown = delay;
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(!started)
        {
            Throw(throwForce);
            started = true;
        }
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !exploded)
        {
            Explode();
            exploded = true;
        }
    }

    public override void Explode()
    {
        //gravnade effect
        Instantiate(blackHoleEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public override void Throw(float force)
    {
        rb.AddForce(transform.up * force);
    }

}