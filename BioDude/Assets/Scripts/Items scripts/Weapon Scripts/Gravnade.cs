using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        countdown = delay;
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
        Debug.Log("boom");
        //gravnade effect
        Instantiate(blackHoleEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}